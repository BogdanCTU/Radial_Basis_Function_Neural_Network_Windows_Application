using System;
using System.Linq;
using ML_Project_Windows_App;

namespace Source
{
    /// <summary>
    /// Wrapper class for the Decision Tree Regressor model.
    /// This class serves as an interface for making predictions using a trained Decision Tree.
    /// </summary>
    public class ScorePredictor
    {
        // The underlying trained Decision Tree model used for regression predictions.
        private readonly DecisionTreeRegressor _tree;

        /// <summary>
        /// Gets the schema definition for the input data.
        /// This string format helps the UI or other consumers understand the structure 
        /// of the CSV data (e.g., column names and order) expected by this model.
        /// </summary>
        public string InputSchema { get; private set; }

        // Checks if the underlying model has been successfully loaded/initialized.
        public bool IsLoaded => _tree != null;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScorePredictor"/> class.
        /// </summary>
        /// <param name="tree">The trained Decision Tree Regressor instance.</param>
        /// <param name="schema">The CSV header string representing the data schema used during training.</param>
        public ScorePredictor(DecisionTreeRegressor tree, string schema)
        {
            _tree = tree;
            InputSchema = schema;
        }

        /// <summary>
        /// Generates a prediction for a single input vector.
        /// </summary>
        /// <param name="rawInput">The input features array. 
        /// Note: Decision Trees are non-parametric and handle raw data directly, 
        /// so no normalization or scaling (Z-score) is required on this input.</param>
        /// <returns>The predicted score (continuous value) from the decision tree.</returns>
        /// <exception cref="InvalidOperationException">Thrown if the model has not been loaded.</exception>
        public double Predict(double[] rawInput)
        {
            if (_tree == null) throw new InvalidOperationException("Model not loaded.");
            
            // Validation Note:
            // Input validation (checking dimensions against schema) is currently implicitly handled 
            // by the underlying tree or trusted to the caller for performance reasons.
            // Unlike RBF networks, we don't need to preprocess 'rawInput' here.
            
            return _tree.PredictSingle(rawInput);
        }
    }
}
