using System.Globalization; // Essential for string serialization

namespace Source
{
    public static class NormalizationHelper
    {
        /// <summary>
        /// Computes Mean and Standard Deviation for Z-Score Normalization.
        /// Use this on your RAW training data.
        /// </summary>
        public static (double[] Mean, double[] StdDev) ComputeZScoreStats(List<double[]> data)
        {
            if (data.Count == 0) return (new double[0], new double[0]);

            int dim = data[0].Length;
            double[] mean = new double[dim];
            double[] stdDev = new double[dim];

            // 1. Calculate Mean
            foreach (var row in data)
            {
                for (int i = 0; i < dim; i++)
                {
                    mean[i] += row[i];
                }
            }
            for (int i = 0; i < dim; i++)
            {
                mean[i] /= data.Count;
            }

            // 2. Calculate Standard Deviation
            foreach (var row in data)
            {
                for (int i = 0; i < dim; i++)
                {
                    double diff = row[i] - mean[i];
                    stdDev[i] += diff * diff;
                }
            }
            for (int i = 0; i < dim; i++)
            {
                // Standard Deviation = Sqrt(Sum(diff^2) / N)
                // Using Population StdDev formula. For Sample, use (data.Count - 1)
                stdDev[i] = Math.Sqrt(stdDev[i] / data.Count);

                // Prevent division by zero if a column has constant values
                if (stdDev[i] == 0) stdDev[i] = 1.0;
            }

            return (mean, stdDev);
        }

        /// <summary>
        /// Applies (x - mean) / stdDev to a single row.
        /// </summary>
        public static double[] NormalizeRow(double[] input, double[] mean, double[] stdDev)
        {
            double[] normalized = new double[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                normalized[i] = (input[i] - mean[i]) / stdDev[i];
            }
            return normalized;
        }

        // --- Serialization Helpers for Database Storage ---

        public static string SerializeArray(double[] array)
        {
            // returns "val1;val2;val3"
            return string.Join(";", array.Select(x => x.ToString(CultureInfo.InvariantCulture)));
        }

        public static double[] DeserializeArray(string data)
        {
            if (string.IsNullOrWhiteSpace(data)) return new double[0];
            return data.Split(';')
                       .Select(x => double.Parse(x, CultureInfo.InvariantCulture))
                       .ToArray();
        }
    }
}