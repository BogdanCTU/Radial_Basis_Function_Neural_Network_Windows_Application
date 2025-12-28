using Source;
using Source.Data;
using System.Data;

namespace WinForm_RFBN_APP
{
    public partial class TestingPage : UserControl
    {
        public TestingPage()
        {
            InitializeComponent();
        }

        private void TestButton_Click(object sender, EventArgs e)
        {
            RichTextBoxOutput.AppendText("--------------------------------------------------\n");

            // 1. Load Model Entity from DB
            using var db = new AppDbContext();
            var entity = db.TrainedModels
                            .OrderByDescending(m => m.CreatedAt)
                            .FirstOrDefault(m => m.ModelName == "FoodClassifier_V1");

            if (entity == null)
            {
                RichTextBoxOutput.AppendText("No model found. Please train first.\r\n");
                return;
            }

            // 2. Reconstruct Network
            var model = new RbfNetwork(entity.InputCount, entity.HiddenCount, 1);
            model.Bias = entity.Bias;
            model.Weights = entity.WeightsData.Split(';').Select(val => double.Parse(val, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            model.Sigmas = entity.SigmasData.Split(';').Select(val => double.Parse(val, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            model.Centroids = ModelRepository.DeserializeCentroids(entity.CentroidsData);

            // 3. Load Normalization Stats from DB
            var means = NormalizationHelper.DeserializeArray(entity.NormalizationMeans);
            var stdDevs = NormalizationHelper.DeserializeArray(entity.NormalizationStdDevs);

            if (means.Length == 0 || stdDevs.Length == 0)
            {
                RichTextBoxOutput.AppendText("Error: Model is missing normalization stats. Please retrain.\r\n");
                return;
            }

            // 4. Load RAW Test Data
            var rawTestData = DataLoader.LoadCsv("test_20k_raw_data.csv");

            RichTextBoxOutput.AppendText($"Loaded {rawTestData.Inputs.Count} raw test records.\n");

            // 5. Normalize Test Data (Using SAVED Stats)
            // IMPORTANT: Do NOT re-compute stats on test data. Use the training stats.
            List<double> rawScores = new List<double>();
            List<double> actuals = rawTestData.Targets;

            Task.Run(() =>
            {
                foreach (var rawInput in rawTestData.Inputs)
                {
                    // Normalize single row
                    double[] normInput = NormalizationHelper.NormalizeRow(rawInput, means, stdDevs);

                    // Forward pass
                    rawScores.Add(model.Forward(normInput));
                }

                // 6. Find Optimal Threshold & Metrics
                double bestThreshold = 0.5;
                double bestAccuracy = 0.0;
                EvaluationMetrics bestMetrics = new EvaluationMetrics();

                for (double sweetSpot = 0.05; sweetSpot < 0.95; sweetSpot += 0.05)
                {
                    EvaluationMetrics metrics = MetricsCalculator.Calculate(rawScores, actuals, sweetSpot);
                    if (metrics.Accuracy > bestAccuracy)
                    {
                        bestAccuracy = metrics.Accuracy;
                        bestThreshold = sweetSpot;
                        bestMetrics = metrics;
                    }
                }

                // 7. Update UI
                this.Invoke((MethodInvoker)delegate
                {
                    RichTextBoxOutput.AppendText($"Optimal Threshold: {bestThreshold:F2}\n");
                    RichTextBoxOutput.AppendText(bestMetrics.ToString() + "\r\n");
                });
            });
        }
       
    }
}