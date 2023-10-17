using System.Data.Entity;
using ZKKDotNetCore.WindowsFormsApp.Models;

namespace ZKKDotNetCore.WindowsFormsApp
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
        }

        public DbSet<StudentDataModel> Students { get; set; }

    }
}