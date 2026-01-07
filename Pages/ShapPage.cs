
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Source.Data;
using Source;

namespace WinForm_RFBN_APP
{
    public partial class ShapPage : UserControl
    {
        private FoodClassifier _classifier;
        private double[] _backgroundMeans; // Mean of each feature from training data

        public ShapPage()
        {
            InitializeComponent();
        }

        private async void ExplainButton_Click(object sender, EventArgs e)
        {
            // 1. Validate Feature Inputs
            if (!ParseInputs(out double[] inputFeatures)) return;

            ExplainButton.Enabled = false;

            try
            {
                // 2. Load Model & Background Data (Lazy Loading)
                if (_classifier == null)
                {
                    await Task.Run(() =>
                    {
                        // Load Classifier
                        _classifier = ModelRepository.LoadClassifier("FoodClassifier_V1");
                        
                        // Load Background Data (Means)
                        // We use the same cache file or re-calculate from CSV if needed.
                        // For efficiency, we'll re-calculate means from the CSV here as a "Background Dataset" proxy.
                        var (inputs, _) = DataLoader.LoadCsv(
                            @"e:\_3_Master\_1_Machine_Learning\Project_Repository\Radial_Basis_Function_Neural_Network_Windows_Application\DataSet\train_80k.csv",
                            "PROTEIN;TOTAL_FAT;CARBOHYDRATE;ENERGY_KCAL;FIBER;SATURATED_FAT;SUGAR;CLASSIFICATION");

                        var stats = NormalizationHelper.ComputeZScoreStats(inputs);
                        _backgroundMeans = stats.Mean;
                    });
                }

                if (_classifier == null)
                {
                    MessageBox.Show("Model 'FoodClassifier_V1' not found. Please train it first.");
                    return;
                }

                // 3. Calculate SHAP Values (Simplified: Perturbation from Mean)
                var shapValues = await Task.Run(() => CalculateShapValues(inputFeatures, _classifier, _backgroundMeans));

                // 4. Update Chart
                UpdateChart(shapValues);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                ExplainButton.Enabled = true;
            }
        }

        private double[] CalculateShapValues(double[] input, FoodClassifier classifier, double[] backgroundMeans)
        {
            double basePrediction = classifier.Predict(input);
            double[] shapValues = new double[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                // Create perturbed input: Replace feature i with background mean
                double[] perturbedInput = (double[])input.Clone();
                perturbedInput[i] = backgroundMeans[i];

                double perturbedPrediction = classifier.Predict(perturbedInput);

                // Contribution = Actual - Perturbed
                // If Actual is HIGHER (closer to 1/Healthy) than Perturbed, feature contributed Positively.
                // If Actual is LOWER, feature contributed Negatively.
                shapValues[i] = basePrediction - perturbedPrediction;
            }

            return shapValues;
        }

        private void UpdateChart(double[] shapValues)
        {
            ShapChart.Series["ShapValues"].Points.Clear();

            string[] featureNames = { "Protein", "Fat", "Carbs", "Kcal", "Fiber", "Sat. Fat", "Sugar" };

            for (int i = 0; i < shapValues.Length; i++)
            {
                var pointIndex = ShapChart.Series["ShapValues"].Points.AddXY(featureNames[i], shapValues[i]);
                var point = ShapChart.Series["ShapValues"].Points[pointIndex];

                // Color Coding: Red = Negative Impact (Towards 0/Unhealthy), Green = Positive Impact (Towards 1/Healthy)
                // Note: The logic depends on what 1 represents. 
                // Assuming 1 = Healthy.
                // Positive SHAP = Pushed towards Healthy.
                // Negative SHAP = Pushed towards Unhealthy.
                
                point.Color = shapValues[i] >= 0 ? Color.ForestGreen : Color.Crimson;
                point.Label = $"{shapValues[i]:F4}";
            }
            
            ShapChart.ChartAreas[0].AxisX.Interval = 1;
            ShapChart.ChartAreas[0].AxisY.Title = "Impact on Prediction";
            ShapChart.ChartAreas[0].RecalculateAxesScale();
        }

        private bool ParseInputs(out double[] inputs)
        {
            inputs = new double[7];
            try
            {
                inputs[0] = Parse(ProteinBox.Text);
                inputs[1] = Parse(FatBox.Text);
                inputs[2] = Parse(CarbBox.Text);
                inputs[3] = Parse(KcalBox.Text);
                inputs[4] = Parse(FiberBox.Text);
                inputs[5] = Parse(SatFatBox.Text);
                inputs[6] = Parse(SugarBox.Text);
                return true;
            }
            catch
            {
                MessageBox.Show("Invalid numeric input.");
                return false;
            }
        }

        private double Parse(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return 0.0;
            return double.Parse(text, CultureInfo.InvariantCulture);
        }
    }
}
