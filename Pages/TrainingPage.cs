using Source;
using Source.Data;

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
            RichTextBoxOutput.AppendText("--------------------------------------------------\n");
            RichTextBoxOutput.AppendText("Starting Training Pipeline...\n");

            // 1. Load RAW Data
            // Assumes file has columns: 0-6 Features, 7 Ignored, 8 Target
            var rawData = DataLoader.LoadCsv("train_80k_raw_data.csv");

            RichTextBoxOutput.AppendText($"Raw Data Loaded: {rawData.Inputs.Count} records.\n");

            // 2. Compute Normalization Stats (Z-Score)
            // We must compute these on the TRAINING set only
            var (means, stdDevs) = NormalizationHelper.ComputeZScoreStats(rawData.Inputs);

            RichTextBoxOutput.AppendText("Normalization Stats Computed (Mean/StdDev).\n");

            // 3. Normalize the Training Data
            var normalizedInputs = new List<double[]>();
            foreach (var row in rawData.Inputs)
            {
                normalizedInputs.Add(NormalizationHelper.NormalizeRow(row, means, stdDevs));
            }

            // 4. Parse UI Parameters
            int hiddenNeurons = 25;
            int epochs = 100;
            double learningRate = 0.01d;

            int.TryParse(HiddenNeuronsTextBox.Text, out hiddenNeurons);
            int.TryParse(EpochsTextBox.Text, out epochs);
            double.TryParse(LearningRateTextBox.Text, out learningRate);

            RichTextBoxOutput.AppendText($"Training with {hiddenNeurons} hidden neurons, {epochs} epochs...\n");

            // 5. Train Model (Using NORMALIZED inputs)
            var trainer = new RbfTrainer();
            RbfNetwork model = await Task.Run(() =>
            {
                return trainer.Train(normalizedInputs, rawData.Targets, hiddenNeurons, epochs, learningRate);
            });

            RichTextBoxOutput.AppendText("Training Complete!\n");

            // 6. Save Model AND Normalization Stats
            string meanStr = NormalizationHelper.SerializeArray(means);
            string stdStr = NormalizationHelper.SerializeArray(stdDevs);

            ModelRepository.SaveModel("FoodClassifier_V1", model, meanStr, stdStr);

            RichTextBoxOutput.AppendText("--------------------------------------------------\n");
            RichTextBoxOutput.AppendText("Model and Normalization Stats Saved to Repository.\n");
        }

        private void CleanButton_Click(object sender, EventArgs e)
        {
            // ... (Existing clean logic)
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
    }
}