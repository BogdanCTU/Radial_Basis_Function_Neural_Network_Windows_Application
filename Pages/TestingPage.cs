using Source;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            // 1. Load the trained model from SQLite
            var model = Source.Data.ModelRepository.LoadModel("FoodClassifier_V1");
            if (model == null)
            {
                RichTextBoxOutput.AppendText("No trained model found. Please train first...\r\n");
                return;
            }

            // 2. Load Testing Data (using the same CSV parser helper as before)
            // NOTE: If you implemented Normalization during training, you MUST apply
            // the SAME saved Min/Max values to this test set here.
            var testData = LoadCsv("test_20k_normalized_data.csv");
            RichTextBoxOutput.AppendText("Test Data Loaded. Running Evaluation...\r\n");

            // 3. Run Batch Prediction
            List<double> rawScores = new List<double>();
            List<double> actuals = testData.Targets;

            RichTextBoxOutput.AppendText("Evaluation Started in Background...\r\n");
            // Run on background thread to prevent UI freezing with 20k records
            Task.Run(() =>
            {
                foreach (var input in testData.Inputs)
                {
                    // Get raw continuous output (e.g., 0.85, 0.12) instead of class (0, 1)
                    double score = model.Forward(input);
                    rawScores.Add(score);
                }

                // 4. Calculate Metrics
                var metrics = MetricsCalculator.Calculate(rawScores, actuals);

                // 5. Update UI (Invoke required since we are in a bg thread)
                this.Invoke((MethodInvoker)delegate
                {
                    Console.WriteLine("-- - Evaluation Results (20k Records) ---\r\n");
                    Console.WriteLine(metrics.ToString());
                    RichTextBoxOutput.AppendText(metrics.ToString() + "\r\n");
                });
            });
            RichTextBoxOutput.AppendText("Evaluation Complete!\r\n");
        }


        #region Data Access ---------------------------------------------------------

        /// <summary>
        /// LoadCsv
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public (List<double[]> Inputs, List<double> Targets) LoadCsv(string filePath)
        {
            var lines = File.ReadAllLines(filePath).Skip(1); // Skip header
            var inputs = new List<double[]>();
            var targets = new List<double>();

            foreach (var line in lines)
            {
                var parts = line.Split(';');
                // Indexes 0-7 are inputs (8 features), Index 8 is Class
                double[] rowInput = new double[8];
                for (int i = 0; i < 8; i++)
                {
                    rowInput[i] = double.Parse(parts[i]);
                }

                inputs.Add(rowInput);
                targets.Add(double.Parse(parts[8]));
            }

            return (inputs, targets);
        }

        #endregion

    }
}
