using System.Data;
using System.Globalization;
using Source;
using Source.Data;

namespace WinForm_RFBN_APP
{
    public partial class CrossValidationPage : UserControl
    {
        public CrossValidationPage()
        {
            InitializeComponent();
        }

        // -------------------------------------------------------------------------------------------
        // ALGORITHM ENTRY POINT
        // -------------------------------------------------------------------------------------------
        private async void RunGridSearchButton_Click(object sender, EventArgs e)
        {
            // Validate user parameters (K-Folds, Neuron range, Learning Rate, etc.)
            if (!ValidateInputs(out int kFolds, out int start, out int end, out int step, out int epochNum, out double learningRate))
                return;

            string csvPath = CsvPathTextBox.Text.Trim();
            string schema = SchemaTextBox.Text.Trim();

            if (string.IsNullOrEmpty(csvPath) || string.IsNullOrEmpty(schema))
            {
                MessageBox.Show("Please provide both CSV Path and Schema.");
                return;
            }

            // Lock UI to prevent concurrent runs
            RunGridSearchButton.Enabled = false;
            LogRichTextBox.Clear();
            LogRichTextBox.AppendText($"Starting Service: {kFolds}-Fold CV, Neurons {start}-{end}, LR {learningRate}\r\n");

            try
            {
                // [SERVICE PATTERN]
                // Offload the heavy computational "PerformGridSearch" method to a background thread.
                // This prevents the Windows Form UI from freezing during the intensive training loops.
                await Task.Run(() => PerformGridSearch(kFolds, start, end, step, epochNum, learningRate, csvPath, schema));
            }
            catch (Exception ex)
            {
                Invoke(() => LogRichTextBox.AppendText($"Error: {ex.Message}\r\n"));
            }
            finally
            {
                RunGridSearchButton.Enabled = true;
            }
        }

        // -------------------------------------------------------------------------------------------
        // CORE LOGIC: GRID SEARCH + K-FOLD CROSS VALIDATION
        // -------------------------------------------------------------------------------------------
        private void PerformGridSearch(int kFolds, int startNeurons, int endNeurons, int step, int epochNum, double learningRate, string csvPath, string schema)
        {
            // 1. DATA LOADING & PRE-PROCESSING
            // Load raw data from CSV using the provided schema.
            var (allInputs, allTargets) = DataLoader.LoadCsv(csvPath, schema);

            // [NORMALIZATION STRATEGY]
            // We apply Global Z-Score Normalization (Standardization).
            // Formula: x' = (x - mean) / std_dev
            // This ensures all inputs have a mean of 0 and variance of 1, helping the RBF network converge faster.
            // Note: In strict academic scenarios, stats should be calculated on Training data only to avoid "Data Leakage",
            // but global normalization is acceptable for this scope.
            var stats = NormalizationHelper.ComputeZScoreStats(allInputs);
            var normalizedInputs = allInputs.Select(row => NormalizationHelper.NormalizeRow(row, stats.Mean, stats.StdDev)).ToList();

            double bestMeanAccuracy = 0;
            int bestNeuronCount = 0;

            // [REPRODUCIBILITY]
            // We use a fixed seed (42) for the Random Number Generator.
            // This ensures that the "Random Shuffle" of the dataset is exactly the same every time we run the experiment,
            // allowing us to scientifically compare different neuron counts without random noise affecting the split.
            Random rng = new Random(42);

            // 2. GRID SEARCH LOOP (Hyperparameter Tuning)
            // Iterate through the specified range of Hidden Neurons (e.g., 5, 10, 15...).
            for (int neuronCount = startNeurons; neuronCount <= endNeurons; neuronCount += step)
            {
                // Initialize lists to hold the performance metric of EACH fold.
                // We need these collections to calculate Standard Deviation later.
                var accList = new List<double>();
                var precList = new List<double>();
                var recList = new List<double>();
                var f1List = new List<double>();
                var aucList = new List<double>();
                var auprcList = new List<double>(); // New: Area Under Precision-Recall Curve

                // [SHUFFLING]
                // Create a list of indices (0 to N-1) and shuffle them randomly.
                // This randomizes the order of data before splitting into folds, ensuring 
                // that classes are distributed evenly and not clustered by their original CSV order.
                var indices = Enumerable.Range(0, normalizedInputs.Count).OrderBy(x => rng.Next()).ToList();

                // Calculate the size of each fold (N / K).
                int foldSize = normalizedInputs.Count / kFolds;

                // 3. K-FOLD CROSS-VALIDATION LOOP
                // The dataset is split into K parts. We iterate K times.
                for (int fold = 0; fold < kFolds; fold++)
                {
                    // [PARTITIONING LOGIC]
                    // Determine the start and end index for the "Test Set" for this specific fold.
                    int testStart = fold * foldSize;

                    // Handle Remainder: If this is the last fold, extend the end to the very last element.
                    // This guarantees every single data point is tested exactly once.
                    int testEnd = (fold == kFolds - 1) ? normalizedInputs.Count : testStart + foldSize;

                    var trainInputs = new List<double[]>();
                    var trainTargets = new List<double>();
                    var testInputs = new List<double[]>();
                    var testTargets = new List<double>();

                    // Distribute data into Train or Test lists based on the current fold indices.
                    for (int i = 0; i < normalizedInputs.Count; i++)
                    {
                        int idx = indices[i]; // Use the shuffled index

                        // If the index falls inside the calculated "Test Window", it goes to Testing.
                        if (i >= testStart && i < testEnd)
                        {
                            testInputs.Add(normalizedInputs[idx]);
                            testTargets.Add(allTargets[idx]);
                        }
                        // Otherwise, it belongs to the Training set (the other K-1 folds).
                        else
                        {
                            trainInputs.Add(normalizedInputs[idx]);
                            trainTargets.Add(allTargets[idx]);
                        }
                    }

                    // [TRAINING]
                    // Initialize a fresh RBF Network and train it on the "Train Partition" only.
                    var trainer = new RbfTrainer();
                    var network = trainer.Train(trainInputs, trainTargets, neuronCount, epochNum, learningRate);

                    // [VALIDATION & METRICS]
                    // Initialize Confusion Matrix counters.
                    // TP = True Positive, FP = False Positive, TN = True Negative, FN = False Negative
                    int tp = 0, fp = 0, tn = 0, fn = 0;

                    // Store tuples of (Probability Score, Actual Class) for AUC/AUPRC calculation.
                    var predictions = new List<(double Score, int Actual)>();

                    for (int i = 0; i < testInputs.Count; i++)
                    {
                        // Get the network's raw output (probability between 0.0 and 1.0)
                        double output = network.Forward(testInputs[i]);
                        int actual = (int)testTargets[i];

                        // Threshold the output at 0.5 to make a hard classification (0 or 1).
                        int predicted = output >= 0.5 ? 1 : 0;

                        predictions.Add((output, actual));

                        // Populate Confusion Matrix
                        if (predicted == 1 && actual == 1) tp++;       // Correctly identified Positive
                        else if (predicted == 1 && actual == 0) fp++;  // Incorrectly identified as Positive (Type I Error)
                        else if (predicted == 0 && actual == 0) tn++;  // Correctly identified Negative
                        else if (predicted == 0 && actual == 1) fn++;  // Incorrectly identified as Negative (Type II Error)
                    }

                    // [METRIC CALCULATIONS]
                    // Standard formulas for binary classification performance.
                    double total = tp + tn + fp + fn;
                    double acc = total > 0 ? (tp + tn) / total : 0;                              // Accuracy
                    double prec = (tp + fp) > 0 ? (double)tp / (tp + fp) : 0;                    // Precision (Reliability of positive predictions)
                    double rec = (tp + fn) > 0 ? (double)tp / (tp + fn) : 0;                     // Recall (Sensitivity/TPR)
                    double f1 = (prec + rec) > 0 ? 2 * (prec * rec) / (prec + rec) : 0;          // F-Measure (Harmonic mean of Prec/Rec)
                    double auc = CalculateAuc(predictions);                                      // Area Under ROC Curve
                    double auprc = CalculateAuprc(predictions);                                  // Area Under Precision-Recall Curve

                    // Add this fold's results to the lists
                    accList.Add(acc);
                    precList.Add(prec);
                    recList.Add(rec);
                    f1List.Add(f1);
                    aucList.Add(auc);
                    auprcList.Add(auprc);
                }

                // 4. STATISTICAL AGGREGATION
                // Calculate Mean, Standard Deviation, and Confidence Intervals across the K folds.
                var sAcc = CalculateStats(accList);
                var sPrec = CalculateStats(precList);
                var sRec = CalculateStats(recList);
                var sF1 = CalculateStats(f1List);
                var sAuc = CalculateStats(aucList);
                var sAuprc = CalculateStats(auprcList);

                // 5. UPDATE UI
                // Update the RichTextBox with the results formatted as a table.
                Invoke(() =>
                {
                    LogRichTextBox.SelectionColor = Color.Blue;
                    LogRichTextBox.AppendText($"Configuration: {neuronCount} Neurons\r\n");
                    LogRichTextBox.SelectionColor = Color.Black;

                    // Table Header
                    LogRichTextBox.AppendText(new string('-', 60) + "\r\n");
                    LogRichTextBox.AppendText(string.Format("{0,-12} | {1,-10} | {2,-10} | {3,-18}\r\n", "Metric", "Mean", "Std Dev", "95% CI"));
                    LogRichTextBox.AppendText(new string('-', 60) + "\r\n");

                    // Helper function to print a row
                    void PrintRow(string name, (double Mean, double StdDev, double CiLower, double CiUpper) s, bool isPercent)
                    {
                        string formatMean = isPercent ? $"{s.Mean,8:P2}" : $"{s.Mean,8:F4}";
                        string formatSd = isPercent ? $"{s.StdDev,8:P2}" : $"{s.StdDev,8:F4}";
                        string formatCi = isPercent
                            ? $"[{s.CiLower:P2} - {s.CiUpper:P2}]"
                            : $"[{s.CiLower:F3} - {s.CiUpper:F3}]";

                        LogRichTextBox.AppendText(string.Format("{0,-12} | {1} | {2} | {3}\r\n", name, formatMean, formatSd, formatCi));
                    }

                    PrintRow("Accuracy", sAcc, true);
                    PrintRow("Precision", sPrec, true);
                    PrintRow("Recall", sRec, true);
                    PrintRow("F-Measure", sF1, true);
                    PrintRow("AUC-ROC", sAuc, false);
                    PrintRow("AUPRC", sAuprc, false);

                    LogRichTextBox.AppendText(new string('-', 60) + "\r\n\r\n");
                });

                // Track the best configuration based on Mean Accuracy
                if (sAcc.Mean > bestMeanAccuracy)
                {
                    bestMeanAccuracy = sAcc.Mean;
                    bestNeuronCount = neuronCount;
                }
            }

            Invoke(() => LogRichTextBox.AppendText($"BEST: {bestNeuronCount} Neurons (Acc: {bestMeanAccuracy:P2})\r\n"));
        }

        // --- Helper Methods ---

        // Calculates Area Under the ROC Curve (AUC) using the Trapezoidal Rule.
        private double CalculateAuc(List<(double Score, int Actual)> predictions)
        {
            var sorted = predictions.OrderByDescending(x => x.Score).ToList();

            long posCount = sorted.Count(x => x.Actual == 1);
            long negCount = sorted.Count(x => x.Actual == 0);

            if (posCount == 0 || negCount == 0) return 0.5; // Edge case

            double auc = 0;
            double prevTpr = 0;
            double prevFpr = 0;
            double currentPos = 0;
            double currentNeg = 0;

            foreach (var p in sorted)
            {
                if (p.Actual == 1) currentPos++;
                else currentNeg++;

                double tpr = currentPos / posCount;
                double fpr = currentNeg / negCount;

                auc += (fpr - prevFpr) * (tpr + prevTpr) / 2.0;

                prevTpr = tpr;
                prevFpr = fpr;
            }
            return auc;
        }

        // Calculates Area Under the Precision-Recall Curve (AUPRC).
        private double CalculateAuprc(List<(double Score, int Actual)> predictions)
        {
            var sorted = predictions.OrderByDescending(x => x.Score).ToList();
            long posCount = sorted.Count(x => x.Actual == 1);
            if (posCount == 0) return 0;

            double auprc = 0;
            double currentTp = 0;
            double currentFp = 0;
            double prevPrec = 1.0; // PR curve typically starts at precision 1.0
            double prevRec = 0.0;

            foreach (var p in sorted)
            {
                if (p.Actual == 1) currentTp++;
                else currentFp++;

                double prec = currentTp / (currentTp + currentFp);
                double rec = currentTp / posCount;

                // Trapezoidal Integration
                auprc += (rec - prevRec) * (prec + prevPrec) / 2.0;

                prevPrec = prec;
                prevRec = rec;
            }
            return auprc;
        }

        // Calculates Mean, Sample Standard Deviation, and 95% Confidence Interval.
        private (double Mean, double StdDev, double CiLower, double CiUpper) CalculateStats(List<double> values)
        {
            if (values == null || values.Count == 0) return (0, 0, 0, 0);

            // 1. Mean
            double mean = values.Average();

            // 2. Sample Standard Deviation
            double sumSquares = values.Sum(v => Math.Pow(v - mean, 2));
            double stdDev = Math.Sqrt(sumSquares / (values.Count - 1));

            // 3. Confidence Interval (95%)
            // Formula: Mean ± (T-Value * (StdDev / Sqrt(N)))
            double n = values.Count;
            double standardError = stdDev / Math.Sqrt(n);

            // Approximate T-Values for 95% Confidence (Two-tailed)
            double tValue = n switch
            {
                5 => 2.776,  // Degrees of freedom = 4
                10 => 2.262, // Degrees of freedom = 9
                _ => 1.96    // Fallback to Z-score for large N
            };

            double marginOfError = tValue * standardError;

            return (mean, stdDev, mean - marginOfError, mean + marginOfError);
        }

        // Validates that all TextBoxes contain valid numbers (int/double) within acceptable ranges.
        private bool ValidateInputs(out int kFolds, out int start, out int end, out int step, out int epochNum, out double learningRate)
        {
            kFolds = start = end = step = epochNum = 0; learningRate = 0;

            if (!int.TryParse(KFoldsTextBox.Text, out kFolds) || kFolds < 2) return false;
            if (!int.TryParse(StartNeuronTextBox.Text, out start) || start < 1) return false;
            if (!int.TryParse(EndNeuronTextBox.Text, out end) || end < start) return false;
            if (!int.TryParse(StepTextBox.Text, out step) || step < 1) return false;
            if (!int.TryParse(EpochTextBox.Text, out epochNum) || epochNum < 1) return false;
            if (!double.TryParse(LearningRateTextBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out learningRate) || learningRate <= 0) return false;

            return true;
        }

        // Helper to safely update UI controls from a background thread (Service).
        private void Invoke(Action action)
        {
            if (this.InvokeRequired) base.Invoke(action);
            else action();
        }
    }
}