using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZKKDotNetCore.MvcApp.EFDbContext;
using ZKKDotNetCore.MvcApp.Models;

namespace ZKKDotNetCore.MvcApp.Controllers
{
    public class StudentAjaxController : Controller
    {

        private readonly AppDbContext _appDbContext;

        public StudentAjaxController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [ActionName("List")]
        public async Task<IActionResult> StudentList(int pageNo = 1, int pageSize = 10)
        {
            StudentDataResponseModel model = new StudentDataResponseModel();
            List<StudentDataModel> lst = _appDbContext.Students.AsNoTracking()
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            int rowCount = await _appDbContext.Students.CountAsync();
            int pageCount = (rowCount / pageSize) + (rowCount % pageSize == 0 ? 0 : 1);
            PageSettingModel pageSetting = new PageSettingModel(pageNo, pageSize, pageCount, "/studentajax/list");
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

            MessageModel model = new MessageModel(message, result>0);
            return Json(model);
        }

        [ActionName("Edit")]
        public async Task<IActionResult> StudentEdit(int id)
        {
            if(!await _appDbContext.Students.AsNoTracking().AnyAsync(x => x.Student_Id == id))
            {
                TempData["Message"] = "No Data Found";
                TempData["isSuccess"] = false;
                return Redirect("/studentajax/list");
            }
            var student = await _appDbContext.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Student_Id == id);
            return View("StudentEdit", student);
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> StudentUpdate(StudentDataModel reqModel)
        {
            string message;
            MessageModel model;
            if (!await _appDbContext.Students.AsNoTracking().AnyAsync(x => x.Student_Id == reqModel.Student_Id) || reqModel == null)
            {
                message = "No Data Found";
                model = new MessageModel(message, false);
                return Redirect("/studentajax/list");
            }
            var stu = await _appDbContext.Students.FirstOrDefaultAsync(x => x.Student_Id == reqModel.Student_Id);
            if (stu is not null)
            {
                stu.Student_Name = reqModel.Student_Name;
                stu.Student_City = reqModel.Student_City;
                stu.Student_Gender = reqModel.Student_Gender;
            }
            int result = _appDbContext.SaveChanges();
            message = result > 0 ? "Saving Successful !" : "Saving Error !";
            model = new MessageModel(message, result > 0);
            return Json(model);
        }

        [ActionName("Delete")]
        public async Task<IActionResult> StudentDelete(StudentDataModel reqModel)
        {
            string message;
            MessageModel model;
            if (!await _appDbContext.Students.AsNoTracking().AnyAsync(x => x.Student_Id == reqModel.Student_Id))
            {
                message = "No Data Found";
                model = new MessageModel(message, false);
                return Redirect("/studentajax/list");
            }
            var student = await _appDbContext.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Student_Id == reqModel.Student_Id);
            if (student is null)
            {
                message = "No Data Found";
                model = new MessageModel(message, false);
                return Redirect("/studentajax/list");
            }

            _appDbContext.Remove(student);
            var result = _appDbContext.SaveChanges();
            message = result > 0 ? "Delete Successful !" : "Delete Error !";
            model = new MessageModel(message, result > 0);
            return Json(model);
        }
    }
}
