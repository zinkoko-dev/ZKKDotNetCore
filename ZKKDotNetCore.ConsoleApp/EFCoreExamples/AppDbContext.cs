using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using ZKKDotNetCore.ConsoleApp.Models;

namespace ZKKDotNetCore.ConsoleApp.EFCoreExamples
{
    public class AppDbContext : DbContext
    {

        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "ZKKDotNetCore",
            UserID = "sa",
            Password = "sasa"
        };

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(sqlConnectionStringBuilder.ConnectionString);
            }
        }

        public DbSet<StudentDataModel> Students { get; set; }
    }
}
