using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using FileUploader.WebApp.Models;

namespace FileUploader.WebApp.DAL
{
    public class StatisticsContext : DbContext
    {
        public DbSet<FileStatisticsEntity> FileStatisticsEntities { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}