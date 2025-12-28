using Source;
using Source.Data;
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
    public partial class TrainingPage : UserControl
    {
        public TrainingPage()
        {
            InitializeComponent();
        }

        private async void TrainButton_Click(object sender, EventArgs e)
        {
            var data = LoadCsv("train_80k_normalized_data.csv");
            RichTextBoxOutput.AppendText("Data Loaded. Starting Training...\n");

            // Normalize data (Crucial for RBF/K-Means)
            // NOTE: In production, save Min/Max stats to DB to normalize new inputs similarly
            // For brevity, skipping explicit normalization code here, but HIGHLY recommended.

            var trainer = new RbfTrainer();

            // Run on background thread to keep UI responsive
            RbfNetwork model = await Task.Run(() =>
            {
                // 10 Hidden Neurons, 50 Epochs, 0.01 Learning Rate
                return trainer.Train(data.Inputs, data.Targets, 10, 1, 0.01);
            });
            RichTextBoxOutput.AppendText("Training Complete!\n");

            ModelRepository.SaveModel("FoodClassifier_V1", model);
            RichTextBoxOutput.AppendText("Model Saved to Repository.\n");
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
