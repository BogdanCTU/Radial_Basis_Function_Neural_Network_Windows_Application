using Source;
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
            TestManualInput(inputRaw);
        }

        private void TestCustomButton_Click(object sender, EventArgs e)
        {
            // As input schema "PROTEIN;TOTAL_FAT;CARBS;ENERGY;FIBER;SATURATED_FAT;SUGARS;"
            // We add a safety check: if a box is empty/whitespace, treat it as "0"
            string p = string.IsNullOrWhiteSpace(ProteinTextBox.Text) ? "0" : ProteinTextBox.Text.Trim();
            string tf = string.IsNullOrWhiteSpace(TotalFatTextBox.Text) ? "0" : TotalFatTextBox.Text.Trim();
            string c = string.IsNullOrWhiteSpace(CarbohydratesTextBox.Text) ? "0" : CarbohydratesTextBox.Text.Trim();
            string en = string.IsNullOrWhiteSpace(KiloCaloriesTextBox.Text) ? "0" : KiloCaloriesTextBox.Text.Trim();
            string fib = string.IsNullOrWhiteSpace(FiberTextBox.Text) ? "0" : FiberTextBox.Text.Trim();
            string sf = string.IsNullOrWhiteSpace(SaturatedFatTextBox.Text) ? "0" : SaturatedFatTextBox.Text.Trim();
            string s = string.IsNullOrWhiteSpace(SugarTextBox.Text) ? "0" : SugarTextBox.Text.Trim();

            string inputRaw = $"{p};{tf};{c};{en};{fib};{sf};{s}";

            TestManualInput(inputRaw);
        }

        private void TestManualInputNormalized(string inputRaw)
        {
            if (string.IsNullOrEmpty(inputRaw))
            {
                MessageBox.Show("Please enter data in the text box first.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Parse Single Input
            double[] singleInput;
            try
            {
                // Split by semicolon and parse using InvariantCulture to handle dots correctly
                singleInput = inputRaw.Split(';')
                                      .Select(val => double.Parse(val.Trim(), System.Globalization.CultureInfo.InvariantCulture))
                                      .ToArray();

                // Validate Dimension (Should be 7 based on your previous update)
                if (singleInput.Length != 7)
                {
                    MessageBox.Show($"Expected 7 values (Protein, Fat, Carbs, Energy, Fiber, SatFat, Sugars). Got {singleInput.Length}.", "Dimension Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid number format. Please ensure inputs are numbers separated by semicolons (e.g. -0.44;-1.02...)", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Load Model (Same logic as batch test)
            using var db = new Source.Data.AppDbContext();
            var entity = db.TrainedModels
                            .OrderByDescending(m => m.CreatedAt)
                            .FirstOrDefault(m => m.ModelName == "FoodClassifier_V1");

            if (entity == null)
            {
                RichTextBoxOutput.AppendText("No model found. Please train first.\r\n");
                return;
            }

            // Reconstruct Network
            var model = new Source.RbfNetwork(entity.InputCount, entity.HiddenCount, 1);
            model.Bias = entity.Bias;
            model.Weights = entity.WeightsData.Split(';').Select(val => double.Parse(val, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            model.Sigmas = entity.SigmasData.Split(';').Select(val => double.Parse(val, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            model.Centroids = DeserializeCentroids(entity.CentroidsData);

            // 3. Execute Single Prediction
            double score = model.Forward(singleInput);

            // threshold 0.5 is standard for single-shot unless you stored the "bestThreshold" from training
            int predictedClass = score >= 0.5 ? 1 : 0;
            string label = predictedClass == 0 ? "UNHEALTHY (0)" : "HEALTHY (1)";

            // 4. Update UI
            RichTextBoxOutput.AppendText("--------------------------------------------------\n");
            RichTextBoxOutput.AppendText($"Manual Test Input: {inputRaw}\n");
            RichTextBoxOutput.AppendText($"Raw Network Score: {score:F4}\n");
            RichTextBoxOutput.AppendText($"Prediction: {label}\n");
            RichTextBoxOutput.AppendText("--------------------------------------------------\n");
        }

        private void TestManualInput(string inputRaw)
        {
            if (string.IsNullOrEmpty(inputRaw))
            {
                MessageBox.Show("Please enter data in the text box first.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Parse Single Input
            double[] singleInput;
            try
            {
                // Split by semicolon and parse using InvariantCulture to handle dots correctly
                singleInput = inputRaw.Split(';')
                                      .Select(val => double.Parse(val.Trim(), System.Globalization.CultureInfo.InvariantCulture))
                                      .ToArray();

                // Validate Dimension (Should be 7 based on your previous update)
                if (singleInput.Length != 7)
                {
                    MessageBox.Show($"Expected 7 values (Protein, Fat, Carbs, Energy, Fiber, SatFat, Sugars). Got {singleInput.Length}.", "Dimension Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid number format. Please ensure inputs are numbers separated by semicolons (e.g. -0.44;-1.02...)", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 2. Load Model & Stats
            using var db = new Source.Data.AppDbContext();
            var entity = db.TrainedModels
                            .OrderByDescending(m => m.CreatedAt)
                            .FirstOrDefault(m => m.ModelName == "FoodClassifier_V1");

            if (entity == null)
            {
                RichTextBoxOutput.AppendText("Error: No trained model found. Please train the model first.\n");
                return;
            }

            // --- NEW: Load Normalization Stats ---
            var means = NormalizationHelper.DeserializeArray(entity.NormalizationMeans);
            var stdDevs = NormalizationHelper.DeserializeArray(entity.NormalizationStdDevs);

            // Check if stats exist (backward compatibility)
            double[] normalizedInput;
            if (means.Length > 0 && stdDevs.Length > 0)
            {
                // NORMALIZE the user input (Raw -> Z-Score)
                normalizedInput = NormalizationHelper.NormalizeRow(singleInput, means, stdDevs);

                RichTextBoxOutput.AppendText($"Normalized Input: {string.Join(";", normalizedInput.Select(n => n.ToString("F2")))}\n");
            }
            else
            {
                // Fallback if model was trained without saving stats
                normalizedInput = singleInput;
                RichTextBoxOutput.AppendText("WARNING: No normalization stats found in DB. Using raw input.\n");
            }

            // 3. Reconstruct Network
            var model = new RbfNetwork(entity.InputCount, entity.HiddenCount, 1);
            model.Bias = entity.Bias;
            model.Weights = entity.WeightsData.Split(';').Select(val => double.Parse(val, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            model.Sigmas = entity.SigmasData.Split(';').Select(val => double.Parse(val, System.Globalization.CultureInfo.InvariantCulture)).ToArray();
            model.Centroids = DeserializeCentroids(entity.CentroidsData);

            // 4. Execute Prediction using NORMALIZED input
            double score = model.Forward(normalizedInput);

            // Threshold 0.5 is standard for single-shot
            int predictedClass = score >= 0.5 ? 1 : 0;
            string label = predictedClass == 0 ? "Class 0" : "Class 1";

            // 5. Update UI
            RichTextBoxOutput.AppendText("--------------------------------------------------\n");
            RichTextBoxOutput.AppendText($"Manual Test Input: {inputRaw}\n");
            RichTextBoxOutput.AppendText($"Raw Network Score: {score:F4}\n");
            RichTextBoxOutput.AppendText($"Prediction: {label}\n");
            RichTextBoxOutput.AppendText("--------------------------------------------------\n");
        }

        #endregion


        #region Data Access ---------------------------------------------------------

        /// <summary>
        /// LoadCsv
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public (List<double[]> Inputs, List<double> Targets) LoadCsv(string filePath)
        {
            // Read all lines
            var lines = File.ReadAllLines(filePath);

            // Skip header if it exists (assuming header is purely text)
            // If your CSV strictly has no header, remove the .Skip(1)
            var dataLines = lines.Skip(1).Where(l => !string.IsNullOrWhiteSpace(l)).ToList();

            List<double[]> inputs = new List<double[]>();
            List<double> targets = new List<double>();

            if (dataLines.Count == 0) return (inputs, targets);

            // Detect dimension from the first data row
            // We assume the LAST column is the target, so InputDim = TotalColumns - 1
            int totalColumns = dataLines[0].Split(';').Length;
            int inputDim = totalColumns - 1;

            foreach (var line in dataLines)
            {
                var parts = line.Split(';');

                // Safety check to ensure row consistency
                if (parts.Length != totalColumns) continue;

                double[] rowInput = new double[inputDim];

                // Loop dynamically up to inputDim
                for (int i = 0; i < inputDim; i++)
                {
                    if (double.TryParse(parts[i], CultureInfo.InvariantCulture, out double val))
                    {
                        rowInput[i] = val;
                    }
                    else
                    {
                        rowInput[i] = 0.0; // Handle missing/bad data safely
                    }
                }

                // Parse Target (Last Column)
                if (double.TryParse(parts[inputDim], CultureInfo.InvariantCulture, out double target))
                {
                    targets.Add(target);
                }
                else
                {
                    // If target is invalid, you might want to skip this row entirely
                    continue;
                }

                inputs.Add(rowInput);
            }

            return (inputs, targets);
        }

        /// <summary>
        /// DeserializeCentroids
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private double[][] DeserializeCentroids(string data)
        {
            // Split the main string by '|' to get each centroid (row)
            var rows = data.Split('|');
            var result = new double[rows.Length][];

            for (int i = 0; i < rows.Length; i++)
            {
                // Split each row by ',' and parse the doubles safely
                // FIX: Added CultureInfo.InvariantCulture to handle "." decimals correctly
                result[i] = rows[i]
                    .Split(',')
                    .Select(val => double.Parse(val, System.Globalization.CultureInfo.InvariantCulture))
                    .ToArray();
            }

            return result;
        }

        #endregion

    }
}
