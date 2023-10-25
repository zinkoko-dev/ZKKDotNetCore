using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;
using ZKKDotNetCore.RestAPI.Models;

namespace ZKKDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentDapperController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _connectionStringBuilder;

        // IConfiguration can read appsetting.json
        public StudentDapperController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");
            _connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            string query = "SELECT * FROM Tbl_Student order by Student_Id desc";
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);

            List<StudentDataModel> lst = db.Query<StudentDataModel>(query).ToList();

            StudentListResponseModel stuList = new StudentListResponseModel()
            {
                IsSuccess = true,
                Message = "Success",
                ListStudentDataModel = lst
            };
            return Ok(stuList);
        }

        [HttpGet("{id}")]
        public IActionResult EditStudent(int id)
        {
            string query = "SELECT * FROM [Tbl_Student] WHERE [Student_Id] = @Student_Id";

            StudentDataModel student = new StudentDataModel()
            {
                Student_Id = id,
            };

            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            StudentDataModel item = db.Query<StudentDataModel>(query, student).FirstOrDefault();

            StudentResponseModle model = new StudentResponseModle();
            if (item == null)
            {
                model.IsSuccess = false;
                model.Message = "No Data Found!!";
                model.StudentDataModel = student;
                return NotFound(model);
            }

            model.IsSuccess = true;
            model.Message = "Success";
            model.StudentDataModel = item;
            return Ok(model);
        }

        [HttpPost]
        public IActionResult SaveStudent([FromBody] StudentDataModel student)
        {

            string query = $@"insert into [dbo].[Tbl_Student]
                            ([Student_Name],[Student_City],[Student_Gender])
                            values
                            (@Student_Name, @Student_City, @Student_Gender);";

            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            var result = db.Execute(query, student);
            string message = result > 0 ? "Saving Successful !!" : "Saving Fail !!";

            StudentResponseModle model = new StudentResponseModle()
            {
                IsSuccess = result > 0,
                Message = message
            };

            return Ok(model);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id, [FromBody] StudentDataModel student)
        {
            string query = @"UPDATE [dbo].[Tbl_Student]
                             SET
                             [Student_Name] = @Student_Name,
                             [Student_City] = @Student_City,
                             [Student_Gender] = @Student_Gender
                             WHERE
                             [Student_Id] = @Student_Id";

            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            student.Student_Id = id;
            var result = db.Execute(query, student);

            string message = result > 0 ? "Update Successful !!" : "Error While Update !!";

            StudentResponseModle model = new StudentResponseModle();

            if (result < 0)
            {
                model.IsSuccess = false;
                model.Message = message;
                return NotFound(model);
            }

            model.IsSuccess = result > 0;
            model.Message = message;
            model.StudentDataModel = student;
            return Ok(model);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchStudent(int id, [FromBody] StudentDataModel student)
        {
            string query = "SELECT * FROM [Tbl_Student] WHERE [Student_Id] = @Student_Id";
            student.Student_Id = id;
            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            var item = db.Query<StudentDataModel>(query, student).FirstOrDefault();

            StudentResponseModle responseModle = new StudentResponseModle();

            if (item == null)
            {
                responseModle.IsSuccess = false;
                responseModle.Message = "No Data Found!!";
                return NotFound(responseModle);
            }

            string query1 = @"UPDATE [dbo].[Tbl_Student]
                             SET
                             [Student_Name] = @Student_Name,
                             [Student_City] = @Student_City,
                             [Student_Gender] = @Student_Gender
                             WHERE
                             [Student_Id] = @Student_Id";

            if (!string.IsNullOrWhiteSpace(student.Student_Name))
            {
                item.Student_Name = student.Student_Name;
            }
            if (!string.IsNullOrWhiteSpace(student.Student_City))
            {
                item.Student_City = student.Student_City;
            }
            if (!string.IsNullOrWhiteSpace(student.Student_Gender))
            {
                item.Student_Gender = student.Student_Gender;
            }

            var result = db.Execute(query1, item);

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";

            responseModle.IsSuccess = result > 0;
            responseModle.Message = message;
            return Ok(responseModle);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Student] WHERE [Student_Id] = @Student_Id";

            StudentDataModel item = new StudentDataModel()
            {
                Student_Id = id,
            };

            using IDbConnection db = new SqlConnection(_connectionStringBuilder.ConnectionString);
            var result = db.Execute(query, item);

            string message = result > 0 ? "Delete Successful !!" : "Error While Delete !!";

            StudentResponseModle model = new StudentResponseModle();
            if (result > 0)
            {
                model.IsSuccess = result > 0;
                model.Message = message;
                return Ok(model);
            }
            model.IsSuccess = result > 0;
            model.Message = message;
            return Ok(model);
        }
    }
}
