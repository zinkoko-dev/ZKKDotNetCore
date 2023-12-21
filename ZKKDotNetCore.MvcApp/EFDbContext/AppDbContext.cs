using Microsoft.EntityFrameworkCore;
using ZKKDotNetCore.MvcApp.Models;
namespace ZKKDotNetCore.MvcApp.EFDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<StudentDataModel> Students { get; set; }
    }
}