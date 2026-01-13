using System.Data;
using System.Globalization;
using Source.Data;
using Source;
using System.Windows.Forms.DataVisualization.Charting;

namespace WinForm_RFBN_APP
{
    public partial class ShapPage : UserControl
    {
        private FoodClassifier _classifier;
        private double[] _backgroundMeans; // Mean of each feature from training data
        private double[] _factorials;      // Helper for SHAP math

        public ShapPage()
        {
            InitializeComponent();
        }

        private async void ExplainButton_Click(object sender, EventArgs e)
        {
            // 1. Validate Feature Inputs
            if (!ParseInputs(out double[] inputFeatures)) return;

            string csvPath = CsvPathTextBox.Text.Trim();
            string schema = SchemaTextBox.Text.Trim();

            if (string.IsNullOrEmpty(csvPath)) csvPath = "train.csv";
            if (string.IsNullOrEmpty(schema)) schema = "energy_kcal;protein_g;carbohydrate_g;sugar_g;total_fat_g;sat_fat_g;fiber_g;salt_g;is_healthy";

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
                        // We use the full dataset to get accurate baseline (mean) values
                        var (inputs, _) = DataLoader.LoadCsv(csvPath, schema);
                        if (inputs.Count > 0)
                        {
                            var stats = NormalizationHelper.ComputeZScoreStats(inputs);
                            _backgroundMeans = stats.Mean;
                        }
                    });
                }

                if (_classifier == null)
                {
                    MessageBox.Show("Model 'FoodClassifier_V1' not found. Please train it first.");
                    return;
                }

                if (_backgroundMeans == null)
                {
                    MessageBox.Show("Could not calculate background means. Check CSV path.");
                    return;
                }

                // 3. Initialize Factorials for Math
                int n = inputFeatures.Length;
                _factorials = new double[n + 1];
                _factorials[0] = 1;
                for (int i = 1; i <= n; i++) _factorials[i] = i * _factorials[i - 1];

                // 4. Calculate Exact SHAP Values
                var shapValues = await Task.Run(() => CalculateExactShapValues(inputFeatures, _classifier, _backgroundMeans));

                // 5. Update Chart
                UpdateChart(shapValues, inputFeatures);

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

        // ---------------------------------------------------------
        // LOGIC: EXACT SHAPLEY CALCULATION (Coalition Method)
        // ---------------------------------------------------------
        private double[] CalculateExactShapValues(double[] instance, FoodClassifier classifier, double[] backgroundMeans)
        {
            int n = instance.Length;
            double[] shapValues = new double[n];
            int[] featureIndices = Enumerable.Range(0, n).ToArray();

            // Loop through every feature 'j' to find its contribution
            for (int j = 0; j < n; j++)
            {
                // Identify all other features
                var otherFeatures = featureIndices.Where(idx => idx != j).ToArray();

                // Iterate through all possible subsets (2^(N-1))
                int subsetCount = 1 << otherFeatures.Length;

                for (int i = 0; i < subsetCount; i++)
                {
                    // Construct Subset S
                    List<int> S = new List<int>();
                    for (int bit = 0; bit < otherFeatures.Length; bit++)
                    {
                        if ((i & (1 << bit)) != 0) S.Add(otherFeatures[bit]);
                    }

                    // Weight = (|S|! * (N - |S| - 1)!) / N!
                    int sSize = S.Count;
                    double weight = (_factorials[sSize] * _factorials[n - sSize - 1]) / _factorials[n];

                    // Prepare inputs
                    double[] inputWith = (double[])backgroundMeans.Clone();
                    double[] inputWithout = (double[])backgroundMeans.Clone();

                    // Fill subset features with ACTUAL values
                    foreach (int idx in S)
                    {
                        inputWith[idx] = instance[idx];
                        inputWithout[idx] = instance[idx];
                    }

                    // Toggle Target Feature j
                    inputWith[j] = instance[j];       // Present
                    inputWithout[j] = backgroundMeans[j]; // Absent (Mean)

                    // Marginal Contribution
                    double predWith = classifier.Predict(inputWith);
                    double predWithout = classifier.Predict(inputWithout);

                    shapValues[j] += weight * (predWith - predWithout);
                }
            }

            return shapValues;
        }

        private void UpdateChart(double[] shapValues, double[] inputFeatures)
        {
            ShapChart.Series["ShapValues"].Points.Clear();

            // Updated Schema to match Global Page (8 Items)
            string[] featureNames = { "Energy", "Protein", "Carbs", "Sugar", "Fat", "Sat. Fat", "Fiber", "Salt" };

            // Find min/max for chart scaling
            double minVal = Math.Min(0, shapValues.Min());
            double maxVal = Math.Max(0, shapValues.Max());

            for (int i = 0; i < shapValues.Length; i++)
            {
                // Ensure we don't crash if array lengths mismatch
                string name = i < featureNames.Length ? featureNames[i] : $"Feat {i}";

                var pointIndex = ShapChart.Series["ShapValues"].Points.AddXY(name, shapValues[i]);
                var point = ShapChart.Series["ShapValues"].Points[pointIndex];

                // Color Logic: 
                // Green = Pushes prediction HIGHER (Healthy)
                // Red   = Pushes prediction LOWER (Unhealthy)
                point.Color = shapValues[i] >= 0 ? Color.ForestGreen : Color.Crimson;

                // Detailed Label
                point.Label = $"{shapValues[i]:F4}";
                point.ToolTip = $"{name}\nInput Value: {inputFeatures[i]:F2}\nSHAP Impact: {shapValues[i]:F4}";
            }

            var area = ShapChart.ChartAreas[0];
            area.AxisX.Interval = 1;
            area.AxisY.Title = "Impact (SHAP Value)";

            // Add a horizontal line at 0 for clarity
            StripLine zeroLine = new StripLine();
            zeroLine.Interval = 0;
            zeroLine.StripWidth = 0.001;
            zeroLine.BackColor = Color.Black;
            zeroLine.IntervalOffset = 0;
            area.AxisY.StripLines.Clear();
            area.AxisY.StripLines.Add(zeroLine);

            area.RecalculateAxesScale();
        }

        private bool ParseInputs(out double[] inputs)
        {
            // IMPORTANT: Updated to size 8 to match schema
            inputs = new double[8];
            try
            {
                // Map UI Boxes to Schema Order:
                // energy_kcal;protein_g;carbohydrate_g;sugar_g;total_fat_g;sat_fat_g;fiber_g;salt_g

                inputs[0] = Parse(KcalBox.Text);       // Energy
                inputs[1] = Parse(ProteinBox.Text);    // Protein
                inputs[2] = Parse(CarbBox.Text);       // Carbs
                inputs[3] = Parse(SugarBox.Text);      // Sugar
                inputs[4] = Parse(FatBox.Text);        // Total Fat
                inputs[5] = Parse(SatFatBox.Text);     // Sat Fat
                inputs[6] = Parse(FiberBox.Text);      // Fiber
                inputs[7] = Parse(SaltTextBox.Text);   // Salt

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
            if (double.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
            {
                return result;
            }
            return 0.0;
        }
    }
}