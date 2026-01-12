using System;
using System.Collections.Generic;
using System.Linq;

namespace Source
{
    public static class CrossValidator
    {
        /// <summary>
        /// Performs K-Fold Cross Validation.
        /// </summary>
        /// <param name="inputs">Full dataset inputs.</param>
        /// <param name="targets">Full dataset targets.</param>
        /// <param name="k">Number of folds (e.g., 5).</param>
        /// <param name="trainAndPredict">
        /// A function that takes (trainInputs, trainTargets, valInputs) and returns predicted values for the validation set.
        /// </param>
        /// <returns>Tuple containing Mean R2 Score and Standard Deviation of R2 Scores.</returns>
        public static (double MeanR2, double StdDevR2) CrossValidate(
            List<double[]> inputs, 
            List<double> targets, 
            int k, 
            Func<List<double[]>, List<double>, List<double[]>, double[]> trainAndPredict)
        {
            if (inputs.Count != targets.Count) throw new ArgumentException("Input and Target counts must match.");
            if (k < 2) throw new ArgumentException("K must be at least 2.");

            int n = inputs.Count;
            // Shuffle indices for random folds
            var rand = new Random(42);
            var indices = Enumerable.Range(0, n).OrderBy(x => rand.Next()).ToList();

            var foldSize = n / k;
            var scores = new List<double>();

            for (int i = 0; i < k; i++)
            {
                // Identify validation indices
                int valStart = i * foldSize;
                int valEnd = (i == k - 1) ? n : valStart + foldSize; // Handle remainder in last fold
                
                // Split data using the shuffled indices
                var trainInputs = indices.Where((_, idx) => idx < valStart || idx >= valEnd).Select(idx => inputs[idx]).ToList();
                var trainTargets = indices.Where((_, idx) => idx < valStart || idx >= valEnd).Select(idx => targets[idx]).ToList();
                
                var valInputs = indices.Skip(valStart).Take(valEnd - valStart).Select(idx => inputs[idx]).ToList();
                var valTargets = indices.Skip(valStart).Take(valEnd - valStart).Select(idx => targets[idx]).ToList();


                // Train and Predict on this fold
                try
                {
                    double[] predictions = trainAndPredict(trainInputs, trainTargets, valInputs);
                    
                    // Calculate Metric (R2)
                    var metrics = RegressionMetricsCalculator.Calculate(predictions.ToList(), valTargets);
                    scores.Add(metrics.R2);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[CrossValidator] Error in Fold {i+1}: {ex.Message}");
                    scores.Add(0); // Penalize failure
                }
            }

            double mean = scores.Average();
            double sumSq = scores.Sum(s => Math.Pow(s - mean, 2));
            double stdDev = Math.Sqrt(sumSq / scores.Count);

            return (mean, stdDev);
        }
    }
}
