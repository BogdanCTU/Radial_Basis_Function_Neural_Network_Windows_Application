# 📘 Technical Deep Dive: RBFN Implementation Details

This document provides a line-by-line breakdown of the **Radial Basis Function Network (RBFN)** implementation used in the Food Quality Classifier. It explains the "Hybrid Training" approach where unsupervised learning (Clustering) is combined with supervised learning (Gradient Descent).

---

## 1. System Architecture Overview

The model consists of three distinct processing layers, implemented across `RbfNetwork.cs` and `RbfTrainer.cs`.

### The Pipeline
1.  **Input Layer:** Receives $N$ features (e.g., Protein, Fat).
2.  **Hidden Layer (RBF):** $M$ neurons. Calculates the similarity between the input and learned "Centroids".
3.  **Output Layer:** 1 neuron. Performs a weighted sum of similarities and applies a Sigmoid activation to output a probability (0-1).

---

## 2. Phase 1: Data Preprocessing (Normalization)
**File:** `NormalizationHelper.cs`

Before any training occurs, data is standardized using **Z-Score Normalization**. This is critical for RBFNs because the "Distance" calculation (Euclidean) is sensitive to feature scaling.

* **Logic:**
    $$x_{new} = \frac{x_{old} - \mu}{\sigma}$$
* **Code Implementation:**
    * **Step 1:** `ComputeZScoreStats` iterates through the training set to calculate the Mean ($\mu$) and Population Standard Deviation ($\sigma$) for every column.
    * **Step 2:** `NormalizeRow` applies these stats to every input vector.
    * **Safety:** The code includes a check `if (stdDev[i] == 0) stdDev[i] = 1.0;` to prevent division-by-zero errors if a feature column is constant.

---

## 3. Phase 2: Unsupervised Learning (Centroids)
**File:** `RbfTrainer.cs` -> `ComputeCentroidsKMeans()`

The Hidden Layer does not use weights in the traditional sense. Instead, it uses **Centroids** ($c$)—prototypical points in the N-dimensional space that represent clusters of data.

* **Algorithm:** K-Means Clustering.
* **Why Unsupervised?** We do not look at the target labels (Healthy/Unhealthy) here. We only look at the structure of the input data.

**The Code Logic:**
1.  **Initialization:** Randomly selects $K$ data points from the training set to act as initial centroids.
2.  **Assignment Loop:**
    * Iterates through every training sample.
    * Calculates Euclidean distance to all $K$ centroids.
    * Assigns the sample to the *nearest* centroid.
3.  **Update Loop:**
    * Calculates the average position (Mean) of all samples assigned to a specific centroid.
    * Moves the centroid to this new mean.
4.  **Convergence:** Repeats until centroids stop moving or `maxIter` (100) is reached.

---

## 4. Phase 3: Heuristic Parameter (Sigmas)
**File:** `RbfTrainer.cs` -> `ComputeSigmas()`

Each hidden neuron has a "width" or "spread" ($\sigma$) that determines its sensitivity.

* **Algorithm:** Global Average Distance.
* **The Code Logic:**
    1.  Calculates the distance between **every pair** of learned centroids.
    2.  Computes the average of these distances (`avgDist`).
    3.  Sets the $\sigma$ for *all* neurons to `avgDist * 1.0`.
* **Effect:** This ensures the Gaussian curves overlap sufficiently to cover the input space, but aren't so wide that they lose discrimination power.

---

## 5. Phase 4: Supervised Learning (Weights)
**File:** `RbfTrainer.cs` -> `Train()`

Now that the Hidden Layer (Centroids & Sigmas) is frozen, we train the Output Layer. This is mathematically equivalent to **Logistic Regression** on the output of the hidden layer.

* **Algorithm:** Gradient Descent (Stochastic).
* **Activation Function:** Sigmoid (Logistic).
    $$Output = \frac{1}{1 + e^{-x}}$$

**The Code Logic (Step-by-Step):**

### A. Forward Pass
For each input sample $x$:
1.  **RBF Activation:** Calculate how close $x$ is to each centroid.
    ```csharp
    // Gaussian Kernel Formula
    double diff = inputs[i][d] - network.Centroids[h][d];
    distSq += diff * diff;
    hiddenActivations[h] = Math.Exp(-distSq / (2 * Math.Pow(network.Sigmas[h], 2)));
    ```
2.  **Linear Sum:**
    ```csharp
    double linearSum = network.Bias;
    for (int h = 0; h < hiddenNeurons; h++)
        linearSum += hiddenActivations[h] * network.Weights[h];
    ```
3.  **Output Generation:**
    ```csharp
    double output = 1.0 / (1.0 + Math.Exp(-linearSum));
    ```

### B. Backward Pass (Weight Update)
We adjust the weights to minimize the error between `output` and `target`.

1.  **Calculate Gradient:**
    We use the derivative of the Sigmoid function combined with the Error.
    ```csharp
    double error = targets[i] - output;
    // Gradient = Error * Derivative of Sigmoid
    double gradient = error * output * (1.0 - output);
    ```
2.  **Update Weights:**
    Move weights in the direction of the gradient, scaled by `learningRate`.
    ```csharp
    network.Weights[h] += learningRate * gradient * hiddenActivations[h];
    ```
    *Note: The bias is updated similarly, treating its input as 1.0.*

---

## 6. Inference (Prediction)
**File:** `FoodClassifier.cs`

This class wraps the trained network to ensure safety and consistency during production/testing.

**The Workflow:**
1.  **Input:** User provides a raw array (e.g., `[7.4, 25.9, ...]`).
2.  **Schema Check:** The wrapper verifies the input length matches the `InputSchema` saved in the database.
3.  **Normalize:** It applies `(Raw - Mean) / StdDev` using the *saved training statistics*.
4.  **Forward:** Calls `_network.Forward()` on the normalized data.
5.  **Result:** Returns the probability (0.0 to 1.0).

---
