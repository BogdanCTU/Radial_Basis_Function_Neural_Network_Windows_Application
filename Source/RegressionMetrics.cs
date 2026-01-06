using System;
using System.Collections.Generic;
using System.Linq;

namespace Source
{
    public class RegressionMetrics
    {
        public double MAE { get; set; }
        public double RMSE { get; set; }
        public double NRMSE { get; set; }
        public double R2 { get; set; }

        public override string ToString()
        {
            return $"MAE:   {MAE:F4}\n" +
                   $"RMSE:  {RMSE:F4}\n" +
                   $"NRMSE: {NRMSE:P2}\n" +
                   $"R2:    {R2:F4}";
        }
    }

    public static class RegressionMetricsCalculator
    {
        public static RegressionMetrics Calculate(List<double> predictions, List<double> actuals)
        {
            if (predictions.Count != actuals.Count || predictions.Count == 0)
                throw new ArgumentException(" predictions and actuals must have the same non-zero length.");

            int n = predictions.Count;
            double sumAbsError = 0;
            double sumSqError = 0;
            
            double sumActuals = 0;
            double minActual = double.MaxValue;
            double maxActual = double.MinValue;

            for (int i = 0; i < n; i++)
            {
                double p = predictions[i];
                double a = actuals[i];
                double err = p - a;

                sumAbsError += Math.Abs(err);
                sumSqError += err * err;

                sumActuals += a;
                if (a < minActual) minActual = a;
                if (a > maxActual) maxActual = a;
            }

            var metrics = new RegressionMetrics();

            // MAE
            metrics.MAE = sumAbsError / n;

            // RMSE
            double mse = sumSqError / n;
            metrics.RMSE = Math.Sqrt(mse);

            // NRMSE (Normalized by Range: Max - Min)
            // Prevent division by zero if all values are the same
            double range = maxActual - minActual;
            metrics.NRMSE = range > 0 ? metrics.RMSE / range : 0;

            // R2 Score
            // R2 = 1 - (SS_res / SS_tot)
            // SS_res = sum((y_i - f_i)^2) = sumSqError
            // SS_tot = sum((y_i - y_mean)^2)
            double meanActual = sumActuals / n;
            double ssTot = 0;
            for (int i = 0; i < n; i++)
            {
                double dev = actuals[i] - meanActual;
                ssTot += dev * dev;
            }
            
            metrics.R2 = ssTot > 0 ? 1.0 - (sumSqError / ssTot) : 0;

            return metrics;
        }
    }
}
