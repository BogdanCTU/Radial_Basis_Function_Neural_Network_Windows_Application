using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ML_Project_Windows_App;
using Source;
using Source.Data;

namespace ConsoleRunner
{
    class Program
    {
        // Default Configuration
        static string _trainFile = Path.Combine("DataSet", "ML_Training_Data.csv"); 
        static string _rbfSchema = "energy_kj;sugar_g;sat_fat_g;salt_g;fiber_g;protein_g;is_healthy";
        static string _dtSchema = "energy_kj;sugar_g;sat_fat_g;salt_g;fiber_g;protein_g;final_score";
        static string CurrentSchema => _modelType == 1 ? _rbfSchema : _dtSchema;

        // RBF Params
        static int _hiddenNeurons = 25;
        static int _epochs = 50;
        static double _learningRate = 0.01;

        // Decision Tree Params
        static int _dtTotalSamples = 0; // 0 = All
        static int _minSamplesSplit = 20;
        static int _minSamplesLeaf = 1;
        static int _maxDepth = 10;

        // Trained Models
        static FoodClassifier? _trainedRbfNetwork;
        static DecisionTreeRegressor? _trainedDecisionTree;
        static List<double[]>? _trainingInputs;

        static int _modelType = 1; // 1: RBF Network, 2: Decision Tree

        static void Main(string[] args)
        {
            while (true)
            {
                PrintMenu();
                string? input = Console.ReadLine();

                switch (input?.Trim())
                {
                    case "0":
                        _modelType = _modelType == 1 ? 2 : 1;
                        Console.WriteLine($"\n[Switched to {(_modelType == 1 ? "RBF Network" : "Decision Tree")}]");
                        break;
                    case "1":
                        if (_modelType == 1) // RBF - Epochs
                        {
                            Console.Write($"Enter Epochs (Current: {_epochs}): ");
                            if (int.TryParse(Console.ReadLine(), out int ep))
                                _epochs = ep;
                        }
                        else // DT - Total Sample Size
                        {
                            Console.Write(
                                $"Enter Total Sample Size (0 = All, Current: {_dtTotalSamples}): "
                            );
                            if (int.TryParse(Console.ReadLine(), out int ts))
                                _dtTotalSamples = ts;
                        }
                        break;
                    case "2":
                        if (_modelType == 1) // RBF - LR
                        {
                            Console.Write($"Enter Learning Rate (Current: {_learningRate}): ");
                            if (double.TryParse(Console.ReadLine(), out double lr))
                                _learningRate = lr;
                        }
                        else // DT - Min Samples Split
                        {
                            Console.Write(
                                $"Enter Min Samples Per Split (Current: {_minSamplesSplit}): "
                            );
                            if (int.TryParse(Console.ReadLine(), out int ms))
                                _minSamplesSplit = ms;
                        }
                        break;
                    case "3":
                        if (_modelType == 2)
                        {
                            // DT - Min Samples Leaf
                            Console.Write($"Enter Min Samples Per Leaf (Current: {_minSamplesLeaf}): ");
                            if (int.TryParse(Console.ReadLine(), out int ml))
                                _minSamplesLeaf = ml;
                        }
                        else
                        {
                            // RBF - Hidden Neurons
                            Console.Write($"Enter Hidden Neurons (Current: {_hiddenNeurons}): ");
                            if (int.TryParse(Console.ReadLine(), out int hn))
                                _hiddenNeurons = hn;
                        }
                        break;
                    case "4":
                        if (_modelType == 2)
                        {
                            // DT - Max Depth
                            Console.Write($"Enter Max Depth (Current: {_maxDepth}): ");
                            if (int.TryParse(Console.ReadLine(), out int md))
                                _maxDepth = md;
                        }
                        else
                        {
                            // RBF - Training File
                            Console.Write($"Enter Training File Path (Current: {_trainFile}): ");
                            string? tf = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(tf))
                                _trainFile = tf;
                        }
                        break;
                    case "5":
                        if (_modelType == 2)
                        {
                            // DT - Training File
                            Console.Write($"Enter Training File Path (Current: {_trainFile}): ");
                            string? tf = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(tf))
                                _trainFile = tf;
                        }
                        else
                        {
                            // RBF - Schema
                            Console.Write($"Enter Schema (Current: {_rbfSchema}): ");
                            string? sch = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(sch))
                                _rbfSchema = sch;
                        }
                        break;
                    case "6":
                        if (_modelType == 2)
                        {
                            Console.Write($"Enter Schema (Current: {_dtSchema}): ");
                            string? sch = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(sch))
                                _dtSchema = sch;
                        }
                        else
                        {
                             Console.Write($"Enter Schema (Current: {_rbfSchema}): ");
                             string? sch = Console.ReadLine();
                             if (!string.IsNullOrWhiteSpace(sch))
                                 _rbfSchema = sch; 
                        }
                        break;
                        
                    case "7":
                         RunTraining();
                         Console.WriteLine("\n[Press Enter to return to menu]");
                         Console.ReadLine();
                         break;
                    case "8":
                         RunPrediction();
                         Console.WriteLine("\n[Press Enter to return to menu]");
                         Console.ReadLine();
                         break;
                    case "9":
                         RunOptimization();
                         Console.WriteLine("\n[Press Enter to return to menu]");
                         Console.ReadLine();
                         break;
                    case "10":
                         return; // Exit for both
                    default:
                         Console.WriteLine("Invalid option. Press Enter...");
                         Console.ReadLine();
                         break;
                }
            }
        }

        static void PrintMenu()
        {
            Console.Clear();
            Console.WriteLine("==================================================");
            Console.WriteLine("             ML Console Runner");
            Console.WriteLine("==================================================");
            Console.WriteLine(
                $" [0] Model Type:       {(_modelType == 1 ? "RBF Network (Classification)" : "Decision Tree (Regression)")}"
            );
            Console.WriteLine("--------------------------------------------------");

            if (_modelType == 1)
            {
                Console.WriteLine($" [1] Epochs:           {_epochs}");
                Console.WriteLine($" [2] Learning Rate:    {_learningRate}");
                Console.WriteLine($" [3] Hidden Neurons:   {_hiddenNeurons}");
            }
            else
            {
                Console.WriteLine(
                    $" [1] Total Dataset Size: {(_dtTotalSamples == 0 ? "All" : _dtTotalSamples.ToString())}"
                );
                Console.WriteLine($" [2] Min Samples/Split:  {_minSamplesSplit}");
                Console.WriteLine($" [3] Min Samples/Leaf:   {_minSamplesLeaf}");
                Console.WriteLine($" [4] Max Depth:          {_maxDepth}");
            }

            Console.WriteLine($" [5] Training File:    {_trainFile}");
            Console.WriteLine($" [6] Schema:           {CurrentSchema}");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(" [7] RUN TRAINING");
            Console.WriteLine(" [8] PREDICT (Using trained model)");
            Console.WriteLine(" [9] OPTIMIZE (Grid Search)");
            Console.WriteLine(" [10] EXIT");
            Console.WriteLine("==================================================");
            Console.Write(" >> Select Option: ");
        }

        static void RunOptimization()
        {
            if (_modelType == 1)
            {
                Console.WriteLine("Optimization currently implemented for Decision Tree only.");
                return;
            }

            Console.WriteLine("\n--- Hyperparameter Grid Search ---");

            if (!File.Exists(_trainFile))
            {
                Console.WriteLine(
                    $"[Error] Train file not found at: {Path.GetFullPath(_trainFile)}"
                );
                return;
            }

            Console.Write("Loading Data... ");
            
            // Updated logic to use same training source as RunTraining
            string trainSplitPath = Path.Combine("..", "ML_Software_Project", "data", "processed", "train_split.csv");
            List<double[]> inputs;
            List<double> targets;

            if (File.Exists(trainSplitPath))
            {
                 Console.WriteLine($"[Source] Using Pre-Split Data: {trainSplitPath}");
                 var trainData = DataLoader.LoadCsv(trainSplitPath, CurrentSchema);
                 inputs = trainData.Inputs;
                 targets = trainData.Targets;
            }
            else
            {
                 Console.WriteLine($"[Source] Using Main Dataset: {_trainFile} (80% Split)");
                 // Fallback: Load Main & Split manually to emulate training conditions
                 var (allInputs, allTargets) = DataLoader.LoadCsv(_trainFile, CurrentSchema);
                 var rand = new Random(42);
                 int n = allInputs.Count;
                 var indices = Enumerable.Range(0, n).OrderBy(x => rand.Next()).ToList();
                 int trainCount = (int)(n * 0.8);
                 
                 inputs = indices.Take(trainCount).Select(i => allInputs[i]).ToList();
                 targets = indices.Take(trainCount).Select(i => allTargets[i]).ToList();
            }
            
            _trainingInputs = inputs;

            // Optional: limit data size for faster optimization
            if (_dtTotalSamples > 0 && _dtTotalSamples < inputs.Count)
            {
                inputs = inputs.Take(_dtTotalSamples).ToList();
                targets = targets.Take(_dtTotalSamples).ToList();
            }
            Console.WriteLine($"Done ({inputs.Count} records)");

            // Define Grid
            int[] depthGrid = { 15, 20, 30 };
            int[] splitGrid = { 20, 30, 50 };
            int[] leafGrid = { 20, 30, 50 };

            // Run Optimizer
            // Use GridSearchOptimizer to find best parameters
            var (bestDepth, bestSplit, bestLeaf, bestScore) = GridSearchOptimizer.RunGridSearch(
                inputs, 
                targets, 
                5,
                depthGrid, 
                splitGrid, 
                leafGrid
            );

            Console.WriteLine("\n--------------------------------------------------");
            Console.WriteLine($"Optimization Complete!");
            Console.WriteLine($"Selected R2: {bestScore:F4}");
            Console.WriteLine($"Best Params: MaxDepth={bestDepth}, MinSamplesSplit={bestSplit}, MinSamplesLeaf={bestLeaf}");
            Console.WriteLine("--------------------------------------------------");

            Console.Write("Update current configuration with these parameters? (Y/n): ");
            var resp = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(resp) || resp.Trim().ToLower() == "y")
            {
                _maxDepth = bestDepth;
                _minSamplesSplit = bestSplit;
                _minSamplesLeaf = bestLeaf;
                Console.WriteLine("Configuration updated.");
            }
        }

        static void RunTraining()
        {
            Console.WriteLine("\nStarting Training Pipeline...");

            List<double[]> trainInputs;
            List<double> trainTargets;
            List<double[]> valInputs;
            List<double> valTargets;

            // Updated path to point to processed data
            string trainSplitPath = Path.Combine("..", "ML_Software_Project", "data", "processed", "train_split.csv");
            string valSplitPath = Path.Combine("..", "ML_Software_Project", "data", "processed", "val_split.csv");

            string currentSchema = (_modelType == 1) ? _rbfSchema : _dtSchema;
            Console.WriteLine($"[Config] Using Schema: {currentSchema}");

            // Load data from splits if available, otherwise fall back to splitting main dataset
            if (File.Exists(trainSplitPath) && File.Exists(valSplitPath))
            {
                 Console.WriteLine($"[1/4] Loading Pre-Split Data from {trainSplitPath}...");
                 
                 var trainData = DataLoader.LoadCsv(trainSplitPath, currentSchema);
                 var valData = DataLoader.LoadCsv(valSplitPath, currentSchema);
                 
                 trainInputs = trainData.Inputs;
                 trainTargets = trainData.Targets;
                 valInputs = valData.Inputs;
                 valTargets = valData.Targets;
            }
            else
            {
                if (!File.Exists(_trainFile))
                {
                    Console.WriteLine(
                        $"[Error] Dataset file not found at: {Path.GetFullPath(_trainFile)}"
                    );
                    return;
                }

                Console.WriteLine($"[1/4] Loading Main Dataset: {_trainFile}");
                Console.WriteLine("      Fallback to 80/20 Random Split.");

                var (allInputs, allTargets) = DataLoader.LoadCsv(_trainFile, currentSchema);
                
                // Shuffle
                var rand = new Random(42);
                int n = allInputs.Count;
                var indices = Enumerable.Range(0, n).OrderBy(x => rand.Next()).ToList();
                
                int trainCount = (int)(n * 0.8);
                
                trainInputs = indices.Take(trainCount).Select(i => allInputs[i]).ToList();
                trainTargets = indices.Take(trainCount).Select(i => allTargets[i]).ToList();
                
                valInputs = indices.Skip(trainCount).Select(i => allInputs[i]).ToList();
                valTargets = indices.Skip(trainCount).Select(i => allTargets[i]).ToList();
            }

            // Report Sizes
            Console.WriteLine($"      Train: {trainInputs.Count}, Val: {valInputs.Count}");

            try
            {
                if (_modelType == 1) // RBF Network
                {
                    // Update: RbfModule now takes lists, no file paths
                    _trainedRbfNetwork = ConsoleRunner.Modules.RbfModule.Run(
                        trainInputs,
                        trainTargets,
                        valInputs,
                        valTargets,
                        _hiddenNeurons,
                        _epochs,
                        _learningRate,
                        CurrentSchema
                    );
                }
                else // Decision Tree
                {
                    // Limit sample size if configured
                    if (_dtTotalSamples > 0 && _dtTotalSamples < trainInputs.Count)
                    {
                         Console.WriteLine(
                            $"[Config] Limiting training set to {_dtTotalSamples} samples."
                        );
                        trainInputs = trainInputs.Take(_dtTotalSamples).ToList();
                        trainTargets = trainTargets.Take(_dtTotalSamples).ToList();
                    }

                    _trainedDecisionTree = ConsoleRunner.Modules.DecisionTreeModule.Run(
                        trainInputs,
                        trainTargets,
                        valInputs,
                        valTargets,
                        _minSamplesSplit,
                        _minSamplesLeaf,
                        _maxDepth,
                        CurrentSchema
                    );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[Fatal Error] {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }

        static void RunPrediction()
        {
            // Check if model exists
            if (_modelType == 1 && _trainedRbfNetwork == null)
            {
                Console.WriteLine("[Error] No RBF Network trained yet. Please Option 6 first.");
                return;
            }
            if (_modelType == 2 && _trainedDecisionTree == null)
            {
                Console.WriteLine("[Error] No Decision Tree trained yet. Please Option 6 first.");
                return;
            }

            Console.WriteLine("\n--- Manual Prediction ---");
            Console.WriteLine($"Using {(_modelType == 1 ? "RBF Network" : "Decision Tree")}");

            var columns = CurrentSchema.Split(';');
            var featureNames = new List<string>();

            int targetCount = _modelType == 1 ? 2 : 1;
            int featureCount = columns.Length - targetCount;

            for (int i = 0; i < featureCount; i++)
            {
                featureNames.Add(columns[i]);
            }

            double[] userInputs = new double[featureCount];

            for (int i = 0; i < featureCount; i++)
            {
                while (true)
                {
                    Console.Write($"Enter {featureNames[i]}: ");
                    if (double.TryParse(Console.ReadLine(), out double val))
                    {
                        userInputs[i] = val;
                        break;
                    }
                    Console.WriteLine("Invalid number.");
                }
            }

            // Predict
            try
            {
                double result = 0;
                if (_modelType == 1)
                {
                    result = _trainedRbfNetwork!.Predict(userInputs);
                    Console.WriteLine($"\n>> Predicted Value: {result:F4}");
                    Console.WriteLine(
                        $">> Class: {(result >= 0.5 ? "Healthy (1)" : "Unhealthy (0)")}"
                    );
                }
                else
                {
                    var predictions = _trainedDecisionTree!.Predict(new double[][] { userInputs });
                    result = predictions[0];
                    Console.WriteLine($"\n>> Predicted Score: {result:F4}");
                }

                // Explain prediction
                if (_trainingInputs != null && _trainingInputs.Count > 0)
                {
                    Console.WriteLine("\nGenerating LIME Explanation...");
                    var explainer = new LimeExplainer(_trainingInputs);

                    Func<double[][], double[]> predictFn = (batch) =>
                    {
                        var outputs = new double[batch.Length];
                        for (int k = 0; k < batch.Length; k++)
                        {
                            if (_modelType == 1)
                                outputs[k] = _trainedRbfNetwork!.Predict(batch[k]);
                            else
                                outputs[k] = _trainedDecisionTree!.PredictSingle(batch[k]);
                        }
                        return outputs;
                    };

                    // Explain
                    var explanation = explainer.Explain(userInputs, predictFn, numSamples: 500);

                    Console.WriteLine("Top 3 Influential Features:");
                    foreach (var (idx, weight) in explanation.Take(3))
                    {
                        string name =
                            (idx < featureNames.Count) ? featureNames[idx] : $"Feat {idx}";
                        Console.WriteLine($"  {name}: {weight:F4}");
                    }
                }
                else
                {
                    Console.WriteLine("\n(LIME Explanation skipped: Training data not loaded)");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during prediction: {ex.Message}");
            }
        }
    }
}
