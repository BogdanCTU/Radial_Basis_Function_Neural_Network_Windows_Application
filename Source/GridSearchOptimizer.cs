using System;
using System.Collections.Generic;
using System.Linq;
using ML_Project_Windows_App;
using Source;
using Source.Data; // Ensure this namespace exists for CrossValidator

namespace Source
{
    public class GridResult
    {
        public int Depth { get; set; }
        public int Split { get; set; }
        public int Leaf { get; set; }
        public double Score { get; set; }
        public double StdDev { get; set; }

        public override string ToString()
        {
            return $"Depth={Depth}, Split={Split}, Leaf={Leaf} => R2={Score:F4} (+/- {StdDev:F4})";
        }
    }

    public static class GridSearchOptimizer
    {
        public static (int BestDepth, int BestSplit, int BestLeaf, double BestScore) RunGridSearch(
            List<double[]> inputs, 
            List<double> targets,
            int cvFolds,
            int[] depthGrid,
            int[] splitGrid,
            int[] leafGrid)
        {
            var results = new List<GridResult>();
            int total = depthGrid.Length * splitGrid.Length * leafGrid.Length;
            int count = 1;

            Console.WriteLine($"Total Combinations: {total}");
            Console.WriteLine($"Running {cvFolds}-Fold Cross Validation for each combination...\n");

            foreach (var d in depthGrid)
            {
                foreach (var s in splitGrid)
                {
                    foreach (var l in leafGrid)
                    {
                        Console.Write($"[{count}/{total}] Depth={d}, Split={s}, Leaf={l} ... ");

                        var (meanR2, stdDev) = CrossValidator.CrossValidate(
                            inputs,
                            targets,
                            cvFolds,
                            (trainX, trainY, valX) =>
                            {
                                var dt = new DecisionTreeRegressor(s, l, d);
                                dt.Fit(trainX.ToArray(), trainY.ToArray());
                                return dt.Predict(valX.ToArray());
                            }
                        );

                        Console.WriteLine($"R2={meanR2:F4} (+/- {stdDev:F4})");
                        
                        results.Add(new GridResult 
                        { 
                            Depth = d, 
                            Split = s, 
                            Leaf = l, 
                            Score = meanR2, 
                            StdDev = stdDev 
                        });

                        count++;
                    }
                }
            }

            return SelectBestModel(results);
        }

        private static (int, int, int, double) SelectBestModel(List<GridResult> results)
        {
            if (results.Count == 0)
                return (10, 20, 1, 0.0); // Default fallback

            // 1. Identify Ceiling
            double maxScore = results.Max(r => r.Score);
            double tolerance = 0.001;

            Console.WriteLine("\n--- Smart Model Selection (Occam's Razor) ---");
            Console.WriteLine($"Max Score: {maxScore:F4}");
            Console.WriteLine($"Tolerance: {tolerance}");

            // 2. Filter Acceptable Models
            var acceptable = results
                .Where(r => r.Score >= maxScore - tolerance)
                .ToList();

            Console.WriteLine($"Candidates within tolerance: {acceptable.Count}");

            // 3. Prioritize Simplicity
            // Notebook Logic:
            // sort_values(by=['depth', 'leaf', 'split'], ascending=[True, False, False])
            var bestModel = acceptable
                .OrderBy(r => r.Depth)            // Minimize Depth (Simpler)
                .ThenByDescending(r => r.Leaf)    // Maximize Leaf Size (Robustness)
                .ThenByDescending(r => r.Split)   // Maximize Split Size (Robustness)
                .First();

            Console.WriteLine($"Selected Model: {bestModel}");
            
            if (bestModel.Score < maxScore)
            {
                Console.WriteLine($"Frequency Note: Selected simpler model over max score (Diff: {maxScore - bestModel.Score:F5})");
            }

            return (bestModel.Depth, bestModel.Split, bestModel.Leaf, bestModel.Score);
        }
    }
}
