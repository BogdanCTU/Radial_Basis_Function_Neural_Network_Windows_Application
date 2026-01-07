
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Source.Data;
using Source;

namespace WinForm_RFBN_APP
{
    public partial class GlobalShapPage : UserControl
    {
        private FoodClassifier _classifier;
        private double[] _backgroundMeans; 

        public GlobalShapPage()
        {
            InitializeComponent();
        }

        // ---------------------------------------------------------
        // EVENT: Calculate Global SHAP (Batch from CSV)
        // ---------------------------------------------------------
        private async void ExplainBatchButton_Click(object sender, EventArgs e)
        {
            string csvPath = CsvPathTextBox.Text.Trim();
            string schema = SchemaTextBox.Text.Trim();

            // Default fallback if empty
            if (string.IsNullOrEmpty(csvPath)) csvPath = "train_80k.csv";
            if (string.IsNullOrEmpty(schema)) schema = "PROTEIN;TOTAL_FAT;CARBS;ENERGY;FIBER;SATURATED_FAT;SUGARS;CLASSIFICATION";

            ExplainBatchButton.Enabled = false; 

            try
            {
                // 1. Load Model (Lazy Load)
                if (_classifier == null)
                {
                    _classifier = ModelRepository.LoadClassifier("FoodClassifier_V1");
                    if (_classifier == null)
                    {
                        MessageBox.Show("Model not found. Please train the model first.");
                        return;
                    }
                }

                // 2. Run Batch Calculation on Background Thread
                var globalShapValues = await Task.Run(() => 
                {
                    // Reuse existing DataLoader to parse CSV
                    var (inputs, _) = DataLoader.LoadCsv(csvPath, schema);
                    
                    if (inputs.Count == 0) throw new Exception("No data found in CSV.");

                    // Compute background means from this dataset
                    var stats = NormalizationHelper.ComputeZScoreStats(inputs);
                    _backgroundMeans = stats.Mean;

                    // Calculate Global Importance
                    return CalculateGlobalShapValues(inputs, _classifier, _backgroundMeans);
                });

                // 3. Update Chart (SteelBlue for Global Importance)
                UpdateGlobalChart(globalShapValues);
                
                MessageBox.Show("Global SHAP calculation complete.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                ExplainBatchButton.Enabled = true;
            }
        }

        // ---------------------------------------------------------
        // LOGIC: Global SHAP Calculation
        // ---------------------------------------------------------
        private double[] CalculateGlobalShapValues(List<double[]> inputs, FoodClassifier classifier, double[] backgroundMeans)
        {
            int featureCount = inputs[0].Length;
            double[] globalShapAccumulator = new double[featureCount];

            // A. Iterate through every record in the file
            foreach (var input in inputs)
            {
                // Calculate SHAP for this specific record
                double[] localShap = CalculateSingleInstanceShap(input, classifier, backgroundMeans);

                // B. Accumulate the ABSOLUTE values
                // We use Abs() because we want to know magnitude of importance.
                // -0.5 (Unhealthy) and +0.5 (Healthy) are both "Important".
                for (int i = 0; i < featureCount; i++)
                {
                    globalShapAccumulator[i] += Math.Abs(localShap[i]);
                }
            }

            // C. Average the results
            for (int i = 0; i < featureCount; i++)
            {
                globalShapAccumulator[i] /= inputs.Count;
            }

            return globalShapAccumulator;
        }

        private double[] CalculateSingleInstanceShap(double[] input, FoodClassifier classifier, double[] backgroundMeans)
        {
            double basePrediction = classifier.Predict(input);
            double[] shapValues = new double[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                // Perturbation: Replace feature i with background mean
                double[] perturbedInput = (double[])input.Clone();
                perturbedInput[i] = backgroundMeans[i];

                double perturbedPrediction = classifier.Predict(perturbedInput);
                
                // Marginal Contribution
                shapValues[i] = basePrediction - perturbedPrediction;
            }

            return shapValues;
        }

        // ---------------------------------------------------------
        // UI: Chart Update
        // ---------------------------------------------------------
        private void UpdateGlobalChart(double[] shapValues)
        {
            ShapChart.Series["ShapValues"].Points.Clear();
            
            // Assuming standard features order based on your schema defaults
            string[] featureNames = { "Protein", "Fat", "Carbs", "Kcal", "Fiber", "Sat. Fat", "Sugar" };

            // Sort by magnitude (Pareto style)
            var sortedPoints = shapValues
                .Select((val, index) => new { Value = val, Name = (index < featureNames.Length ? featureNames[index] : $"Feat {index}") })
                .OrderByDescending(x => x.Value) 
                .ToList();

            foreach (var item in sortedPoints)
            {
                var pointIndex = ShapChart.Series["ShapValues"].Points.AddXY(item.Name, item.Value);
                var point = ShapChart.Series["ShapValues"].Points[pointIndex];

                // Global Importance is always positive, so we use a neutral "Analytical" color like SteelBlue
                point.Color = Color.SteelBlue;
                point.Label = $"{item.Value:F4}";
            }

            ShapChart.ChartAreas[0].AxisY.Title = "Mean |Impact| (Global Importance)";
            ShapChart.ChartAreas[0].RecalculateAxesScale();
        }
    }
}
