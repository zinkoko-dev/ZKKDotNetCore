using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;
using ZKKDotNetCore.MinimalApi.Models;

namespace ZKKDotNetCore.MinimalApi
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<StudentDataModel> Students { get; set; }
    }
}
