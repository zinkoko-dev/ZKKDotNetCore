using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> StudentIndex(int pageNo = 1, int pageSize = 10)
        {
            List<StudentDataModel> studentList = await _appDbContext.Students
                .AsNoTracking()
                .OrderByDescending(x => x.Student_Id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            int pageRowCount = await _appDbContext.Students.CountAsync();
            int pageCount = pageRowCount / pageSize;
            if (pageRowCount % pageSize > 0) pageCount++;

            StudentListResponseModel model = new StudentListResponseModel()
            {
                ListStudentDataModel = studentList,
                PageRowCount = pageRowCount,
                PageCount = pageCount,
                PageNo = pageNo,
                PageSize = pageSize
            };

            return View("StudentIndex", model);
        }

        [ActionName("Create")]
        public IActionResult StudentCreate()
        {
            return View("StudentCreate");
        }

        [HttpPost]
        [ActionName("Save")]
        public async Task<IActionResult> StudentSave(StudentDataModel student)
        {
            await _appDbContext.AddAsync(student);
            var result = await _appDbContext.SaveChangesAsync();
            TempData["IsSuccess"] = result > 0;
            TempData["Message"] = result > 0 ? "Saving Successful" : "Error While Saving !!";
            return Redirect("/Student");
        }

        [ActionName("Edit")]
        public async Task<IActionResult> BlogEdit(int id)
        {
            bool isExist = await _appDbContext.Students.AsNoTracking().AnyAsync(x => x.Student_Id == id);
            if (!isExist)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No data found.";
                return Redirect("/Student");
            }

            StudentDataModel? item = await _appDbContext.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Student_Id == id);
            if (item == null)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No data found.";
                return Redirect("/Student");
            }
            return View("StudentEdit", item);
        }

        public async Task<IActionResult> Generate()
        {
            for(int i=1; i<=1000; i++)
            {
                await _appDbContext.AddAsync(new StudentDataModel
                {
                    Student_Name = i.ToString(),
                    Student_City = i.ToString(),
                    Student_Gender = i.ToString(),
                });
                var result = await _appDbContext.SaveChangesAsync();
            }
            return Redirect("/Student");
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> StudentUpdate(int id, StudentDataModel blog)
        {
            bool isExist = await _appDbContext.Students.AsNoTracking().AnyAsync(x => x.Student_Id == id);
            if (!isExist)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No data found.";
                return Redirect("/Student");
            }

            var item = await _appDbContext.Students.FirstOrDefaultAsync(x => x.Student_Id == id);
            if (item == null)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No data found.";
                return Redirect("/Student");
            }

            item.Student_Name = blog.Student_Name;
            item.Student_City = blog.Student_City;
            item.Student_Gender = blog.Student_Gender;

            var result = await _appDbContext.SaveChangesAsync();
            TempData["IsSuccess"] = result > 0;
            TempData["Message"] = result > 0 ? "Updating Successful." : "Updating Failed.";
            return Redirect("/Student");
        }

        [ActionName("Delete")]
        public async Task<IActionResult> StudentDelete(int id)
        {
            bool isExist = await _appDbContext.Students.AsNoTracking().AnyAsync(x => x.Student_Id == id);
            if (!isExist)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No data found.";
                return Redirect("/Student");
            }

            var item = await _appDbContext.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Student_Id == id);
            if (item == null)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = "No data found.";
                return Redirect("/Student");
            }

            _appDbContext.Students.Remove(item);
            var result = await _appDbContext.SaveChangesAsync();
            TempData["IsSuccess"] = result > 0;
            TempData["Message"] = result > 0 ? "Deleting Successful." : "Deleting Failed.";

            return Redirect("/Student");
        }
    }
}
