using Source;
using Source.Data;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Concurrent;

namespace WinForm_RFBN_APP
{
    public partial class GlobalShapPage : UserControl
    {
        private FoodClassifier _classifier;
        private double[] _backgroundMeans;
        private double[] _factorials;

        // 1000 samples is statistically sufficient to represent the Mean of 80k rows
        private const int MAX_SAMPLE_SIZE = 1000;

        public GlobalShapPage()
        {
            InitializeComponent();
        }

        public class ShapResult
        {
            public List<double[]> ShapValues { get; set; }
            public List<double[]> FeatureValues { get; set; }
            public string[] FeatureNames { get; set; }
        }

        // ---------------------------------------------------------
        // BUTTON 1: CALCULATE & SHOW BEESWARM (Detailed View)
        // ---------------------------------------------------------
        private async void ExplainSHAPButton_Click(object sender, EventArgs e)
        {
            // Always Recalculate
            var result = await RunShapCalculationAsync();

            if (result != null)
            {
                DrawBeeswarmPlot(result);
            }
        }

        // ---------------------------------------------------------
        // BUTTON 2: SHOW MEAN IMPORTANCE (Global Bar Chart)
        // ---------------------------------------------------------
        // Link this to your new button (e.g., btnShowMean)
        private async void ExplainMeanSHAPButton_Click(object sender, EventArgs e)
        {
            // Always Recalculate
            var result = await RunShapCalculationAsync();

            if (result != null)
            {
                DrawGlobalImportanceChart(result);
            }
        }

        // ---------------------------------------------------------
        // LOGIC: Shared Calculation Runner (Returns Result directly)
        // ---------------------------------------------------------
        private async Task<ShapResult> RunShapCalculationAsync()
        {
            string csvPath = CsvPathTextBox.Text.Trim();
            string schema = SchemaTextBox.Text.Trim();

            if (string.IsNullOrEmpty(csvPath)) csvPath = "train.csv";
            if (string.IsNullOrEmpty(schema)) schema = "energy_kcal;protein_g;carbohydrate_g;sugar_g;total_fat_g;sat_fat_g;fiber_g;salt_g;is_healthy";

            // Disable buttons to prevent double-clicking
            ExplainMeanSHAPButton.Enabled = false;
            // ShowMeanShapButton.Enabled = false; // Uncomment if you have this button reference

            try
            {
                if (_classifier == null)
                {
                    _classifier = ModelRepository.LoadClassifier("FoodClassifier_V1");
                    if (_classifier == null)
                    {
                        MessageBox.Show("Model not found. Please train the model first.");
                        return null;
                    }
                }

                return await Task.Run(() =>
                {
                    var (inputs, _) = DataLoader.LoadCsv(csvPath, schema);
                    if (inputs.Count == 0) throw new Exception("No data found in CSV.");

                    // 1. Compute Means using the FULL dataset for accuracy
                    var stats = NormalizationHelper.ComputeZScoreStats(inputs);
                    _backgroundMeans = stats.Mean;

                    // 2. Initialize Factorials
                    int featureCount = inputs[0].Length;
                    _factorials = new double[featureCount + 1];
                    _factorials[0] = 1;
                    for (int i = 1; i <= featureCount; i++) _factorials[i] = i * _factorials[i - 1];

                    // 3. Sample the data
                    var random = new Random();
                    var sampledInputs = inputs.Count > MAX_SAMPLE_SIZE
                        ? inputs.OrderBy(x => random.Next()).Take(MAX_SAMPLE_SIZE).ToList()
                        : inputs;

                    // 4. Calculate
                    return CalculateRawShapValues(sampledInputs, _classifier, _backgroundMeans);
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return null;
            }
            finally
            {
                ExplainMeanSHAPButton.Enabled = true;
                // ShowMeanShapButton.Enabled = true;
            }
        }

        // ---------------------------------------------------------
        // CHART 1: Global Feature Importance (Bar Chart)
        // ---------------------------------------------------------
        private void DrawGlobalImportanceChart(ShapResult data)
        {
            ShapChart.Series.Clear();
            ShapChart.ChartAreas[0].AxisY.CustomLabels.Clear();

            // Reset Axis Styling for Bar Chart
            ShapChart.ChartAreas[0].AxisY.LabelStyle.Enabled = true;
            ShapChart.ChartAreas[0].AxisY.MajorGrid.Enabled = true;
            ShapChart.ChartAreas[0].AxisY.MajorTickMark.Enabled = true;
            ShapChart.ChartAreas[0].AxisY.Interval = 0; // Auto interval
            ShapChart.ChartAreas[0].AxisY.Minimum = double.NaN; // Auto
            ShapChart.ChartAreas[0].AxisY.Maximum = double.NaN; // Auto

            ShapChart.ChartAreas[0].AxisX.Title = "Feature Name";
            ShapChart.ChartAreas[0].AxisY.Title = "Mean |SHAP Value| (Global Importance)";

            var series = new Series("Importance");
            series.ChartType = SeriesChartType.Column; // Vertical Bars
            series.Color = Color.SteelBlue;
            ShapChart.Series.Add(series);

            // 1. Calculate Mean Absolute SHAP for each feature
            int featureCount = data.FeatureNames.Length;
            double[] meanAbsShap = new double[featureCount];

            foreach (var row in data.ShapValues)
            {
                for (int i = 0; i < featureCount; i++)
                {
                    meanAbsShap[i] += Math.Abs(row[i]);
                }
            }

            for (int i = 0; i < featureCount; i++)
            {
                meanAbsShap[i] /= data.ShapValues.Count;
            }

            // 2. Sort and Plot
            var sortedData = meanAbsShap
                .Select((val, idx) => new { Value = val, Name = data.FeatureNames[idx] })
                .OrderByDescending(x => x.Value) // Highest impact first
                .ToList();

            foreach (var item in sortedData)
            {
                int idx = series.Points.AddXY(item.Name, item.Value);
                series.Points[idx].Label = $"{item.Value:F4}";
                series.Points[idx].ToolTip = $"Feature: {item.Name}\nMean Impact: {item.Value:F4}";
            }

            ShapChart.ChartAreas[0].RecalculateAxesScale();
        }

        // ---------------------------------------------------------
        // CHART 2: Beeswarm Plot
        // ---------------------------------------------------------
        private void DrawBeeswarmPlot(ShapResult data)
        {
            ShapChart.Series.Clear();
            ShapChart.ChartAreas[0].AxisY.CustomLabels.Clear();

            var series = new Series("Beeswarm");
            series.ChartType = SeriesChartType.Point;
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerSize = 6;
            series.Color = Color.FromArgb(100, Color.Gray);
            ShapChart.Series.Add(series);

            // 1. Sort features by Global Importance
            var featureImportance = new double[data.FeatureNames.Length];
            foreach (var row in data.ShapValues)
            {
                for (int i = 0; i < row.Length; i++) featureImportance[i] += Math.Abs(row[i]);
            }

            var sortedIndices = featureImportance
                .Select((val, idx) => new { Value = val, Index = idx })
                .OrderBy(x => x.Value) // Ascending for Y-axis
                .Select(x => x.Index)
                .ToArray();

            // 2. Setup Axis for Labels
            var area = ShapChart.ChartAreas[0];
            area.AxisY.LabelStyle.Enabled = true;
            area.AxisY.MajorGrid.Enabled = false;
            area.AxisY.MajorTickMark.Enabled = false;
            area.AxisY.Interval = 1;
            area.AxisY.Minimum = -0.5;
            area.AxisY.Maximum = sortedIndices.Length - 0.5;
            area.AxisX.Title = "SHAP Value (Impact on Model Output)";
            area.AxisY.Title = "";

            // 3. Pre-calculate Min/Max for Coloring
            int totalPoints = data.ShapValues.Count;
            int featureCount = data.FeatureNames.Length;
            double[] minFeat = new double[featureCount];
            double[] maxFeat = new double[featureCount];

            for (int i = 0; i < featureCount; i++) { minFeat[i] = double.MaxValue; maxFeat[i] = double.MinValue; }

            foreach (var row in data.FeatureValues)
            {
                for (int i = 0; i < featureCount; i++)
                {
                    if (row[i] < minFeat[i]) minFeat[i] = row[i];
                    if (row[i] > maxFeat[i]) maxFeat[i] = row[i];
                }
            }

            Random rnd = new Random();

            // 4. Plot Points
            for (int yIndex = 0; yIndex < sortedIndices.Length; yIndex++)
            {
                int originalFeatureIndex = sortedIndices[yIndex];
                string featureName = data.FeatureNames[originalFeatureIndex];

                area.AxisY.CustomLabels.Add(yIndex - 0.5, yIndex + 0.5, featureName);

                for (int i = 0; i < totalPoints; i++)
                {
                    if (i >= data.ShapValues.Count) break;

                    double shapValue = data.ShapValues[i][originalFeatureIndex];
                    double featureValue = data.FeatureValues[i][originalFeatureIndex];
                    double jitter = (rnd.NextDouble() - 0.5) * 0.5;
                    double yPos = yIndex + jitter;

                    int ptIdx = series.Points.AddXY(shapValue, yPos);
                    DataPoint pt = series.Points[ptIdx];

                    double range = maxFeat[originalFeatureIndex] - minFeat[originalFeatureIndex];
                    double normalized = 0.5;
                    if (range > 0.000001) normalized = (featureValue - minFeat[originalFeatureIndex]) / range;

                    pt.Color = GetColorGradient(normalized);
                    pt.ToolTip = $"{featureName}\nVal: {featureValue:F2}\nSHAP: {shapValue:F4}";
                }
            }
            area.RecalculateAxesScale();
        }

        private ShapResult CalculateRawShapValues(List<double[]> inputs, FoodClassifier classifier, double[] backgroundMeans)
        {
            string[] names = { "Energy", "Protein", "Carbs", "Sugar", "Fat", "Sat. Fat", "Fiber", "Salt" };

            var result = new ShapResult
            {
                ShapValues = new List<double[]>(),
                FeatureValues = inputs,
                FeatureNames = names
            };

            var resultsBag = new ConcurrentBag<double[]>();
            Parallel.ForEach(inputs, input =>
            {
                resultsBag.Add(CalculateSingleInstanceShap(input, classifier, backgroundMeans));
            });
            result.ShapValues = resultsBag.ToList();
            return result;
        }

        private double[] CalculateSingleInstanceShap(double[] instance, FoodClassifier classifier, double[] backgroundMeans)
        {
            int n = instance.Length;
            double[] shapValues = new double[n];
            int[] featureIndices = Enumerable.Range(0, n).ToArray();

            for (int j = 0; j < n; j++)
            {
                var otherFeatures = featureIndices.Where(idx => idx != j).ToArray();
                int subsetCount = 1 << otherFeatures.Length;

                for (int i = 0; i < subsetCount; i++)
                {
                    List<int> S = new List<int>();
                    for (int bit = 0; bit < otherFeatures.Length; bit++)
                    {
                        if ((i & (1 << bit)) != 0) S.Add(otherFeatures[bit]);
                    }

                    int sSize = S.Count;
                    double weight = (_factorials[sSize] * _factorials[n - sSize - 1]) / _factorials[n];

                    double[] inputWith = (double[])backgroundMeans.Clone();
                    double[] inputWithout = (double[])backgroundMeans.Clone();

                    foreach (int idx in S)
                    {
                        double val = instance[idx];
                        inputWith[idx] = val;
                        inputWithout[idx] = val;
                    }
                    inputWith[j] = instance[j];
                    inputWithout[j] = backgroundMeans[j];

                    double predictionWith = classifier.Predict(inputWith);
                    double predictionWithout = classifier.Predict(inputWithout);

                    shapValues[j] += weight * (predictionWith - predictionWithout);
                }
            }
            return shapValues;
        }

        private Color GetColorGradient(double value)
        {
            value = Math.Max(0, Math.Min(1, value));
            int r = (int)(255 * value);
            int b = (int)(255 * (1 - value));
            return Color.FromArgb(150, r, 0, b);
        }
    }
}