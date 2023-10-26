using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using ZKKDotNetCore.RestAPI.Models;

namespace ZKKDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentAdoDotNetController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _connectionStringBuilder;

        // IConfiguration can read appsetting.json
        public StudentAdoDotNetController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DbConnection");
            _connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
        }

        [HttpGet]
        public IActionResult GetStudents()
        {
            string query = "select * from tbl_student";

            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            List<StudentDataModel> students = new List<StudentDataModel>();
            foreach (DataRow dr in dt.Rows)
            {
                StudentDataModel student = new StudentDataModel()
                {
                    Student_Id = Convert.ToInt32(dr["Student_Id"]),
                    Student_Name = dr["Student_Name"].ToString(),
                    Student_City = dr["Student_City"].ToString(),
                    Student_Gender = dr["Student_Gender"].ToString()
                };
                students.Add(student);
            }

            StudentListResponseModel stuList = new StudentListResponseModel()
            {
                IsSuccess = true,
                Message = "Success",
                ListStudentDataModel = students
            };
            return Ok(stuList);
        }

        [HttpGet("{id}")]
        public IActionResult EditStudent(int id)
        {
            string query = "SELECT * FROM [Tbl_Student] WHERE [Student_Id] = @Student_Id";

            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Student_Id", id);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            StudentResponseModle student = new StudentResponseModle();

            if (dt.Rows.Count == 0)
            {
                student.IsSuccess = false;
                student.Message = "No Data Found!!";
                return NotFound(student);
            }

            DataRow dr = dt.Rows[0];

            StudentDataModel model = new StudentDataModel()
            {
                Student_Id = Convert.ToInt32(dr["Student_Id"]),
                Student_Name = dr["Student_Name"].ToString(),
                Student_City = dr["Student_City"].ToString(),
                Student_Gender = dr["Student_Gender"].ToString()
            };

            student.IsSuccess = true;
            student.Message = "Success";
            student.StudentDataModel = model;
            return Ok(student);
        }

        [HttpPost]
        public IActionResult SaveStudent([FromBody] StudentDataModel student)
        {

            string query = $@"insert into [dbo].Tbl_Student
                            ([Student_Name],[Student_City],[Student_Gender])
                            values
                            (@Student_Name, @Student_City, @Student_Gender);
            ";

            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@Student_Name", student.Student_Name);
            cmd.Parameters.AddWithValue("@Student_City", student.Student_City);
            cmd.Parameters.AddWithValue("@Student_Gender", student.Student_Gender);

            int result = cmd.ExecuteNonQuery();

            string message = result > 0 ? "Saving Successful !!" : "Error While Saving !!";

            connection.Close();

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

            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@Student_Id", id);
            cmd.Parameters.AddWithValue("@Student_Name", student.Student_Name);
            cmd.Parameters.AddWithValue("@Student_City", student.Student_City);
            cmd.Parameters.AddWithValue("@Student_Gender", student.Student_Gender);

            int result = cmd.ExecuteNonQuery();

            string message = result > 0 ? "Update Successful !!" : "Error While Update !!";

            connection.Close();

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
        public IActionResult PatchBlog(int id, [FromBody] StudentDataModel student)
        {
            string query = "SELECT * FROM [Tbl_Student] WHERE [Student_Id] = @Student_Id";

            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Student_Id", id);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            StudentResponseModle responseModle = new StudentResponseModle();

            if (dt.Rows.Count == 0)
            {
                responseModle.IsSuccess = false;
                responseModle.Message = "No Data Found!!";
                return NotFound(responseModle);
            }

            DataRow dr = dt.Rows[0];

            StudentDataModel model = new StudentDataModel()
            {
                Student_Id = Convert.ToInt32(dr["Student_Id"]),
                Student_Name = dr["Student_Name"].ToString(),
                Student_City = dr["Student_City"].ToString(),
                Student_Gender = dr["Student_Gender"].ToString()
            };

            string query1 = @"UPDATE [dbo].[Tbl_Student]
                             SET
                             [Student_Name] = @Student_Name,
                             [Student_City] = @Student_City,
                             [Student_Gender] = @Student_Gender
                             WHERE
                             [Student_Id] = @Student_Id";

            SqlCommand cmd1 = new SqlCommand(query1, connection);

            cmd1.Parameters.AddWithValue("@Student_Id", id);
           
            if (!string.IsNullOrWhiteSpace(student.Student_Name))
            {
                cmd1.Parameters.AddWithValue("@Student_Name", student.Student_Name);
            }
            else
            {
                cmd1.Parameters.AddWithValue("@Student_Name", model.Student_Name);
            }

            if (!string.IsNullOrWhiteSpace(student.Student_City))
            {
                cmd1.Parameters.AddWithValue("@Student_City", student.Student_City);
            }
            else
            {
                cmd1.Parameters.AddWithValue("@Student_City", model.Student_City);
            }

            if (!string.IsNullOrWhiteSpace(student.Student_Gender))
            {
                cmd1.Parameters.AddWithValue("@Student_Gender", student.Student_Gender);
            }
            else
            {
                cmd1.Parameters.AddWithValue("@Student_Gender", model.Student_Gender);
            }

            int result = cmd1.ExecuteNonQuery();

            string message = result > 0 ? "Updating Successful." : "Updating Failed.";

            responseModle.IsSuccess = result > 0;
            responseModle.Message = message;
            return Ok(responseModle);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Student] WHERE [Student_Id] = @Student_Id";

            SqlConnection connection = new SqlConnection(_connectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Student_Id", id);

            int result = cmd.ExecuteNonQuery();

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
