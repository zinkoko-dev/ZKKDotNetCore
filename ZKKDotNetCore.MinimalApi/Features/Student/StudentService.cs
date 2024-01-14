using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ZKKDotNetCore.MinimalApi.Features.Student
{
    public static class StudentService
    {
        public static void AddStudentService(this IEndpointRouteBuilder app)
        {
            app.MapGet("/Student/{pageNo}/{pageSize}", async ([FromServices] AppDbContext db, int pageNo, int pageSize) =>
            {
                return await db.Students
                .AsNoTracking()
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            })
            .WithName("GetStudents")
            .WithOpenApi();
        }
    }
}
