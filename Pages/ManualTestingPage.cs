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

            // 1. Load the Wrapper
            // Note: In a real app, you might cache this field so you don't reload DB on every click.
            FoodClassifier classifier = ModelRepository.LoadClassifier("FoodClassifier_V1");

            if (classifier == null)
            {
                RichTextBoxOutput.AppendText("Model not found. Please train the model first.\r\n");
                return;
            }

            // 2. Parse User Input (Raw numbers)
            double[] rawInput;
            try
            {
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

            // 3. Predict
            // The classifier handles normalization internally now!
            double score = classifier.Predict(rawInput);

            string label = score >= 0.5 ? "HEALTHY (1)" : "UNHEALTHY (0)";

            // 4. Output
            RichTextBoxOutput.AppendText("--------------------------------------------------\n");
            RichTextBoxOutput.AppendText($"Input: {inputString}\n");
            RichTextBoxOutput.AppendText($"Score: {score:F4}\n");
            RichTextBoxOutput.AppendText($"Prediction: {label}\n");
        }

        #endregion

    }
}