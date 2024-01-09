using Refit;
using System.Threading.Tasks;
using ZKKDotNetCore.MvcApp.Interfaces;
using ZKKDotNetCore.MvcApp.Models;

namespace ZKKDotNetCore.MvcApp.Interfaces
{
    public interface IStudentApi
    {
        [Get("/api/student")]
        Task<StudentListResponseModel> GetStudents();

        [Get("/api/student/{pageNo}/{pageSize}")]
        Task<StudentListResponseModel> GetStudent(int pageNo, int pageSize = 10);

        [Get("/api/blog/{id}")]
        Task<StudentResponseModle> EditStudent(int id);

        [Post("/api/blog")]
        Task<StudentResponseModle> CreateStudent(StudentDataModel blog);

        [Put("/api/blog/{id}")]
        Task<StudentResponseModle> UpdateStudent(int id, StudentDataModel blog);

        [Patch("/api/blog/{id}")]
        Task<StudentResponseModle> PathStudent(int id, StudentDataModel blog);

        [Delete("/api/blog/{id}")]
        Task<StudentResponseModle> DeleteStudent(int id);
    }
}
