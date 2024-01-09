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

        [Get("/api/student/{id}")]
        Task<StudentResponseModle> EditStudent(int id);

        [Post("/api/student")]
        Task<StudentResponseModle> CreateStudent(StudentDataModel blog);

        [Put("/api/student/{id}")]
        Task<StudentResponseModle> UpdateStudent(int id, StudentDataModel blog);

        [Patch("/api/student/{id}")]
        Task<StudentResponseModle> PathStudent(int id, StudentDataModel blog);

        [Delete("/api/student/{id}")]
        Task<StudentResponseModle> DeleteStudent(int id);
    }
}
