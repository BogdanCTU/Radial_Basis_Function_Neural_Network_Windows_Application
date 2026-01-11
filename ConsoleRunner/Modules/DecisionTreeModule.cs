using System;
using System.Linq;
using System.Collections.Generic;
using ML_Project_Windows_App;
using Source;

namespace ConsoleRunner.Modules
{
    public static class DecisionTreeModule
    {
        public static void Run(List<double[]> inputs, List<double> targets, int minSamplesSplit, int maxDepth, string schema)
        {

            // 1. Split Data (Train/Val)
            Console.WriteLine("[2/4] Splitting Data (80% Train, 20% Validation)...");
            
            // Simple shuffle and split
            var rand = new Random(42);
            int n = inputs.Count;
            var indices = Enumerable.Range(0, n).OrderBy(x => rand.Next()).ToList();
            
            int trainCount = (int)(n * 0.8);
            
            var trainInputs = indices.Take(trainCount).Select(i => inputs[i]).ToArray();
            var trainTargets = indices.Take(trainCount).Select(i => targets[i]).ToArray();
            
            var valInputs = indices.Skip(trainCount).Select(i => inputs[i]).ToArray();
            var valTargets = indices.Skip(trainCount).Select(i => targets[i]).ToArray();
            
            Console.WriteLine($" > Train Set: {trainInputs.Length} samples");
            Console.WriteLine($" > Val Set:   {valInputs.Length} samples");

            // 2. Training
            Console.WriteLine($"\n[3/4] Training Decision Tree (MinSamplesSplit={minSamplesSplit}, MaxDepth={maxDepth})...");
            
            var dt = new DecisionTreeRegressor(minSamplesSplit: minSamplesSplit, maxDepth: maxDepth);
            dt.Fit(trainInputs, trainTargets);
            Console.WriteLine(" > Training Complete!");
            
            // 3. Evaluation
            Console.WriteLine("\n[4/4] Evaluating on Validation Set... ");
            
            var predictor = new ScorePredictor(dt, schema);
            
            var predictions = new List<double>();
            foreach (var input in valInputs)
            {
                predictions.Add(predictor.Predict(input));
            }
            
            var metrics = Source.RegressionMetricsCalculator.Calculate(predictions.ToList(), valTargets.ToList());

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(metrics.ToString());
            Console.WriteLine("--------------------------------------------------");
            
            // Show a few examples
            Console.WriteLine("\n Sample Predictions:");
            for(int i=0; i<Math.Min(5, predictions.Count); i++)
            {
                 Console.WriteLine($"   Actual: {valTargets[i]:F2} | Predicted: {predictions[i]:F2}");
            }
        }
    }
}
