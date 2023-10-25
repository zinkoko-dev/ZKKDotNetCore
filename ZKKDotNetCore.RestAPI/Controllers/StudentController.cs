using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ZKKDotNetCore.RestAPI.Models;

namespace ZKKDotNetCore.RestAPI.Controllers
{
    // https://localhost:7201/api/Student
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        AppDbContext db = new AppDbContext();

        [HttpGet]
        public IActionResult GetStudents()
        {
            var list = db.Students.ToList();
            StudentListResponseModel stuList = new StudentListResponseModel()
            {
                IsSuccess = true,
                Message = "Success",
                ListStudentDataModel = list
            };
            return Ok(stuList);
        }

        [HttpGet("{id}")]
        public IActionResult EditStudent(int id)
        {
            AppDbContext db = new AppDbContext();
            StudentDataModel item = db.Students.FirstOrDefault(x => x.Student_Id == id);

            StudentResponseModle student = new StudentResponseModle();
            if (item is null)
            {
                student.IsSuccess = false;
                student.Message = "No Data Found!!";
                return NotFound(student);
            }

            student.IsSuccess = true;
            student.Message = "Success";
            student.StudentDataModel = item;
            return Ok(student);
        }

        [HttpPost]
        public IActionResult SaveStudent([FromBody] StudentDataModel student)
        {
            AppDbContext db = new AppDbContext();
            db.Students.Add(student);
            var result = db.SaveChanges();

            string message = result > 0 ? "Save Successful !!" : "Error Successful !!";
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
            AppDbContext db = new AppDbContext();
            StudentDataModel item = db.Students.FirstOrDefault(x => x.Student_Id == id);

            StudentResponseModle model = new StudentResponseModle();

            if (item is null)
            {
                model.IsSuccess = false;
                model.Message = "No Data Found!!";
                return NotFound(model);
            }

            item.Student_Name = student.Student_Name;
            item.Student_City = student.Student_City;
            item.Student_Gender = student.Student_Gender;

            var result = db.SaveChanges();
            string message = result > 0 ? "Update Successful !!" : "Update Error!!";
            model.IsSuccess = result > 0;
            model.Message = message;

            return Ok(model);
        }
        
        //[HttpPatch]
        //public IActionResult Patch(int id, [FromBody] JsonPatchDocument<StudentDataModel> blogPatch)
        //{
        //    AppDbContext db = new AppDbContext();
        //    var blog = db.Students.FirstOrDefault(b => b.Student_Id == id);

        //    StudentResponseModle data = new StudentResponseModle();
        //    if (blog is null)
        //    {
        //        data.IsSuccess = false;
        //        data.Message = "No data found.";
        //        return NotFound(data);
        //    }

        //    blogPatch.ApplyTo(blog);
        //    db.SaveChanges();
        //    data.IsSuccess = true;
        //    data.Message = "Success";
        //    data.StudentDataModel = blog;

        //    return Ok(data);
        //}

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, [FromBody] StudentDataModel student)
        {
            StudentResponseModle model = new StudentResponseModle();
            AppDbContext db = new AppDbContext();
            var item = db.Students.FirstOrDefault(x => x.Student_Id == id);

            if (item is null)
            {
                model.IsSuccess = false;
                model.Message = "No data found.";
                return NotFound(model);
            }

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

            var result = db.SaveChanges();
            string message = result > 0 ? "Updating Successful." : "Updating Failed.";

            model = new StudentResponseModle()
            {
                IsSuccess = result > 0,
                Message = message,
            };

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Students.FirstOrDefault(x => x.Student_Id == id);

            StudentResponseModle model = new StudentResponseModle();
            if (item == null)
            {
                model.IsSuccess = false;
                model.Message = "No Data Found !!";
                return NotFound(model);
            }

            db.Students.Remove(item);
            var result = db.SaveChanges();
            String message = result > 0 ? "Delete Successful !!" : "Delete Error !!";
            model.IsSuccess = result > 0;
            model.Message = message;
            return Ok(model);
        }
    }
}