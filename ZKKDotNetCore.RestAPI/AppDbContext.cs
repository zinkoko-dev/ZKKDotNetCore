using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using ZKKDotNetCore.RestAPI.Models;

namespace ZKKDotNetCore.RestAPI
{
    public class AppDbContext : DbContext
    {

        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "ZKKDotNetCore",
            UserID = "sa",
            Password = "sasa",
            TrustServerCertificate = true
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
