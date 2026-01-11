using System;
using System.Collections.Generic;
using System.Linq;

namespace ML_Project_Windows_App
{
    public class Node
    {
        public int? FeatureIndex { get; set; }
        public double? Threshold { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public double? Value { get; set; }

        public Node(int? featureIndex = null, double? threshold = null, Node left = null, Node right = null, double? value = null)
        {
            FeatureIndex = featureIndex;
            Threshold = threshold;
            Left = left;
            Right = right;
            Value = value;
        }
    }

    public class DecisionTreeRegressor
    {
        public int MinSamplesSplit { get; set; }
        public int MaxDepth { get; set; }
        public Node Root { get; set; }

        public DecisionTreeRegressor(int minSamplesSplit = 2, int maxDepth = 2)
        {
            MinSamplesSplit = minSamplesSplit;
            MaxDepth = maxDepth;
            Root = null;
        }

        /// <summary>
        /// Fits the Decision Tree model to the training data.
        /// </summary>
        /// <param name="X">Input features.</param>
        /// <param name="y">Target values.</param>
        public void Fit(double[][] X, double[] y)
        {
            // Ensure inputs are valid
            if (X.Length == 0 || X.Length != y.Length)
                throw new ArgumentException("Input data X and target y must not be empty and must have the same length.");

            Root = BuildTree(X, y, 0);
        }

        /// <summary>
        /// Predicts target values for the given input samples.
        /// </summary>
        public double[] Predict(double[][] X)
        {
            var predictions = new double[X.Length];
            for (int i = 0; i < X.Length; i++)
            {
                predictions[i] = MakePrediction(X[i], Root);
            }
            return predictions;
        }

        public double PredictSingle(double[] x)
        {
            return MakePrediction(x, Root);
        }

        private double MakePrediction(double[] x, Node node)
        {
            if (node.Value.HasValue)
            {
                return node.Value.Value;
            }

            if (x[node.FeatureIndex.Value] <= node.Threshold.Value)
            {
                return MakePrediction(x, node.Left);
            }
            else
            {
                return MakePrediction(x, node.Right);
            }
        }

        /// <summary>
        /// Recursively builds the decision tree by finding the best split at each node.
        /// </summary>
        private Node BuildTree(double[][] X, double[] y, int currDepth)
        {
            int numSamples = X.Length;
            int numFeatures = X[0].Length;

            // Stopping criteria: Minimum samples for split or Maximum depth reached
            if (numSamples >= MinSamplesSplit && currDepth < MaxDepth)
            {
                var bestSplit = GetBestSplit(X, y, numFeatures);

                // If valid split found (positive variance reduction), recurse
                if (bestSplit.VarReduction > 0)
                {
                    var leftSubtree = BuildTree(bestSplit.XLeft, bestSplit.yLeft, currDepth + 1);
                    var rightSubtree = BuildTree(bestSplit.XRight, bestSplit.yRight, currDepth + 1);

                    return new Node(bestSplit.FeatureIndex, bestSplit.Threshold, leftSubtree, rightSubtree);
                }
            }

            // Leaf node: Return mean of current target values
            double leafValue = y.Average();
            return new Node(value: leafValue);
        }

        /// <summary>
        /// Find the optimal feature and threshold to split the data.
        /// 
        /// Objective:
        /// Minimize the Cost Function (Weighted MSE):
        /// J(j, s) = (N_left / N_total) * MSE_left + (N_right / N_total) * MSE_right
        /// 
        /// Mathematical Equivalence:
        /// MSE(S) = SSE(S) / N  (Mean Squared Error = Sum of Squared Errors / Count)
        /// where SSE(S) = Sum( (y - mean_y)^2 )
        /// 
        /// Substituting into J(j, s):
        /// J(j, s) = (N_left / N) * (SSE_left / N_left) + (N_right / N) * (SSE_right / N_right)
        /// J(j, s) = (1 / N) * (SSE_left + SSE_right)
        /// 
        /// Since N is constant for all splits, minimizing Weighted MSE is mathematically 
        /// identical to minimizing the Total Sum of Squared Errors (SSE_left + SSE_right).
        /// </summary>
        private SplitResult GetBestSplit(double[][] X, double[] y, int numFeatures)
        {
            var bestSplit = new SplitResult
            {
                VarReduction = double.NegativeInfinity
            };

            int nSamples = y.Length;

            // Pre-calculation for Parent Node
            // SSE_parent = Sum(y^2) - (Sum(y)^2)/n
            double sumYParent = y.Sum();
            double sumSqYParent = y.Sum(v => v * v);
            double sseParent = sumSqYParent - (sumYParent * sumYParent) / nSamples;

            // Optimize by looping through features
            for (int featureIndex = 0; featureIndex < numFeatures; featureIndex++)
            {
                // 1. Sort data by current feature
                // Storing indices to avoid copying large arrays repeatedly during sort
                var sortedIndices = Enumerable.Range(0, nSamples)
                                              .OrderBy(i => X[i][featureIndex])
                                              .ToArray();

                var ySorted = new double[nSamples];
                var xSorted = new double[nSamples];
                for (int i = 0; i < nSamples; i++)
                {
                    ySorted[i] = y[sortedIndices[i]];
                    xSorted[i] = X[sortedIndices[i]][featureIndex];
                }

                // 2. Compute Cumulative Sums (Vectorized prefix sums)
                // These allow evaluating ALL possible splits in O(1) time each
                double[] sumYLeft = new double[nSamples];
                double[] sumSqYLeft = new double[nSamples];
                
                double currSumY = 0;
                double currSumSqY = 0;

                for (int i = 0; i < nSamples; i++)
                {
                    currSumY += ySorted[i];
                    currSumSqY += ySorted[i] * ySorted[i];
                    sumYLeft[i] = currSumY;
                    sumSqYLeft[i] = currSumSqY;
                }

                // Iterate through split points
                // nLeft goes from 1 to nSamples - 1
                for (int i = 0; i < nSamples - 1; i++) // i is index of last element in left split
                {
                    // 3. Identify Valid Split Points
                    // - Must split between distinct values (x[i] < x[i+1]) to avoid splitting same values
                    if (xSorted[i] == xSorted[i + 1])
                        continue;

                    int nLeft = i + 1;
                    int nRight = nSamples - nLeft;

                    // Left Child Stats (0 to i)
                    double sYL = sumYLeft[i];
                    double sSqL = sumSqYLeft[i];

                    // Right Child Stats (Totals - Left)
                    double sYR = sumYParent - sYL;
                    double sSqR = sumSqYParent - sSqL; 

                    // 5. Calculate Cost (Total Sum of Squared Errors)
                    // Formula: SSE = Sum(y^2) - (Sum(y)^2) / n
                    double sseLeft = sSqL - (sYL * sYL) / nLeft;
                    double sseRight = sSqR - (sYR * sYR) / nRight;

                    double totalSse = sseLeft + sseRight;

                    // Calculate Variance Reduction (Gain)
                    // Gain = SSE_parent - (SSE_left + SSE_right)
                    double gain = sseParent - totalSse;

                    if (gain > bestSplit.VarReduction)
                    {
                        bestSplit.VarReduction = gain;
                        bestSplit.FeatureIndex = featureIndex;
                        bestSplit.Threshold = xSorted[i];
                    }
                }
            }
            
            // 7. Apply the Best Split Found (if any)
            // Note: We only construct the datasets AFTER finding the global best to save memory/time
            if (bestSplit.FeatureIndex != null)
            {
                int fIdx = bestSplit.FeatureIndex.Value;
                double threshold = bestSplit.Threshold.Value;
                
                var xLeftList = new List<double[]>();
                var yLeftList = new List<double>();
                var xRightList = new List<double[]>();
                var yRightList = new List<double>();

                for (int i = 0; i < nSamples; i++)
                {
                   if (X[i][fIdx] <= threshold)
                   {
                       xLeftList.Add(X[i]);
                       yLeftList.Add(y[i]);
                   }
                   else
                   {
                       xRightList.Add(X[i]);
                       yRightList.Add(y[i]);
                   }
                }
                
                bestSplit.XLeft = xLeftList.ToArray();
                bestSplit.yLeft = yLeftList.ToArray();
                bestSplit.XRight = xRightList.ToArray();
                bestSplit.yRight = yRightList.ToArray();
            }

            return bestSplit;
        }


        private class SplitResult
        {
            public int? FeatureIndex { get; set; }
            public double? Threshold { get; set; }
            public double VarReduction { get; set; }
            public double[][] XLeft { get; set; }
            public double[] yLeft { get; set; }
            public double[][] XRight { get; set; }
            public double[] yRight { get; set; }
        }
    }
}
