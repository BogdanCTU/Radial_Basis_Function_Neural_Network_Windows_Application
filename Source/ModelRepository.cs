using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Source.Data
{
    public static class ModelRepository
    {
        // UPDATE: Added meanStr and stdStr parameters
        public static void SaveModel(string name, RbfNetwork network, string meanStr, string stdStr)
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();

            var entity = new TrainedModel
            {
                ModelName = name,
                InputCount = network.InputCount,
                HiddenCount = network.HiddenCount,
                Bias = network.Bias,
                WeightsData = string.Join(";", network.Weights),
                SigmasData = string.Join(";", network.Sigmas),
                CentroidsData = SerializeCentroids(network.Centroids),

                // NEW: Save Normalization Stats
                NormalizationMeans = meanStr,
                NormalizationStdDevs = stdStr
            };

            db.TrainedModels.Add(entity);
            db.SaveChanges();
        }

        public static RbfNetwork? LoadModel(string name)
        {
            using var db = new AppDbContext();
            db.Database.EnsureCreated();

            var entity = db.TrainedModels
                .OrderByDescending(m => m.CreatedAt)
                .FirstOrDefault(m => m.ModelName == name);

            if (entity == null) return null;

            var network = new RbfNetwork(entity.InputCount, entity.HiddenCount, 1);
            network.Bias = entity.Bias;
            network.Weights = entity.WeightsData.Split(';').Select(double.Parse).ToArray();
            network.Sigmas = entity.SigmasData.Split(';').Select(double.Parse).ToArray();
            network.Centroids = DeserializeCentroids(entity.CentroidsData);

            return network;
        }

        public static void ClearModel()
        {
            using var db = new AppDbContext();
            db.TrainedModels.ExecuteDelete();
            db.Database.EnsureDeleted();
        }

        // Helpers
        private static string SerializeCentroids(double[][] centroids)
        {
            var sb = new StringBuilder();
            foreach (var c in centroids)
            {
                sb.Append(string.Join(",", c));
                sb.Append("|");
            }
            return sb.ToString().TrimEnd('|');
        }

        public static double[][] DeserializeCentroids(string data)
        {
            var rows = data.Split('|');
            var result = new double[rows.Length][];
            for (int i = 0; i < rows.Length; i++)
            {
                result[i] = rows[i].Split(',').Select(double.Parse).ToArray();
            }
            return result;
        }
    }
}