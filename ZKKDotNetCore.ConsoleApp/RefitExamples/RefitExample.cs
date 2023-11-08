using Newtonsoft.Json;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ZKKDotNetCore.ConsoleApp.Models;

namespace ZKKDotNetCore.ConsoleApp.RefitExamples
{
    public class RefitExample
    {
        private readonly IStudentApi studentApi;

        public RefitExample()
        {
            studentApi = RestService.For<IStudentApi>("https://localhost:7201");

        }

        public async Task Run()
        {
            await Read();
        }

        private async Task Read()
        {
            StudentListResponseModel responseModel = await studentApi.GetStudents();

            Console.WriteLine(JsonConvert.SerializeObject(responseModel));
            Console.WriteLine(JsonConvert.SerializeObject(responseModel, Formatting.Indented));
        }

        private async Task Create(string name, string city, string gender)
        {
            StudentResponseModle model = await studentApi.CreateStudent(new StudentDataModel
            {
                Student_Name = name,
                Student_City = city,
                Student_Gender = gender,
            });
            Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
        }

        private async Task Edit(int id)
        {
            StudentResponseModle model = await studentApi.EditStudent(id);

            Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
        }

        private async Task Update(int id, string name, string city, string gender)
        {
            StudentResponseModle model = await studentApi.UpdateStudent(id, new StudentDataModel()
            {
                Student_Name = name,
                Student_City = city,
                Student_Gender = gender,
            });

            Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
        }

        private async Task Delete(int id)
        {
            StudentResponseModle model = await studentApi.DeleteStudent(id);

            Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
        }

        private async Task Patch(int id, string name, string city, string gender)
        {
            StudentResponseModle model = await studentApi.PathStudent(id, new StudentDataModel()
            {
                Student_Name = name,
                Student_City = city,
                Student_Gender = gender,
            });

            Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
        }
    }

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
