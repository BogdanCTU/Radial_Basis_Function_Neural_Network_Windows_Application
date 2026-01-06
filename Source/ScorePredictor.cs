using System;
using System.Linq;
using ML_Project_Windows_App;

namespace Source
{
    /// <summary>
    /// Wrapper class for the Decision Tree Regressor.
    /// </summary>
    public class ScorePredictor
    {
        private readonly DecisionTreeRegressor _tree;

        /// <summary>
        /// Expose the schema so the UI knows how to parse CSVs for this model
        /// </summary>
        public string InputSchema { get; private set; }

        public bool IsLoaded => _tree != null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tree">The trained Decision Tree model</param>
        /// <param name="schema">The CSV schema used for training</param>
        public ScorePredictor(DecisionTreeRegressor tree, string schema)
        {
            _tree = tree;
            InputSchema = schema;
        }

        /// <summary>
        /// Takes RAW (human-readable) input and returns the predicted score.
        /// Decision Trees do not require normalization, so we pass raw input directly.
        /// </summary>
        public double Predict(double[] rawInput)
        {
            if (_tree == null) throw new InvalidOperationException("Model not loaded.");
            
            // Optional: Validation to ensure input length matches schema
            // We assume schema is semicolon separated
            int expectedFeatures = InputSchema.Split(';').Length - 1; // last one is target
            // NOTE: The user might pass input matching exactly feature count, or whole row including target 0
            // But usually Predict is called with just features. 
            // FoodClassifier checks against _means.Length. Here we don't have _means.
            // Let's trust the caller or just check non-zero.
            
            return _tree.PredictSingle(rawInput);
        }
    }
}
