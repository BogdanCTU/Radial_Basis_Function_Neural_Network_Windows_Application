---

# RBFN Food Quality Classifier (C# WinForms)

A robust **Radial Basis Function Network (RBFN)** built entirely **from scratch** in C# to classify food items as "Healthy" or "Unhealthy" based on nutritional content. This project demonstrates a hybrid training pipeline (K-Means + Gradient Descent) without relying on external ML libraries like TensorFlow or ML.NET for the core algorithm.

<img width="1240" height="570" alt="image" src="https://github.com/user-attachments/assets/95fd469c-fb52-44db-a8d7-18a2ffa66cf5" />

---

## 🚀 Key Features

* **From-Scratch Implementation:**
    *   **RBF Network:** Hybrid training (K-Means + GD) for classification.
    *   **Decision Tree Regressor:** ID3/CART-style recursive splitting algorithm for regression tasks.
* **Explainable AI (LIME):** Implements Local Interpretable Model-agnostic Explanations to visualise feature importance for individual predictions.
* **Hyperparameter Optimization:** Built-in **Grid Search** with Cross-Validation to automatically find the best model parameters (Depth, Split Size, Leaf Size).
* **Dual Interface:**
    *   **WinForms GUI:** User-friendly interface for training and management.
    *   **Console Runner:** Advanced CLI for experimentation, optimization, and batch processing.
* **Dynamic Data Loading:** Capable of parsing CSVs with customizable schemas (column selection) at runtime.
* **Persistent Models:** Trains models and saves weights, stats, and schemas to a local SQLite database (`food_classifier.db`).
* **Data Normalization:** Built-in Z-Score normalization pipeline ensures statistical stability.

---

## 🧠 Model Architecture: RBF Neural Network

The model is a 3-layer feed-forward network tailored for binary classification, implemented from scratch without external ML frameworks.

### 1. Input Layer
Accepts a vector of nutritional features. The dimension is dynamic based on user selection (e.g., Protein, Fat, Sugar, etc.).
* **Preprocessing:** All inputs are Z-Score normalized before entering the network to prevent neuron saturation:
    $$x_{norm} = \frac{x - \mu}{\sigma}$$

### 2. Hidden Layer (RBF Kernels)
Instead of standard dot-product neurons, this layer uses **Gaussian Radial Basis Functions**. Each neuron acts as a local prototype for a specific region of the feature space.
* **Activation Function:** Gaussian
    $$\phi_j(x) = e^{-\frac{||x - \mu_j||^2}{2\sigma_j^2}}$$
    * $x$: Input vector
    * $\mu_j$: Centroid vector (learned via Unsupervised K-Means)
    * $\sigma_j$: Spread/Width (learned via **K-Nearest Neighbors** heuristic)

### 3. Output Layer (Probabilistic Classification)
A single neuron aggregates the RBF activations to produce a probability score (0 to 1).
* **Aggregation:** Linear weighted sum of hidden outputs + bias.
* **Activation Function:** Sigmoid
    $$y = \frac{1}{1 + e^{-(\sum w_j \phi_j(x) + b)}}$$

---

### 🚀 Hybrid Training Strategy

The network is trained in three distinct phases to optimize performance:

1.  **Unsupervised Clustering (Centroids):** Uses **K-Means Clustering** to position RBF centroids ($\mu$) at the centers of data density.
2.  **Statistical Heuristic (Sigmas):** Uses a **K-Nearest Neighbors (KNN)** rule ($k=2$) to calculate the spread ($\sigma$). This ensures smooth overlap between neurons, preventing gaps in the decision boundary.
3.  **Supervised Learning (Weights):** The output weights are trained using **Stochastic Gradient Descent (SGD) with Momentum** ($\alpha=0.9$) to minimize **Binary Cross-Entropy Loss**, ensuring high recall and faster convergence.

---

## 📂 Code Structure & Implementation

The core logic is isolated in the `Source` folder, separating the math from the WinForms UI.

### 🔹 Core ML Engine
* **`RbfNetwork.cs`**: The data structure holding Centroids, Sigmas, and Weights. Contains the `Forward()` method for prediction.
* **`DecisionTree.cs`**: Recursive structure for regression trees, supporting custom split criteria.
* **`RbfTrainer.cs`**: Orchestrates the training pipeline:
    1.  Runs **K-Means** to find centroids ($c$).
    2.  Computes standard deviation for $\sigma$.
    3.  Runs **Gradient Descent** to minimize Squared Error on the output weights.
* **`FoodClassifier.cs`**: A high-level wrapper that combines the `RbfNetwork` with `NormalizationStats`. This ensures that any raw input provided by the user is automatically normalized using the *exact* stats from the training set before prediction.
* **`LimeExplainer.cs`**: Generates local perturbations to explain model predictions (LIME).
* **`GridSearchOptimizer.cs`**: Performs exhaustive search over hyperparameter grids using Cross-Validation.

### 🔹 Data Management
* **`DataLoader.cs`**: Dynamically parses CSV files based on a user-defined schema string (e.g., `"energy_kcal;protein_g;carbohydrate_g;sugar_g;total_fat_g;sat_fat_g;fiber_g;salt_g;is_healthy"`).
* **`NormalizationHelper.cs`**: math utility for computing Means and Standard Deviations (Z-Scores) and serializing them for storage.
* **`ModelRepository.cs`**: Handles SQLite operations using Entity Framework Core to save/load trained models.

### 🔹 Console Runner
* **`Program.cs`**: A CLI entry point for running the application in console mode.

---

<!-- ## 📊 Performance Results

The model was tested on a held-out dataset of **20,000 records** (separate from the 80k training set). The from-scratch implementation achieved near-perfect classification for this specific domain.

| Metric | Score |
| :--- | :--- |
| **Accuracy** | **98.29%** |
| **Precision** | **97.49%** |
| **Recall (Sensitivity)** | **94.79%** |
| **F-Measure** | **96.12%** |
| **AUC-ROC** | **0.9985** |
| **AUPRC** | **0.9949** |

**Confusion Matrix:**
```text
[ TP: 4225 | FP: 109 ]
[ FN: 232  | TN: 15434]

```

--- -->

## 🛠️ Installation & Usage

### Prerequisites
* Visual Studio 2022 (or later)
* .NET 9.0 SDK
* **NuGet Packages:**
    * `MaterialSkin.2` (UI Design)
    * `Microsoft.EntityFrameworkCore.Sqlite`

### Getting Started
1. **Clone the Repository**
```bash
git clone https://github.com/BogdanCTU/Radial_Basis_Function_Neural_Network_Windows_Application.git
```
2. **Build the Solution**
Open `WinForm_RFBN_APP.sln` in Visual Studio and build the project to restore NuGet packages.
3. **Run**
Start the application. The SQLite database `food_classifier.db` will be created automatically.

### How to Train (GUI)
1. Navigate to the **Training Page**.
2. Select **Model Type** (RBF Network or Decision Tree).
3. Enter your desired schema (e.g., `energy_kcal;protein_g;carbohydrate_g;sugar_g;total_fat_g;sat_fat_g;fiber_g;salt_g;is_healthy`).
4. Set Hyperparameters (e.g., 25 Hidden Neurons, 100 Epochs, LR 0.01).
5. Click **Train**.

### How to Use Console Runner
1. Run `dotnet run --project ConsoleRunner/ConsoleRunner.csproj`.
2. Use the menu to switch between **Classification (RBF)** and **Regression (DT)** mode.

---

## 📄 License
Distributed under the MIT License. See `LICENSE` for more information.

---

## 👨‍💻 Authors
**Todoran C. Bogdan**
**Bogdan Purdea**
