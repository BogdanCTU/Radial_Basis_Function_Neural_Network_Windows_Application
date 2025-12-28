using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Source.Data
{

    public class AppDbContext : DbContext
    {
        public DbSet<TrainedModel> TrainedModels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Ensures the DB is created in the execution folder
            optionsBuilder.UseSqlite("Data Source=food_classifier.db");
        }
    }

    public class TrainedModel
    {
        [Key]
        public int Id { get; set; }
        public string ModelName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Serialized arrays (JSON or CSV format string)
        // Format: "w1;w2;w3..."
        public string WeightsData { get; set; } = string.Empty;

        // Format: "c1_1,c1_2;c2_1,c2_2..."
        public string CentroidsData { get; set; } = string.Empty;

        // Format: "s1;s2;s3..."
        public string SigmasData { get; set; } = string.Empty;

        public double Bias { get; set; }
        public int InputCount { get; set; }
        public int HiddenCount { get; set; }

        // In order to process manual inputs
        public string NormalizationMeans { get; set; } = string.Empty;
        public string NormalizationStdDevs { get; set; } = string.Empty;
    }

}
