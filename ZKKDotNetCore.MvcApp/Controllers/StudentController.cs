using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using ZKKDotNetCore.MvcApp.EFDbContext;
using ZKKDotNetCore.MvcApp.Models;

namespace ZKKDotNetCore.MvcApp.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public StudentController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [ActionName("Index")]
        public IActionResult StudentIndex()
        {
            List<StudentDataModel> lst = _appDbContext.Students.ToList();
            return View("StudentIndex", lst);
        }

        [ActionName("List")]
        public async Task<IActionResult> StudentList(int pageNo = 1, int pageSize = 10)
        {
            StudentDataResponseModel model = new StudentDataResponseModel();
            List<StudentDataModel> lst = _appDbContext.Students.AsNoTracking()
                .Skip((pageNo - 1)*pageSize)
                .Take(pageSize)
                .ToList();
            int rowCount = await _appDbContext.Students.CountAsync();
            int pageCount = (rowCount / pageSize)+(rowCount % pageSize == 0 ? 0 : 1);
            PageSettingModel pageSetting = new PageSettingModel(pageNo, pageSize, pageCount,"/student/list");
            model.PageSetting = pageSetting;
            model.Students = lst;
            return View("StudentList", model);
        }

        [ActionName("Create")]
        public IActionResult StudentCreate()
        {
            return View("StudentCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> StudentSave(StudentDataModel reqModel)
        {
            await _appDbContext.Students.AddAsync(reqModel);
            var result = await _appDbContext.SaveChangesAsync();
            string message = result > 0 ? "Saving Successful !" : "Saving Error !";
            TempData["Message"] = message;
            TempData["isSuccess"] = result > 0;
            return Redirect("/student/list");
        }

        [ActionName("Edit")]
        public async Task<IActionResult> StudentEdit(int id)
        {
            if(!await _appDbContext.Students.AsNoTracking().AnyAsync(x => x.Student_Id == id))
            {
                TempData["Message"] = "No Data Found";
                TempData["isSuccess"] = false;
                return Redirect("/student/list");
            }
            var student = await _appDbContext.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Student_Id == id);
            return View("StudentEdit", student);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> StudentUpdate(int id, StudentDataModel reqModel)
        {
            if (!await _appDbContext.Students.AsNoTracking().AnyAsync(x => x.Student_Id == id) || reqModel == null)
            {
                TempData["Message"] = "No Data Found";
                TempData["isSuccess"] = false;
                return Redirect("/student/list");
            }
            var stu = await _appDbContext.Students.FirstOrDefaultAsync(x => x.Student_Id == id);
            if(stu is not null)
            {
                stu.Student_Name = reqModel.Student_Name;
                stu.Student_City = reqModel.Student_City;
                stu.Student_Gender = reqModel.Student_Gender;
            }
            int result = _appDbContext.SaveChanges();
            TempData["Message"] = result > 0 ? "Update Successful !" : "Update Fail !";
            TempData["isSuccess"] = result > 0;
            return Redirect("/student/list");
        }

        [ActionName("Delete")]
        public async Task<IActionResult> StudentDelete(int id)
        {
            if (!await _appDbContext.Students.AsNoTracking().AnyAsync(x => x.Student_Id == id))
            {
                TempData["Message"] = "No Data Found";
                TempData["isSuccess"] = false;
                return Redirect("/student/list");
            }
            var student = await _appDbContext.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Student_Id == id);
            if(student is null)
            {
                TempData["Message"] = "No Data Found";
                TempData["isSuccess"] = false;
                return Redirect("/student/list");
            }

            _appDbContext.Remove(student);
            var result = _appDbContext.SaveChanges();
            TempData["Message"] = result > 0 ? "Delete Successful !" : "Delete Fail !";
            TempData["isSuccess"] = result > 0;
            return Redirect("/student/list");
        }

        public IActionResult Generate() {
            for (var i=1; i<=100; i++)
            {
                StudentDataModel stu = new StudentDataModel()
                {
                    Student_Name = i.ToString(),
                    Student_City = i.ToString(),
                    Student_Gender = i.ToString(),
                };
                _appDbContext.Students.Add(stu);
                _appDbContext.SaveChanges();
            }
            return Redirect("/Home/Index");
        }
    }
}
