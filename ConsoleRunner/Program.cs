using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Source;
using Source.Data;

namespace ConsoleRunner
{
    class Program
    {
        // Default Configuration
        static string _trainFile = Path.Combine("DataSet", "train_80k.csv");
        static string _testFile = Path.Combine("DataSet", "test_20k.csv");
        static string _schema = "PROTEIN;TOTAL_FAT;CARBS;ENERGY;FIBER;SATURATED_FAT;SUGARS;NUTRI_SCORE;CLASSIFICATION";
        
        // RBF Params
        static int _hiddenNeurons = 25;
        static int _epochs = 50;
        static double _learningRate = 0.01;
        
        // Decision Tree Params
        static int _dtTotalSamples = 0; // 0 = All
        static int _minSamplesSplit = 10;
        static int _maxDepth = 10;

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
                        if (_modelType == 2)
                        {
                            // Output a warning about path if not absolute, but let's try to find it
                            // Using absolute path from context for reliability in this environment
                            _trainFile = Path.Combine("DataSet", "ML_Training_Data.csv");
                            _schema = "energy_kj;sugar_g;sat_fat_g;salt_g;fiber_g;protein_g;final_score";
                            Console.WriteLine("\n[Switched to Decision Tree]");
                            Console.WriteLine($"Default Train File set to: {_trainFile}");
                            Console.WriteLine($"Default Schema set to: {_schema}");
                        }
                        else
                        {
                            _trainFile = Path.Combine("DataSet", "train_80k.csv");
                            _schema = "PROTEIN;TOTAL_FAT;CARBS;ENERGY;FIBER;SATURATED_FAT;SUGARS;NUTRI_SCORE;CLASSIFICATION";
                             Console.WriteLine("\n[Switched to RBF Network]");
                             Console.WriteLine("Defaults restored.");
                        }
                        Console.WriteLine("Press Enter...");
                        Console.ReadLine();
                        break;
                    case "1":
                        if (_modelType == 1) // RBF - Epochs
                        {
                            Console.Write($"Enter Epochs (Current: {_epochs}): ");
                            if (int.TryParse(Console.ReadLine(), out int ep)) _epochs = ep;
                        }
                        else // DT - Total Sample Size
                        {
                            Console.Write($"Enter Total Sample Size (0 = All, Current: {_dtTotalSamples}): ");
                            if (int.TryParse(Console.ReadLine(), out int ts)) _dtTotalSamples = ts;
                        }
                        break;
                    case "2":
                        if (_modelType == 1) // RBF - LR
                        {
                            Console.Write($"Enter Learning Rate (Current: {_learningRate}): ");
                            if (double.TryParse(Console.ReadLine(), out double lr)) _learningRate = lr;
                        }
                        else // DT - Min Samples Split
                        {
                            Console.Write($"Enter Min Samples Per Split (Current: {_minSamplesSplit}): ");
                            if (int.TryParse(Console.ReadLine(), out int ms)) _minSamplesSplit = ms;
                        }
                        break;
                    case "3":
                        if (_modelType == 2) 
                        { 
                             // DT - Max Depth
                            Console.Write($"Enter Max Depth (Current: {_maxDepth}): ");
                            if (int.TryParse(Console.ReadLine(), out int md)) _maxDepth = md;
                        }
                        else
                        {
                             // RBF - Hidden Neurons
                             Console.Write($"Enter Hidden Neurons (Current: {_hiddenNeurons}): ");
                             if (int.TryParse(Console.ReadLine(), out int hn)) _hiddenNeurons = hn;
                        }
                        break;
                    case "4":
                        Console.Write($"Enter Training File Path (Current: {_trainFile}): ");
                        string? tf = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(tf)) _trainFile = tf;
                        break;
                    case "5":
                        Console.Write($"Enter Schema (Current: {_schema}): ");
                        string? sch = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(sch)) _schema = sch;
                        break;
                    case "6":
                        RunTraining();
                        Console.WriteLine("\n[Press Enter to return to menu]");
                        Console.ReadLine();
                        break;
                    case "7":
                        return; // Exit
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
            Console.WriteLine($" [0] Model Type:       {(_modelType == 1 ? "RBF Network (Classification)" : "Decision Tree (Regression)")}");
            Console.WriteLine("--------------------------------------------------");
            
            if (_modelType == 1)
            {
                Console.WriteLine($" [1] Epochs:           {_epochs}");
                Console.WriteLine($" [2] Learning Rate:    {_learningRate}");
                Console.WriteLine($" [3] Hidden Neurons:   {_hiddenNeurons}");
            }
            else
            {
                Console.WriteLine($" [1] Total Dataset Size: {(_dtTotalSamples == 0 ? "All" : _dtTotalSamples.ToString())}");
                Console.WriteLine($" [2] Min Samples/Split:  {_minSamplesSplit}");
                Console.WriteLine($" [3] Max Depth:          {_maxDepth}");
            }
            
            Console.WriteLine($" [4] Training File:    {_trainFile}");
            Console.WriteLine($" [5] Schema:           {_schema}");
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine(" [6] RUN TRAINING");
            Console.WriteLine(" [7] EXIT");
            Console.WriteLine("==================================================");
            Console.Write(" >> Select Option: ");
        }

        static void RunTraining()
        {
            Console.WriteLine("\nStarting Training Pipeline...");
            
            if (!File.Exists(_trainFile))
            {
                Console.WriteLine($"[Error] Train file not found at: {Path.GetFullPath(_trainFile)}");
                return;
            }

            try
            {
                // 1. Load Data
                Console.Write("\n[1/4] Loading Training Data... ");
                var (inputs, targets) = DataLoader.LoadCsv(_trainFile, _schema);
                Console.WriteLine($"Done ({inputs.Count} records)");

                if (_modelType == 1) // RBF Network
                {
                    ConsoleRunner.Modules.RbfModule.Run(inputs, targets, _hiddenNeurons, _epochs, _learningRate, _testFile, _schema);
                }
                else // Decision Tree
                {
                   if (_dtTotalSamples > 0 && _dtTotalSamples < inputs.Count)
                   {
                       Console.WriteLine($"[Config] Limiting dataset to {_dtTotalSamples} samples.");
                       inputs = inputs.Take(_dtTotalSamples).ToList();
                       targets = targets.Take(_dtTotalSamples).ToList();
                   }
                   ConsoleRunner.Modules.DecisionTreeModule.Run(inputs, targets, _minSamplesSplit, _maxDepth, _schema);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[Fatal Error] {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
}
