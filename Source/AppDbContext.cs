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
        public string ModelType { get; set; } 
        public string? SerializedData { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string WeightsData { get; set; } = string.Empty;
        public string CentroidsData { get; set; } = string.Empty;
        public string SigmasData { get; set; } = string.Empty;

        public double Bias { get; set; }
        public int InputCount { get; set; }
        public int HiddenCount { get; set; }

        public string NormalizationMeans { get; set; } = string.Empty;
        public string NormalizationStdDevs { get; set; } = string.Empty;

        // Stores the schema string used for training (e.g. "PROTEIN;FAT;CLASS")
        public string InputSchema { get; set; } = string.Empty;
    }
}