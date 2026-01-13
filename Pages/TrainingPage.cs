using Source;
using Source.Data;
using MaterialSkin.Controls;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace WinForm_RFBN_APP
{
    public partial class TrainingPage : UserControl
    {
        private MaterialComboBox ModelTypeComboBox;

        #region Constructor ---------------------------------------------------------

        public TrainingPage()
        {
            InitializeComponent();
            SetupCustomUI();
        }

        private void SetupCustomUI()
        {
            // 1. Create Model Type Selector
            ModelTypeComboBox = new MaterialComboBox
            {
                Dock = DockStyle.Fill,
                Hint = "Select Model Type"
            };
            ModelTypeComboBox.Items.Add("RBF Network (Classification)"); // Index 0
            ModelTypeComboBox.Items.Add("Decision Tree (Regression)");   // Index 1
            ModelTypeComboBox.SelectedIndex = 0;
            ModelTypeComboBox.SelectedIndexChanged += ModelTypeComboBox_SelectedIndexChanged;

            // 2. Adjust Layout to insert Model Type at Top
            // Shift existing controls down is hard without strict order, 
            // so let's just prepend a row if possible or append.
            // Appending is safer. Let's append to the BOTTOM for now to avoid breaking layout logic.
            // Actually, Designer has 5 rows. Let's add a 6th row.
            
            ConfigLayout.RowCount++;
            ConfigLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
            
            // Add Label
            var label = new MaterialLabel { Text = "Model Type:", Anchor = AnchorStyles.Left, AutoSize = true };
            ConfigLayout.Controls.Add(label, 0, 5);
            ConfigLayout.Controls.Add(ModelTypeComboBox, 1, 5);
        }

        private void ModelTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool isRbf = ModelTypeComboBox.SelectedIndex == 0;
            
            // Toggle RBF-specific fields
            EpochsTextBox.Enabled = isRbf;
            HiddenNeuronsTextBox.Enabled = isRbf;
            LearningRateTextBox.Enabled = isRbf;
            
            // Default File/Schema updates
            if (isRbf)
            {
                if (InputDocumentTextbox.Text.Contains("ML_Training_Data")) 
                    InputDocumentTextbox.Text = "train_80k.csv";
                    
                if (FeaturesTextBox.Text.Contains("energy_kj")) 
                    FeaturesTextBox.Text = "PROTEIN;TOTAL_FAT;CARBS;ENERGY;FIBER;SATURATED_FAT;SUGARS;CLASSIFICATION";
            }
            else
            {
                InputDocumentTextbox.Text = "ML_Training_Data.csv";
                FeaturesTextBox.Text = "energy_kj;sugar_g;sat_fat_g;salt_g;fiber_g;protein_g;final_score";
            }
        }

        #endregion


        #region Button Commands -----------------------------------------------------

        private async void TrainButton_Click(object sender, EventArgs e)
        {
            try
            {
                TrainButton.Enabled = false;
                RichTextBoxOutput.Clear();
                RichTextBoxOutput.AppendText("--------------------------------------------------\n");
                RichTextBoxOutput.AppendText("Starting Training Pipeline...\n");

                bool isRbf = ModelTypeComboBox.SelectedIndex == 0;
                string modelName = isRbf ? "FoodClassifier_V1" : "ScorePredictor_V1";

                // 1. Get Schema
                string userFeatureSchema = FeaturesTextBox.Text.Trim();
                RichTextBoxOutput.AppendText($"Using Schema: {userFeatureSchema}\n");

                // 2. Load Data
                string inputDocumentName = InputDocumentTextbox.Text.Trim();
                if (string.IsNullOrEmpty(inputDocumentName)) inputDocumentName = isRbf ? "train_80k.csv" : "ML_Training_Data.csv";

                RichTextBoxOutput.AppendText($"Loading: {inputDocumentName}...\n");

                var rawData = await Task.Run(() => DataLoader.LoadCsv(inputDocumentName, userFeatureSchema));

                if (rawData.Inputs.Count == 0)
                {
                    RichTextBoxOutput.AppendText("Error: No training data found.\n");
                    return;
                }
                RichTextBoxOutput.AppendText($"Data Loaded: {rawData.Inputs.Count} records.\n");

                if (isRbf)
                {
                    // === RBF PATH ===
                    var (means, stdDevs) = NormalizationHelper.ComputeZScoreStats(rawData.Inputs);
                    
                    var normalizedInputs = new List<double[]>();
                    foreach (var row in rawData.Inputs)
                    {
                        normalizedInputs.Add(NormalizationHelper.NormalizeRow(row, means, stdDevs));
                    }

                    int hiddenNeurons = 25;
                    int epochs = 100;
                    double learningRate = 0.01d;

                    int.TryParse(HiddenNeuronsTextBox.Text, out hiddenNeurons);
                    int.TryParse(EpochsTextBox.Text, out epochs);
                    double.TryParse(LearningRateTextBox.Text, out learningRate);

                    RichTextBoxOutput.AppendText($"Training RBF ({hiddenNeurons} hidden, {epochs} epochs, LR: {learningRate})...\n");

                    var trainer = new RbfTrainer();
                    RbfNetwork network = await Task.Run(() =>
                    {
                        return trainer.Train(normalizedInputs, rawData.Targets, hiddenNeurons, epochs, learningRate);
                    });

                    string meanStr = NormalizationHelper.SerializeArray(means);
                    string stdStr = NormalizationHelper.SerializeArray(stdDevs);

                    ModelRepository.SaveModel(modelName, network, meanStr, stdStr, userFeatureSchema);
                    RichTextBoxOutput.AppendText($"SUCCESS: RBF Model '{modelName}' saved.\n");
                }
                else
                {
                    // === DECISION TREE PATH ===
                    // No normalization for DT
                    // Params could be added to UI, but hardcoding for now as per Console defaults
                    int minSamplesSplit = 10;
                    int maxDepth = 10;
                    
                    RichTextBoxOutput.AppendText($"Training Decision Tree (MinSplit: {minSamplesSplit}, MaxDepth: {maxDepth})...\n");

                    var dt = new ML_Project_Windows_App.DecisionTreeRegressor(minSamplesSplit, maxDepth);
                    await Task.Run(() => dt.Fit(rawData.Inputs.ToArray(), rawData.Targets.ToArray()));

                    ModelRepository.SaveDecisionTree(modelName, dt, userFeatureSchema);
                    RichTextBoxOutput.AppendText($"SUCCESS: DT Model '{modelName}' saved.\n");
                }
                
                RichTextBoxOutput.AppendText("--------------------------------------------------\n");
            }
            catch (Exception ex)
            {
                RichTextBoxOutput.AppendText($"\nCRITICAL ERROR: {ex.Message}\n");
                RichTextBoxOutput.AppendText(ex.StackTrace + "\n");
            }
            finally
            {
                TrainButton.Enabled = true;
            }
        }

        private void CleanButton_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
               "Are you sure you want to delete all trained models? This cannot be undone.",
               "Confirm Clear",
               MessageBoxButtons.YesNo,
               MessageBoxIcon.Warning);

            if (confirm == DialogResult.Yes)
            {
                ModelRepository.ClearModel();
                RichTextBoxOutput.AppendText("Database cleared.\n");
            }
        }

        #endregion

    }
}