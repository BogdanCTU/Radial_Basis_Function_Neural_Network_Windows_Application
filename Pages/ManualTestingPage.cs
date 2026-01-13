using Source;
using Source.Data;
using System.Data;
using System.Globalization;

namespace WinForm_RFBN_APP
{
    public partial class ManualTestingPage : UserControl
    {

        #region Constructor ---------------------------------------------------------

        public ManualTestingPage()
        {
            InitializeComponent();
        }

        #endregion


        #region Button Commands -----------------------------------------------------

        private void TestFullButton_Click(object sender, EventArgs e)
        {
            string inputRaw = FullInputTextBox.Text.Trim();
            RunPrediction(inputRaw);
        }

        private void TestCustomButton_Click(object sender, EventArgs e)
        {
            // Helper to safely parse text boxes to "0" if empty
            string GetVal(string text) => string.IsNullOrWhiteSpace(text) ? "0" : text.Trim();

            // 1. Gather values from UI
            string energy = GetVal(KiloCaloriesTextBox.Text);
            string protein = GetVal(ProteinTextBox.Text);
            string carb = GetVal(CarbohydratesTextBox.Text);
            string sugar = GetVal(SugarTextBox.Text);
            string totalFat = GetVal(TotalFatTextBox.Text);
            string satFat = GetVal(SaturatedFatTextBox.Text);
            string fiber = GetVal(FiberTextBox.Text);
            string salt = GetVal(SaltTextBox.Text);

            // 2. Construct string in the requested order:
            // "energy_kcal;protein_g;carbohydrate_g;sugar_g;total_fat_g;sat_fat_g;fiber_g;salt_g"
            string inputRaw = $"{energy};{protein};{carb};{sugar};{totalFat};{satFat};{fiber};{salt}";

            RunPrediction(inputRaw);
        }

        #endregion


        #region Logics --------------------------------------------------------------

        private void RunPrediction(string inputString)
        {
            if (string.IsNullOrEmpty(inputString)) return;

            // 1. Determine Active Model
            var modelInfo = ModelRepository.GetLatestModelInfo();
            if (modelInfo == null)
            {
                RichTextBoxOutput.AppendText("Model not found. Please train a model first.\n");
                return;
            }

            string modelName = modelInfo.Value.Name;
            string modelType = modelInfo.Value.Type;

             // 2. Parse User Input (Raw numbers)
            double[] rawInput;
            try
            {
                 // Handle spaces/commas if user typed nicely, but splitting by ; is default
                rawInput = inputString.Split(';')
                    .Select(val => double.Parse(val.Trim(), CultureInfo.InvariantCulture))
                    .ToArray();

                // Updated validation to expect 8 values based on the new order
                if (rawInput.Length != 8)
                {
                    MessageBox.Show("Expected 8 values (Energy, Protein, Carbs, Sugar, TotalFat, SatFat, Fiber, Salt).", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid number format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Predict & Output
            RichTextBoxOutput.AppendText("--------------------------------------------------\n");
            RichTextBoxOutput.AppendText($"Active Model: {modelName} ({modelType})\n");
            RichTextBoxOutput.AppendText($"Input: {inputString}\n");

            if (modelType == "RBF")
            {
                var classifier = ModelRepository.LoadClassifier(modelName);
                if (classifier != null)
                {
                    if (rawInput.Length != 7) 
                    {
                         RichTextBoxOutput.AppendText($"[Warning] RBF expects 7 inputs, got {rawInput.Length}.\n");
                    }
                    
                    double score = classifier.Predict(rawInput);
                    string label = score >= 0.5 ? "HEALTHY (1)" : "UNHEALTHY (0)";
                    RichTextBoxOutput.AppendText($"Score: {score:F4}\n");
                    RichTextBoxOutput.AppendText($"Prediction: {label}\n");
                }
            }
            else // DT
            {
                var predictor = ModelRepository.LoadScorePredictor(modelName);
                if (predictor != null)
                {
                    if (rawInput.Length != 6) 
                    {
                         RichTextBoxOutput.AppendText($"[Warning] DT expects 6 inputs, got {rawInput.Length}.\n");
                    }

                    double score = predictor.Predict(rawInput);
                    RichTextBoxOutput.AppendText($"Predicted Score: {score:F4}\n");
                }
            }
        }

        #endregion

    }
}