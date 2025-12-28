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

        }

        private void TestCustomButton_Click(object sender, EventArgs e)
        {

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
