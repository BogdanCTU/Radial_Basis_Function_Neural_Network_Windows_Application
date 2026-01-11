namespace Source
{
    ///<summary>
    /// Handles the training process for the RBF Network.
    /// Training Strategy: Hybrid (Unsupervised + Supervised)
    /// - Stage 1 (Unsupervised):</b> Find RBF centers (Centroids) using K-Means clustering;
    /// - Stage 2 (Heuristic):</b> Calculate widths (Sigmas) based on cluster spread;
    /// - Stage 3 (Supervised):</b> Learn output weights via Gradient Descent (Backpropagation).
    ///</summary>
    public class RbfTrainer
    {
        #region Fields --------------------------------------------------------------

        private Random _rnd = new Random();

        #endregion


        #region Public Methods ------------------------------------------------------

        ///<summary>
        /// Trains an RBF Network using the provided training data.
        ///</summary>
        ///<param name="inputs">List of input feature vectors (Normalized).</param>
        ///<param name="targets">List of target labels (0.0 or 1.0).</param>
        ///<param name="hiddenNeurons">Number of RBF neurons (K in K-Means).</param>
        ///<param name="epochs">Number of passes through the training set for the supervised phase.</param>
        ///<param name="learningRate">Initial learning rate for weight updates.</param>
        ///<returns>A trained <see cref="RbfNetwork"/> instance.</returns>
        public RbfNetwork Train(List<double[]> inputs, List<double> targets, int hiddenNeurons, int epochs, double learningRate)
        {
            int inputDim = inputs[0].Length;
            var network = new RbfNetwork(inputDim, hiddenNeurons, 1);

            // ---------------------------------------------------------
            // PHASE 1: Unsupervised Learning - Centroids
            // ---------------------------------------------------------
            // Use K-Means simply to find representative prototypes in the data.
            // These form the "centers" of our RBF neurons.
            network.Centroids = ComputeCentroidsKMeans(inputs, hiddenNeurons);

            // ---------------------------------------------------------
            // PHASE 2: Heuristic - Sigmas
            // ---------------------------------------------------------
            // Calculate how "wide" each neuron should be.
            // A common heuristic is the Root Mean Square distance of points in the cluster.
            //
            // CRITICAL: Since data is Z-Score normalized (mean=0, std=1), values typically range [-3, 3].
            // If Sigma is too large (> 3.0), the Gaussian bell curve becomes almost flat across the entire data range.
            // This causes the neuron to fire ~1.0 for everything, providing no discriminative power (Gradient Vanishing).
            // Hence, clamp Sigma at 3.0.
            network.Sigmas = ComputeSigmas(network.Centroids, inputs);

            // ---------------------------------------------------------
            // PHASE 3: Supervised Learning - Weights (Gradient Descent)
            // ---------------------------------------------------------
            // Now that RBF prototypes are fixed, the network behaves like a linear model (Linear Perceptron)
            // transforming the RBF activations to the output. We optimize this with Gradient Descent.
            
            network.Weights = new double[hiddenNeurons];
            for (int i = 0; i < hiddenNeurons; i++) 
                network.Weights[i] = (_rnd.NextDouble() * 2 - 1) * 0.1; // Init small random weights [-0.1, 0.1]
            network.Bias = 0.0;

            double currentLearningRate = learningRate;

            for (int epoch = 0; epoch < epochs; epoch++)
            {
                // Learning Rate Decay:
                // Helps convergence by taking smaller steps as we get closer to the minimum.
                // Decays by 5% every 10 epochs.
                if (epoch > 0 && epoch % 10 == 0)
                {
                    currentLearningRate *= 0.95;
                }

                double totalError = 0;

                for (int i = 0; i < inputs.Count; i++)
                {
                    // --- Forward Pass ---
                    // Calculate RBF layer activations (Features for the linear layer)
                    double[] hiddenActivations = new double[hiddenNeurons];
                    for (int h = 0; h < hiddenNeurons; h++)
                    {
                        double distSq = 0;
                        for (int d = 0; d < inputDim; d++)
                        {
                            double diff = inputs[i][d] - network.Centroids[h][d];
                            distSq += diff * diff;
                        }
                        // Gaussian activation
                        hiddenActivations[h] = Math.Exp(-distSq / (2 * Math.Pow(network.Sigmas[h], 2)));
                    }

                    // Calculate Output (Sigmoid)
                    double linearSum = network.Bias;
                    for (int h = 0; h < hiddenNeurons; h++) linearSum += hiddenActivations[h] * network.Weights[h];
                    double output = 1.0 / (1.0 + Math.Exp(-linearSum));

                    // --- Backward Pass (Backpropagation) ---
                    // Loss Function: Mean Squared Error (MSE) = 0.5 * (Target - Output)^2
                    // We want to find dError/dWeight.
                    
                    double error = targets[i] - output;
                    totalError += error * error;

                    // Chain Rule:
                    // dError/dWeight = (dError/dOutput) * (dOutput/dLinearSum) * (dLinearSum/dWeight)
                    //
                    // 1. dError/dOutput = -(Target - Output) = -error
                    // 2. dOutput/dLinearSum (Sigmoid Derivative) = Output * (1 - Output)
                    //
                    // Combine 1 & 2 to get the "Gradient" (or Delta) at the output unit:
                    // Gradient = error * Output * (1 - Output)
                    double gradient = error * output * (1.0 - output);

                    // 3. dLinearSum/dWeight = hiddenActivation[h]
                    // Update Rule: W_new = W_old + LearningRate * Gradient * Input(HiddenActivation)
                    
                    for (int h = 0; h < hiddenNeurons; h++)
                    {
                        network.Weights[h] += currentLearningRate * gradient * hiddenActivations[h];
                    }
                    
                    // Update Bias (Input is implicitly 1.0)
                    network.Bias += currentLearningRate * gradient;
                }
            }

            return network;
        }

        #endregion


        #region Private Methods ------------------------------------------------------

        ///<summary>
        /// Performs K-Means clustering to find representative centroids.
        ///</summary>
        ///<param name="data">The dataset.</param>
        ///<param name="k">Number of clusters (centroids).</param>
        ///<returns>Array of centroid vectors.</returns>
        private double[][] ComputeCentroidsKMeans(List<double[]> data, int k)
        {
            int dim = data[0].Length;
            
            // Initialization: Randomly select k data points as initial centroids (Forgy method).
            double[][] centroids = new double[k][];
            for (int i = 0; i < k; i++)
                centroids[i] = (double[])data[_rnd.Next(data.Count)].Clone();

            int[] assignments = new int[data.Count];
            bool changed = true;
            int maxIter = 100;
            int iter = 0;

            // K-Means Algo Loop:
            // 1. Assignment Step: Assign each point to simplest centroid.
            // 2. Update Step: Recalculate centroid as mean of assigned points.
            // Repeat until convergence (no changes) or max iterations.
            while (changed && iter < maxIter)
            {
                changed = false;
                iter++;

                // --- Step 1: Assignment ---
                for (int i = 0; i < data.Count; i++)
                {
                    int bestCluster = -1;
                    double bestDist = double.MaxValue;

                    for (int c = 0; c < k; c++)
                    {
                        double dist = 0;
                        for (int d = 0; d < dim; d++)
                        {
                            double diff = data[i][d] - centroids[c][d];
                            dist += diff * diff;
                        }
                        if (dist < bestDist)
                        {
                            bestDist = dist;
                            bestCluster = c;
                        }
                    }

                    if (assignments[i] != bestCluster)
                    {
                        assignments[i] = bestCluster;
                        changed = true;
                    }
                }

                // --- Step 2: Update Centroids ---
                int[] counts = new int[k];
                double[][] sums = new double[k][];
                for (int c = 0; c < k; c++) sums[c] = new double[dim];

                // Accumulate sums
                for (int i = 0; i < data.Count; i++)
                {
                    int cluster = assignments[i];
                    counts[cluster]++;
                    for (int d = 0; d < dim; d++)
                        sums[cluster][d] += data[i][d];
                }

                // Compute means
                for (int c = 0; c < k; c++)
                {
                    if (counts[c] > 0)
                        for (int d = 0; d < dim; d++)
                            centroids[c][d] = sums[c][d] / counts[c];
                }
            }
            return centroids;
        }

        ///<summary>
        /// Computes the width (Sigma) for each RBF neuron.
        ///</summary>
        ///<param name="centroids">The computed centroids.</param>
        ///<param name="inputs">The dataset.</param>
        ///<returns>Array of sigma values.</returns>
        private double[] ComputeSigmas(double[][] centroids, List<double[]> inputs)
        {
            int k = centroids.Length;
            int dim = centroids[0].Length;
            double[] sigmas = new double[k];

            // 1. Assign every input to its nearest centroid to form clusters
            List<double>[] clusterDistances = new List<double>[k];
            for (int i = 0; i < k; i++) clusterDistances[i] = new List<double>();

            foreach (var row in inputs)
            {
                int bestCentroid = -1;
                double minDistSq = double.MaxValue;

                for (int c = 0; c < k; c++)
                {
                    double distSq = 0;
                    for (int d = 0; d < dim; d++)
                    {
                        double diff = row[d] - centroids[c][d];
                        distSq += diff * diff;
                    }
                    if (distSq < minDistSq)
                    {
                        minDistSq = distSq;
                        bestCentroid = c;
                    }
                }
                // Save the squared distance for variance calculation
                clusterDistances[bestCentroid].Add(minDistSq);
            }

            // 2. Calculate Sigma as the root mean square distance of the cluster
            for (int i = 0; i < k; i++)
            {
                if (clusterDistances[i].Count > 1)
                {
                    double sumSq = clusterDistances[i].Sum();
                    // Sigma = Sqrt(Avg Squared Distance)
                    double calculatedSigma = Math.Sqrt(sumSq / clusterDistances[i].Count);

                    // SAFETY CLAMP: 
                    // In Z-Score space, a Sigma > 3.0 means the neuron is "flat" over the whole data range.
                    // We clamp it to 3.0 to force the neuron to be useful.
                    if (calculatedSigma > 3.0) calculatedSigma = 3.0;

                    // Prevent overfitting (too sharp)
                    if (calculatedSigma < -3.0) calculatedSigma = -3.0;

                    sigmas[i] = calculatedSigma;
                }
                else
                {
                    // Fallback for dead neurons (0 or 1 data point)
                    sigmas[i] = 1.0;
                }
            }

            return sigmas;
        }

        #endregion

    }
}
