namespace Source
{

    /// <summary>
    /// Implements a Radial Basis Function (RBF) Neural Network trainer.
    /// The training process is hybrid:
    /// 1. Unsupervised learning (K-Means) to determine RBF centers.
    /// 2. Heuristic method (KNN) to determine RBF widths (sigmas).
    /// 3. Supervised learning (Gradient Descent) to optimize output weights.
    /// </summary>
    public class RbfTrainer
    {

        /// <summary>
        /// Fixed seed ensures deterministic results for debugging and reproducibility.
        /// </summary>
        private Random _rnd = new Random(42);

        /// <summary>
        /// Trains an RBF Network using the specified hyperparameters.
        /// </summary>
        /// <param name="inputs">A list of input vectors (features).</param>
        /// <param name="targets">A list of target values (0.0 to 1.0).</param>
        /// <param name="hiddenNeurons">The number of RBF neurons (centers) to use.</param>
        /// <param name="epochs">The number of passes through the entire training dataset.</param>
        /// <param name="learningRate">The step size for weight updates.</param>
        public RbfNetwork Train(List<double[]> inputs, List<double> targets, int hiddenNeurons, int epochs, double learningRate)
        {
            // Determine the dimensionality of the input data.
            int inputDim = inputs[0].Length;
            var network = new RbfNetwork(inputDim, hiddenNeurons, 1);

            // PHASE 1: Unsupervised Learning (Centroids)
            network.Centroids = ComputeCentroidsKMeans(inputs, hiddenNeurons);

            // PHASE 2: Heuristic Configuration (Sigmas)
            network.Sigmas = ComputeSigmasKNN(network.Centroids, k: 2);

            // PHASE 3: Supervised Learning (Weights)

            // Initialize the weights connecting the hidden layer to the output node.
            network.Weights = new double[hiddenNeurons];

            // Xavier/Glorot Initialization is used to maintain the variance of activations.
            // Formula: Random(-1, 1) * Sqrt(6 / (fan_in + fan_out))
            // This prevents signals from vanishing or exploding during the initial passes.
            double initRange = Math.Sqrt(6.0 / (inputDim + hiddenNeurons));
            for (int i = 0; i < hiddenNeurons; i++)
            {
                network.Weights[i] = (_rnd.NextDouble() * 2 - 1) * initRange;
            }

            network.Bias = 0.0;

            // Momentum Variables Setup
            // Momentum accumulates the gradient of past steps to smooth out the updates and escape local minima.
            double[] weightVelocities = new double[hiddenNeurons];
            double biasVelocity = 0, momentum = 0.9;   // Standard coefficient for momentum

            for (int epoch = 0; epoch < epochs; epoch++)
            {
                // Dynamic Learning Rate Decay (Simulated Annealing)
                // The learning rate decreases over time (1 / (1 + decay * epoch)).
                // This allows large steps initially for exploration and smaller steps later for fine-tuning.
                double currentLr = learningRate * (1.0 / (1.0 + 0.01 * epoch));

                for (int i = 0; i < inputs.Count; i++)
                {
                    // Step 1: Forward Pass 

                    // Calculate the activation of each hidden neuron.
                    double[] hiddenActivations = new double[hiddenNeurons];
                    for (int h = 0; h < hiddenNeurons; h++)
                    {
                        // Calculate Euclidean Distance Squared: ||x - c||^2
                        double distSq = 0;
                        for (int d = 0; d < inputDim; d++)
                        {
                            double diff = inputs[i][d] - network.Centroids[h][d];
                            distSq += diff * diff;
                        }

                        // Apply the Gaussian RBF Kernel: exp( -||x - c||^2 / (2 * sigma^2) )
                        // This output represents the similarity between the input and the centroid.
                        hiddenActivations[h] = Math.Exp(-distSq / (2 * Math.Pow(network.Sigmas[h], 2)));
                    }

                    // Compute the linear combination of hidden activations and weights.
                    double linearSum = network.Bias;
                    for (int h = 0; h < hiddenNeurons; h++)
                    {
                        linearSum += hiddenActivations[h] * network.Weights[h];
                    }

                    // Apply the Sigmoid activation function to squash the output between 0 and 1.
                    double output = 1.0 / (1.0 + Math.Exp(-linearSum));

                    // --- 2. Backward Pass (Gradient Calculation) ---

                    // The gradient is derived based on the Binary Cross-Entropy (Log Loss) function
                    // rather than Mean Squared Error (MSE).
                    // Derivation:
                    // If Loss L = -[y * ln(p) + (1-y) * ln(1-p)] (where y is target, p is output)
                    // and p = sigmoid(z)
                    // Then the derivative dL/dz = p - y.
                    // By defining the error term as (Target - Output), the sign is inverted for gradient ascent/addition.
                    // This creates stronger gradients when the prediction is confident but wrong (e.g., output 0.99 for target 0).
                    double errorGradient = (targets[i] - output);

                    // --- 3. Weight Update with Momentum ---

                    for (int h = 0; h < hiddenNeurons; h++)
                    {
                        // Calculate the delta (change) based on the Chain Rule:
                        // dL/dw = errorGradient * activation
                        double delta = currentLr * errorGradient * hiddenActivations[h];

                        // Apply Momentum: Velocity = delta + (momentum * previous_velocity)
                        weightVelocities[h] = delta + (momentum * weightVelocities[h]);

                        // Update the weight using the calculated velocity
                        network.Weights[h] += weightVelocities[h];
                    }

                    // Update Bias
                    // The bias input is implicitly 1.0, so the gradient is just the errorGradient.
                    double biasDelta = currentLr * errorGradient;
                    biasVelocity = biasDelta + (momentum * biasVelocity);
                    network.Bias += biasVelocity;
                }
            }

            return network;
        }


        // --- Methods ---

        /// <summary>
        /// Computes centroids using the K-Means clustering algorithm.
        /// </summary>
        /// <param name="data">The dataset to cluster.</param>
        /// <param name="k">The desired number of clusters (centroids).</param>
        private double[][] ComputeCentroidsKMeans(List<double[]> data, int k)
        {
            int dim = data[0].Length;
            double[][] centroids = new double[k][];

            // Step 1: Initialization
            for (int i = 0; i < k; i++)
                centroids[i] = (double[])data[_rnd.Next(data.Count)].Clone();

            int[] assignments = new int[data.Count];
            bool changed = true;
            int iter = 0, maxIter = 100;

            // Iterate until convergence (no assignments change) or max iterations reached.
            while (changed && iter < maxIter)
            {
                changed = false;
                iter++;

                // Structures to accumulate sums for the mean calculation
                int[] counts = new int[k];
                double[][] sums = new double[k][];
                for (int c = 0; c < k; c++) sums[c] = new double[dim];

                // Step 2: Assignment
                // Assign each data point to the nearest centroid based on Euclidean distance.
                for (int i = 0; i < data.Count; i++)
                {
                    int bestCluster = 0;
                    double bestDist = double.MaxValue;

                    for (int c = 0; c < k; c++)
                    {
                        double dist = 0;
                        for (int d = 0; d < dim; d++)
                        {
                            double diff = data[i][d] - centroids[c][d];
                            dist += diff * diff;
                        }
                        if (dist < bestDist) { bestDist = dist; bestCluster = c; }
                    }

                    // Check if the cluster assignment has changed
                    if (assignments[i] != bestCluster)
                    {
                        assignments[i] = bestCluster;
                        changed = true;
                    }

                    // Accumulate sum and count for the Update step immediately (single-pass optimization).
                    counts[bestCluster]++;
                    for (int d = 0; d < dim; d++) sums[bestCluster][d] += data[i][d];
                }

                // Step 3: Update
                // Recalculate centroids by taking the mean of all points assigned to the cluster.
                for (int c = 0; c < k; c++)
                {
                    if (counts[c] > 0)
                    {
                        for (int d = 0; d < dim; d++) centroids[c][d] = sums[c][d] / counts[c];
                    }
                    else
                    {
                        // Handle "Dead Cluster" problem:
                        // If a centroid has no assigned points, relocate it to a random data point
                        // to re-enter the competition.
                        centroids[c] = (double[])data[_rnd.Next(data.Count)].Clone();
                    }
                }
            }
            return centroids;
        }

        /// <summary>
        /// Calculates the width (Sigma) for each RBF neuron using a K-Nearest Neighbors heuristic.
        /// </summary>
        /// <param name="centroids">The identified centers of the RBF neurons.</param>
        /// <param name="k">The number of nearest neighbors to average distance over.</param>
        private double[] ComputeSigmasKNN(double[][] centroids, int k)
        {
            int n = centroids.Length;
            int dim = centroids[0].Length;
            double[] sigmas = new double[n];

            if (n == 1) return new double[] { 1.0 };

            for (int i = 0; i < n; i++)
            {
                // Step 1: Calculate distances to all other centroids
                var distances = new List<double>();
                for (int j = 0; j < n; j++)
                {
                    if (i == j) continue;

                    double distSq = 0;
                    for (int d = 0; d < dim; d++)
                    {
                        double diff = centroids[i][d] - centroids[j][d];
                        distSq += diff * diff;
                    }
                    distances.Add(Math.Sqrt(distSq));
                }

                distances.Sort();

                // Step 2: Calculate the average distance to the closest 'k' centroids.
                // This adapts the width to the local density of neurons;
                // dense areas get smaller sigmas (narrower Gaussians), sparse areas get larger sigmas.
                double avgDist = distances.Take(k).Average();

                sigmas[i] = avgDist;

                // Step 3: Safety Clamping
                // Prevent numerical instability or vanishing gradients by restricting
                // sigma to a reasonable range suitable for normalized data (Z-Score).
                if (sigmas[i] < 0.1) sigmas[i] = 0.1;
                if (sigmas[i] > 3.0) sigmas[i] = 3.0;
            }

            return sigmas;
        }
    }

}
