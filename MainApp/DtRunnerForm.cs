using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Source.Data; // Ensure these namespaces match your project structure
using Source;
using ML_Project_Windows_App;

namespace WinForm_RFBN_APP
{
    public partial class DtRunnerForm : MaterialForm
    {
        // Model State
        private DecisionTreeRegressor? _trainedDecisionTree;
        private List<double[]>? _trainingInputs;
        // Default values now in Initializer (or can be set here if not in designer)
        // Designer sets "Text" properties, so we can rely on that, or set them here if we want defaults that differ from Designer.
        // The designer file has the defaults I wrote.
        
        public DtRunnerForm()
        {
            InitializeComponent(); 
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            // Unified Design: Match SelectionForm (Blue)
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue600, Primary.Blue700,
                Primary.Blue200, Accent.LightBlue200,
                TextShade.WHITE
            );
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            new MainScreen().ShowDialog();
            this.Close();
        }


        private void Log(string message)
        {
            if (txtOutput.InvokeRequired)
            {
                txtOutput.Invoke(new Action<string>(Log), message);
                return;
            }
            txtOutput.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\n");
            txtOutput.SelectionStart = txtOutput.Text.Length;
            txtOutput.ScrollToCaret();
        }

        // Helper for raw text (from Console redirection)
        private void AppendRawText(string text)
        {
             if (txtOutput.InvokeRequired)
            {
                txtOutput.Invoke(new Action<string>(AppendRawText), text);
                return;
            }
            txtOutput.AppendText(text);
            txtOutput.SelectionStart = txtOutput.Text.Length;
            txtOutput.ScrollToCaret();
        }

        private async void BtnTrain_Click(object sender, EventArgs e)
        {
            try
            {
                btnTrain.Enabled = false;
                Log("Starting Training Pipeline...");

                // Parse Params
                string trainFile = txtTrainFile.Text;
                string valFile = txtValFile.Text;
                string schema = txtSchema.Text;
                int totalSamples = int.Parse(txtTotalSamples.Text);
                int minSplit = int.Parse(txtMinSamplesSplit.Text);
                int minLeaf = int.Parse(txtMinSamplesLeaf.Text);
                int maxDepth = int.Parse(txtMaxDepth.Text);

                await Task.Run(() => 
                {
                    if (!File.Exists(trainFile)) { Log($"[Error] Train file not found: {trainFile}"); return; }
                    if (!File.Exists(valFile)) { Log($"[Error] Val file not found: {valFile}"); return; }

                    Log("[1/3] Loading Data...");
                    
                    var trainData = DataLoader.LoadCsv(trainFile, schema);
                    var valData = DataLoader.LoadCsv(valFile, schema);
                    
                    var trainInputs = trainData.Inputs;
                    var trainTargets = trainData.Targets;
                    var valInputs = valData.Inputs;
                    var valTargets = valData.Targets;

                    if (totalSamples > 0 && totalSamples < trainInputs.Count)
                    {
                        Log($"[Config] Limiting to {totalSamples} samples.");
                        trainInputs = trainInputs.Take(totalSamples).ToList();
                        trainTargets = trainTargets.Take(totalSamples).ToList();
                    }

                     // Save for explanation
                    _trainingInputs = trainInputs;

                    Log($"[2/3] Training Decision Tree (Split:{minSplit}, Leaf:{minLeaf}, Depth:{maxDepth})...");

                    // --- INLINED LOGIC FROM DecisionTreeModule ---
                    var trainInputsArray = trainInputs.ToArray();
                    var trainTargetsArray = trainTargets.ToArray();

                    var dt = new DecisionTreeRegressor(minSamplesSplit: minSplit, minSamplesLeaf: minLeaf, maxDepth: maxDepth);
                    dt.Fit(trainInputsArray, trainTargetsArray);
                    
                    _trainedDecisionTree = dt; // Store model

                    Log(" > Training Complete!");
                    
                    // Evaluation
                    Log("[3/3] Evaluating on Validation Set...");
                    
                    var predictor = new ScorePredictor(dt, schema);
                    
                    var predictions = new List<double>();
                    foreach (var input in valInputs)
                    {
                        predictions.Add(predictor.Predict(input));
                    }
                    
                    var metrics = Source.RegressionMetricsCalculator.Calculate(predictions.ToList(), valTargets.ToList());

                    Log("--------------------------------------------------");
                    Log(metrics.ToString());
                    Log("--------------------------------------------------");
                    
                    // Show a few examples
                    Log("\n Sample Predictions:");
                    for(int i=0; i<Math.Min(5, predictions.Count); i++)
                    {
                         Log($"   Actual: {valTargets[i]:F2} | Predicted: {predictions[i]:F2}");
                    }
                    
                    Log("TRAINING COMPLETE.");
                });

            }
            catch (Exception ex)
            {
                Log($"[Error] {ex.Message}");
                if(ex.InnerException != null) Log($"[Inner] {ex.InnerException.Message}");
            }
            finally
            {
                btnTrain.Enabled = true;
            }
        }

        private async void BtnOptimize_Click(object sender, EventArgs e)
        {
            try
            {
                btnOptimize.Enabled = false;
                Log("Starting Grid Search Optimization...");
                
                string schema = txtSchema.Text;
                string trainFile = txtTrainFile.Text;
                int totalSamples = int.Parse(txtTotalSamples.Text);
                
                // Redirect Console Output
                var originalOut = Console.Out;
                var guiWriter = new ConsoleWriter((text) => AppendRawText(text));
                Console.SetOut(guiWriter);

                await Task.Run(() =>
                {
                    try
                    {
                        if (!File.Exists(trainFile)) { Log("Error: File not found."); return; }
                        
                        var trainData = DataLoader.LoadCsv(trainFile, schema);
                        var inputs = trainData.Inputs;
                        var targets = trainData.Targets;

                        if (totalSamples > 0 && totalSamples < inputs.Count)
                        {
                            inputs = inputs.Take(totalSamples).ToList();
                            targets = targets.Take(totalSamples).ToList();
                        }

                        int[] depthGrid = { 15, 20, 30 };
                        int[] splitGrid = { 20, 30, 50 };
                        int[] leafGrid = { 20, 30, 50 };

                        Log("\n>>> KICKING OFF GRID SEARCH <<<\n");
                        
                        // Console outputs will now be captured by guiWriter
                        var (bestDepth, bestSplit, bestLeaf, bestScore) = GridSearchOptimizer.RunGridSearch(
                            inputs, 
                            targets, 
                            5,
                            depthGrid, 
                            splitGrid, 
                            leafGrid
                        );

                        Log($"\n>>> OPTIMIZATION COMPLETE <<<");
                        Log($"Best R2: {bestScore:F4}");
                        Log($"Best Params: Depth={bestDepth}, Split={bestSplit}, Leaf={bestLeaf}");

                        // Auto-update UI
                        this.Invoke(new Action(() => 
                        {
                            txtMaxDepth.Text = bestDepth.ToString();
                            txtMinSamplesSplit.Text = bestSplit.ToString();
                            txtMinSamplesLeaf.Text = bestLeaf.ToString();
                            Log("Updated UI with best parameters.");
                        }));
                    }
                    finally
                    {
                        // Restore Console? We can keep it redirected if we want. 
                        // But usually safest to restore if other components expect it (though this is WinForms)
                    }
                });
                
                // Restore logic moved inside finally block or here if we want persistent redirection
                Console.SetOut(originalOut); 
            }
            catch (Exception ex)
            {
                Log($"[Error] {ex.Message}");
            }
            finally
            {
                btnOptimize.Enabled = true;
                // Just in case
                var standardOut = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
                Console.SetOut(standardOut);
            }
        }

        private void BtnPredict_Click(object sender, EventArgs e)
        {
            if (_trainedDecisionTree == null)
            {
                MessageBox.Show("Please train the model first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create a dynamic input form
            var inputForm = new Form
            {
                Text = "Manual Prediction Input",
                Size = new Size(400, 500),
                StartPosition = FormStartPosition.CenterParent
            };

            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                Padding = new Padding(10),
                AutoSize = true
            };

            var schemaParts = txtSchema.Text.Split(';');
            var features = schemaParts.Take(schemaParts.Length - 1).ToList();
            var inputControls = new List<TextBox>();

            foreach (var feat in features)
            {
                layout.Controls.Add(new Label { Text = feat, AutoSize = true, Anchor = AnchorStyles.Left });
                var box = new TextBox { Width = 150 };
                inputControls.Add(box);
                layout.Controls.Add(box);
            }

            var btnOk = new Button { Text = "Predict", DialogResult = DialogResult.OK, Anchor = AnchorStyles.None, AutoSize = true };
            layout.Controls.Add(btnOk);
            layout.SetColumnSpan(btnOk, 2);

            inputForm.Controls.Add(layout);

            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    double[] inputs = new double[features.Count];
                    for (int i = 0; i < features.Count; i++)
                    {
                        if (double.TryParse(inputControls[i].Text, out double val))
                        {
                            inputs[i] = val;
                        }
                        else
                        {
                             MessageBox.Show($"Invalid value for {features[i]}");
                             return;
                        }
                    }

                    double score = _trainedDecisionTree.Predict(new double[][] { inputs })[0];
                    Log($"\n>> Predicted Score: {score:F4}");
                    MessageBox.Show($"Predicted Score: {score:F4}", "Result");
                    
                    // LIME
                    if (_trainingInputs != null && _trainingInputs.Count > 0)
                    {
                        Log("Running LIME Explanation...");
                        Task.Run(() => 
                        {
                            var explainer = new LimeExplainer(_trainingInputs);
                             Func<double[][], double[]> predictFn = (batch) =>
                            {
                                var outputs = new double[batch.Length];
                                for (int k = 0; k < batch.Length; k++)
                                {
                                        outputs[k] = _trainedDecisionTree.PredictSingle(batch[k]);
                                }
                                return outputs;
                            };

                            var explanation = explainer.Explain(inputs, predictFn, 500);
                             Log("Top 3 Influential Features:");
                            foreach (var (idx, weight) in explanation.Take(3))
                            {
                                string name = (idx < features.Count) ? features[idx] : $"Feat {idx}";
                                Log($"  {name}: {weight:F4}");
                            }
                        });
                    }

                }
                catch (Exception ex)
                {
                    Log($"Predict Error: {ex.Message}");
                }
            }
        }
    }

    // Console Writer Implementation
    public class ConsoleWriter : TextWriter
    {
        private Action<string> _output;
        public ConsoleWriter(Action<string> output)
        {
            _output = output;
        }

        public override void Write(char value)
        {
            _output(value.ToString());
        }

        public override void Write(string? value)
        {
            if (value != null) _output(value);
        }

        public override Encoding Encoding => Encoding.UTF8;
    }
}

