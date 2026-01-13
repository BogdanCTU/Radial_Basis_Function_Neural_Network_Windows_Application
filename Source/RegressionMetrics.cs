using System;
using System.Collections.Generic;
using System.Linq;

namespace Source
{
    public class RegressionMetrics
    {
        // Mean Absolute Error: The average of the absolute differences between predictions and actuals.
        public double MAE { get; set; }

        // Root Mean Squared Error: The square root of the average of squared differences.
        public double RMSE { get; set; }

        // Normalized RMSE: RMSE divided by the range (Max - Min) of the actual data.
        public double NRMSE { get; set; }

        // R-squared (Coefficient of Determination): Proportion of variance in the dependent variable explained by the model.
        public double R2 { get; set; }

        public override string ToString()
        {
            return $"\nMAE:   {MAE:F4}\n" +
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

            // n: The total number of data points (samples).
            int n = predictions.Count;

            // sumAbsError: The sum of absolute errors (|predicted - actual|), used for MAE.
            double sumAbsError = 0;

            // sumSqError: The sum of squared errors ((predicted - actual)^2), used for MSE/RMSE.
            double sumSqError = 0;
            
            // sumActuals: The sum of all actual values, used to calculate the mean of actuals.
            double sumActuals = 0;

            // minActual: The minimum observed value in the actual dataset, used for range.
            double minActual = double.MaxValue;

            // maxActual: The maximum observed value in the actual dataset, used for range.
            double maxActual = double.MinValue;

            for (int i = 0; i < n; i++)
            {
                // p: The predicted value for the current sample.
                double p = predictions[i];

                // a: The actual (ground truth) value for the current sample.
                double a = actuals[i];

                // err: The residual or error (predicted - actual) for the current sample.
                double err = p - a;

                sumAbsError += Math.Abs(err);
                sumSqError += err * err;

                sumActuals += a;
                if (a < minActual) minActual = a;
                if (a > maxActual) maxActual = a;
            }

            var metrics = new RegressionMetrics();

            // MAE = Sum(|error|) / n
            metrics.MAE = sumAbsError / n;

            // mse: Mean Squared Error (Sum(error^2) / n).
            double mse = sumSqError / n;
            metrics.RMSE = Math.Sqrt(mse);

            // NRMSE (Normalized by Range: Max - Min)
            // Prevent division by zero if all values are the same
            // range: The difference between the maximum and minimum actual values.
            double range = maxActual - minActual;
            metrics.NRMSE = range > 0 ? metrics.RMSE / range : 0;

            // R2 Score
            // R2 = 1 - (SS_res / SS_tot)
            // SS_res = sum((y_i - f_i)^2) = sumSqError
            // SS_tot = sum((y_i - y_mean)^2)
            
            // meanActual: The arithmetic average of the actual values.
            double meanActual = sumActuals / n;

            // ssTot: Total Sum of Squares (variance * n), measures total variance in the data.
            double ssTot = 0;
            for (int i = 0; i < n; i++)
            {
                // dev: Deviation of the current actual value from the mean.
                double dev = actuals[i] - meanActual;
                ssTot += dev * dev;
            }
            
            metrics.R2 = ssTot > 0 ? 1.0 - (sumSqError / ssTot) : 0;

            return metrics;
        }
    }
}
