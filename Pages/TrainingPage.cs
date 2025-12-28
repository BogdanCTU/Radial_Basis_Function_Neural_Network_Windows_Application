using Source;
using Source.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            // Input data from TextBoxes
            bool successRead true;
            int hiddenNeurons, epochs, learningRate;
            successRead |= int.TryParse(HiddenNeuronsTextBox.Text.ToString(), out hiddenNeurons);
            successRead |= int.TryParse(EpochsTextBox.Text.ToString(), out epochs);
            successRead |= int.TryParse(LearningRateTextBox.Text.ToString(), out learningRate);

            // 25 Hidden Neurons, 100 Epochs, 0.01 Learning Rate
            if (!successRead)
            {
                hiddenNeurons = 25;
                epochs        = 100;
                learningRate  = 0.01;
            }

            var trainer = new RbfTrainer();

            // Run on background thread to keep UI responsive
            RbfNetwork model = await Task.Run(() =>
            {
                return trainer.Train(data.Inputs, data.Targets, hiddenNeurons, epochs, learningRate);
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
            var lines = File.ReadAllLines(filePath).Skip(1);
            var inputs = new List<double[]>();
            var targets = new List<double>();

            foreach (var line in lines)
            {
                var parts = line.Split(';');
                double[] rowInput = new double[8];
                for (int i = 0; i < 8; i++)
                {
                    // FIX: Use CultureInfo.InvariantCulture to handle "." decimals correctly
                    rowInput[i] = double.Parse(parts[i], CultureInfo.InvariantCulture);
                }

                inputs.Add(rowInput);
                targets.Add(double.Parse(parts[8], CultureInfo.InvariantCulture));
            }

            return (inputs, targets);
        }

        #endregion

    }
}
