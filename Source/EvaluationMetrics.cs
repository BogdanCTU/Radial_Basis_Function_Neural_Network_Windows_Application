using System;
using System.Collections.Generic;
using System.Linq;

namespace Source
{
    public class EvaluationMetrics
    {
        public double Accuracy { get; set; }
        public double Precision { get; set; }
        public double Recall { get; set; } // Same as Sensitivity
        public double Sensitivity { get; set; }
        public double FMeasure { get; set; }
        public double AucRoc { get; set; }
        public double Auprc { get; set; }

        // Confusion Matrix
        public int TP { get; set; }
        public int TN { get; set; }
        public int FP { get; set; }
        public int FN { get; set; }

        public override string ToString()
        {
            return $"Accuracy: {Accuracy:P2}\n" +
                   $"Precision: {Precision:P2}\n" +
                   $"Recall (Sensitivity): {Recall:P2}\n" +
                   $"F-Measure: {FMeasure:P2}\n" +
                   $"AUC-ROC: {AucRoc:F4}\n" +
                   $"AUPRC: {Auprc:F4}\n" +
                   $"[TP: {TP}, TN: {TN}, FP: {FP}, FN: {FN}]";
        }
    }

    public static class MetricsCalculator
    {
        public static EvaluationMetrics Calculate(List<double> rawScores, List<double> actualLabels)
        {
            var metrics = new EvaluationMetrics();
            int n = rawScores.Count;

            // 1. Calculate Confusion Matrix (Threshold = 0.5)
            for (int i = 0; i < n; i++)
            {
                int predictedClass = rawScores[i] >= 0.5 ? 1 : 0;
                int actualClass = (int)actualLabels[i];

                if (predictedClass == 1 && actualClass == 1) metrics.TP++;
                else if (predictedClass == 0 && actualClass == 0) metrics.TN++;
                else if (predictedClass == 1 && actualClass == 0) metrics.FP++;
                else if (predictedClass == 0 && actualClass == 1) metrics.FN++;
            }

            // 2. Basic Metrics
            metrics.Accuracy = (double)(metrics.TP + metrics.TN) / n;

            // Handle division by zero edge cases
            metrics.Precision = (metrics.TP + metrics.FP) > 0 ? (double)metrics.TP / (metrics.TP + metrics.FP) : 0;
            metrics.Recall = (metrics.TP + metrics.FN) > 0 ? (double)metrics.TP / (metrics.TP + metrics.FN) : 0;
            metrics.Sensitivity = metrics.Recall; // Synonym

            if (metrics.Precision + metrics.Recall > 0)
            {
                metrics.FMeasure = 2 * (metrics.Precision * metrics.Recall) / (metrics.Precision + metrics.Recall);
            }

            // 3. AUC - ROC Calculation (Trapezoidal Rule)
            // We need pairs of (Score, Label) sorted by Score Descending
            var predictions = rawScores.Zip(actualLabels, (s, l) => new { Score = s, Label = l })
                                       .OrderByDescending(x => x.Score)
                                       .ToList();

            metrics.AucRoc = CalculateAucRoc(predictions);
            metrics.Auprc = CalculateAuprc(predictions);

            return metrics;
        }

        private static double CalculateAucRoc(dynamic sortedPredictions)
        {
            // Mann-Whitney U or Trapezoidal integration
            double auc = 0.0;
            double height = 0.0;
            int posCount = 0;
            int negCount = 0;

            foreach (var p in sortedPredictions)
            {
                if (p.Label == 1)
                {
                    height += 1.0;
                    posCount++;
                }
                else
                {
                    auc += height;
                    negCount++;
                }
            }

            return (posCount == 0 || negCount == 0) ? 0 : auc / (posCount * negCount);
        }

        private static double CalculateAuprc(dynamic sortedPredictions)
        {
            // Area Under Precision-Recall Curve using Average Precision logic
            double sumPrecision = 0.0;
            int runningTp = 0;
            int totalPositives = 0;

            // Count total positives first
            foreach (var p in sortedPredictions)
            {
                if (p.Label == 1) totalPositives++;
            }

            if (totalPositives == 0) return 0;

            for (int i = 0; i < sortedPredictions.Count; i++)
            {
                if (sortedPredictions[i].Label == 1)
                {
                    runningTp++;
                    // Precision at this cutoff k
                    double precisionAtK = (double)runningTp / (i + 1);
                    sumPrecision += precisionAtK;
                }
            }

            return sumPrecision / totalPositives;
        }
    }
}