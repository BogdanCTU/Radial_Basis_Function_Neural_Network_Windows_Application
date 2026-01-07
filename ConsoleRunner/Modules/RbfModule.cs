using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using Source;
using Source.Data;

namespace ConsoleRunner.Modules
{
    public static class RbfModule
    {
        public static void Run(List<double[]> inputs, List<double> targets, int hiddenNeurons, int epochs, double learningRate, string testFile, string schema)
        {
            // 1. Normalization
            Console.Write("[2/4] Computing Normalization Stats... ");
            int dim = inputs[0].Length;
            double[] means = new double[dim];
            double[] stdDevs = new double[dim];

            for (int d = 0; d < dim; d++)
            {
                var colValues = inputs.Select(row => row[d]).ToList();
                means[d] = colValues.Average();

                double sumSq = colValues.Sum(v => Math.Pow(v - means[d], 2));
                stdDevs[d] = Math.Sqrt(sumSq / colValues.Count);
                if (stdDevs[d] == 0) stdDevs[d] = 1; // Prevent div/0
            }

            var normalizedInputs = new List<double[]>();
            foreach (var row in inputs)
            {
                double[] normRow = new double[dim];
                for (int d = 0; d < dim; d++) normRow[d] = (row[d] - means[d]) / stdDevs[d];
                normalizedInputs.Add(normRow);
            }
            Console.WriteLine("Done");

            // 2. Training
            Console.WriteLine($"\n[3/4] Training Model ({epochs} epochs, {hiddenNeurons} neurons)...");
            var trainer = new RbfTrainer();
            var network = trainer.Train(normalizedInputs, targets, hiddenNeurons, epochs, learningRate);
            Console.WriteLine(" > Training Complete!");

            // 3. Evaluation
            Console.Write("\n[4/4] Evaluating on Test Set... ");
            
            if (!File.Exists(testFile))
            {
                Console.WriteLine($"\n[Warning] Test file not found at {testFile}. Skipping evaluation.");
            }
            else
            {
                // Load test data using SAME schema
                var (testInputs, testTargets) = DataLoader.LoadCsv(testFile, schema);

                int correct = 0;
                int total = testInputs.Count;

                var classifier = new FoodClassifier(network, means, stdDevs, schema);

                var predictions = new List<double>();
                var actuals = new List<double>();

                for (int i = 0; i < total; i++)
                {
                    double prob = classifier.Predict(testInputs[i]);
                    predictions.Add(prob);
                    actuals.Add(testTargets[i]);
                }

                Console.WriteLine("Done\n");
                
                var metrics = MetricsCalculator.Calculate(predictions, actuals);
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine(metrics.ToString());
                Console.WriteLine("--------------------------------------------------");
            }
        }
    }
}
