using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;
using ML_Project_Windows_App;

namespace Source.Data
{
    public static class ModelRepository
    {

        #region Public Methods ------------------------------------------------------

        ///<summary>
        /// Added 'schema' parameter
        ///</summary>
        ///<param name="name"></param>
        ///<param name="network"></param>
        ///<param name="meanStr"></param>
        ///<param name="stdStr"></param>
        ///<param name="schema"></param>
        public static void SaveModel(string name, RbfNetwork network, string meanStr, string stdStr, string schema)
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();

            var entity = new TrainedModel
            {
                ModelName = name,
                ModelType = "RBF",
                InputCount = network.InputCount,
                HiddenCount = network.HiddenCount,
                Bias = network.Bias,
                WeightsData = string.Join(";", network.Weights),
                SigmasData = string.Join(";", network.Sigmas),
                CentroidsData = SerializeCentroids(network.Centroids),
                NormalizationMeans = meanStr,
                NormalizationStdDevs = stdStr,

                // Save the schema
                InputSchema = schema
            };

            db.TrainedModels.Add(entity);
            db.SaveChanges();
        }

        /// <summary>
        /// Saves a Decision Tree Model
        /// </summary>
        public static void SaveDecisionTree(string name, DecisionTreeRegressor tree, string schema)
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();

            var options = new JsonSerializerOptions { WriteIndented = false };
            string json = JsonSerializer.Serialize(tree, options);

            var entity = new TrainedModel
            {
                ModelName = name,
                ModelType = "DT",
                SerializedData = json,
                InputSchema = schema,
                
                // RBF fields safely ignored or 0
                InputCount = 0,
                HiddenCount = 0,
                Bias = 0
            };

            db.TrainedModels.Add(entity);
            db.SaveChanges();
        }

        public static FoodClassifier LoadClassifier(string name)
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();

            var entity = db.TrainedModels
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefault(m => m.ModelName == name && m.ModelType == "RBF");

            if (entity == null) return null;

            var network = new RbfNetwork(entity.InputCount, entity.HiddenCount, 1);
            network.Bias = entity.Bias;
            network.Weights = entity.WeightsData.Split(';').Select(double.Parse).ToArray();
            network.Sigmas = entity.SigmasData.Split(';').Select(double.Parse).ToArray();
            network.Centroids = DeserializeCentroids(entity.CentroidsData);

            var means = NormalizationHelper.DeserializeArray(entity.NormalizationMeans);
            var stdDevs = NormalizationHelper.DeserializeArray(entity.NormalizationStdDevs);

            // Pass the loaded schema to the wrapper
            // Fallback to empty string if null (for backward compatibility with old DBs)
            string schema = entity.InputSchema ?? string.Empty;

            return new FoodClassifier(network, means, stdDevs, schema);
        }

        public static ScorePredictor LoadScorePredictor(string name)
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();

             var entity = db.TrainedModels
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefault(m => m.ModelName == name && m.ModelType == "DT");

            if (entity == null) return null;

            if (string.IsNullOrEmpty(entity.SerializedData)) return null;

            try 
            {
                var tree = JsonSerializer.Deserialize<DecisionTreeRegressor>(entity.SerializedData);
                string schema = entity.InputSchema ?? string.Empty;
                return new ScorePredictor(tree, schema);
            }
            catch
            {
                return null;
            }
        }

        public static (string Name, string Type)? GetLatestModelInfo()
        {
            using var db = new AppDbContext();
            try {
                db.Database.EnsureCreated();
                var entity = db.TrainedModels.OrderByDescending(m => m.CreatedAt).FirstOrDefault();
                if (entity == null) return null;
                return (entity.ModelName, entity.ModelType);
            } catch { return null; }
        }

        public static void ClearModel()
        {
            using var db = new AppDbContext();
            try {
                 db.Database.EnsureCreated();
                 db.TrainedModels.ExecuteDelete(); 
            } catch { /* Ignore if DB doesn't exist */ }
        }

        #endregion


        #region Private Methods -----------------------------------------------------

        private static string SerializeCentroids(double[][] centroids)
        {
            if (centroids == null) return "";
            var sb = new StringBuilder();
            foreach (var c in centroids)
            {
                sb.Append(string.Join(",", c));
                sb.Append("|");
            }
            return sb.ToString().TrimEnd('|');
        }

        private static double[][] DeserializeCentroids(string data)
        {
            if (string.IsNullOrEmpty(data)) return new double[0][];
            var rows = data.Split('|');
            var result = new double[rows.Length][];
            for (int i = 0; i < rows.Length; i++)
            {
                result[i] = rows[i].Split(',').Select(double.Parse).ToArray();
            }
            return result;
        }

        #endregion

    }
}