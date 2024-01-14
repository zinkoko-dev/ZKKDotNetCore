using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZKKDotNetCore.MinimalApi.Models;

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

            app.MapPost("/Student", async ([FromServices] AppDbContext db, StudentDataModel reqModel) =>
            {
                await db.Students.AddAsync(reqModel);
                int result = await db.SaveChangesAsync();

                string message = result > 0 ? "Saving Successful." : "Saving Failed.";

                StudentResponseModle model = new StudentResponseModle
                {
                    IsSuccess = result > 0,
                    Message = message,
                    StudentDataModel = reqModel
                };
                return Results.Ok(model);
            })
            .WithName("CreateStudent")
            .WithOpenApi();

            app.MapPut("/Student/{id}", async ([FromServices] AppDbContext db, int id, StudentDataModel reqModel) =>
            {
                var item = await db.Students.FirstOrDefaultAsync(x => x.Student_Id == id);
                if (item is null)
                {
                    return Results.NotFound(new
                    {
                        IsSuccess = false,
                        Message = "No data found."
                    });
                }

                item.Student_Name = reqModel.Student_Name;
                item.Student_Gender = reqModel.Student_Gender;
                item.Student_City = reqModel.Student_City;

                var result = db.SaveChanges();
                StudentResponseModle model = new StudentResponseModle
                {
                    IsSuccess = result > 0,
                    Message = result > 0 ? "Update Successful." : "Updating Failed.",
                    StudentDataModel = item
                };
                return Results.Ok(model);
            })
            .WithName("UpdateStudent")
            .WithOpenApi();

            app.MapPatch("/Student/{id}", ([FromServices] AppDbContext db, int id, StudentDataModel reqModel) =>
            {
                StudentResponseModle model = new StudentResponseModle();
                var item = db.Students.FirstOrDefault(x => x.Student_Id == id);

                if (item is null)
                {
                    model.IsSuccess = false;
                    model.Message = "No data found.";
                    return Results.NotFound(model);
                }

                if (!string.IsNullOrWhiteSpace(reqModel.Student_Name))
                {
                    item.Student_Name = reqModel.Student_Name;
                }
                if (!string.IsNullOrWhiteSpace(reqModel.Student_City))
                {
                    item.Student_City = reqModel.Student_City;
                }
                if (!string.IsNullOrWhiteSpace(reqModel.Student_Gender))
                {
                    item.Student_Gender = reqModel.Student_Gender;
                }

                var result = db.SaveChanges();
                string message = result > 0 ? "Updating Successful." : "Updating Failed.";

                model = new StudentResponseModle()
                {
                    IsSuccess = result > 0,
                    Message = message,
                };

                return Results.Ok(model);
            })
            .WithName("PatchStudent")
            .WithOpenApi();
        }
    }
}
