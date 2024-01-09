using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Reflection;
using System.Text;
using ZKKDotNetCore.MvcApp.Interfaces;
using ZKKDotNetCore.MvcApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ZKKDotNetCore.MvcApp.Controllers
{
    public class StudentRefitController : Controller
    {
        private readonly IStudentApi _iStudentApi;

        public StudentRefitController(IStudentApi iStudentApi)
        {
            _iStudentApi = iStudentApi;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _iStudentApi.GetStudents();
            TempData["ControllerName"] = "studentrefit";
            return View(model);
        }

        public IActionResult CreateForm()
        {
            TempData["ControllerName"] = "studentrefit";
            return View("Create");
        }

        public async Task<IActionResult> Create(StudentDataModel reqModel)
        {
            StudentResponseModle model = await _iStudentApi.CreateStudent(reqModel);
            return Redirect("/studentrefit");
        }

        public async Task<IActionResult> Edit(int id)
        {
            StudentResponseModle model = await _iStudentApi.EditStudent(id);
            TempData["ControllerName"] = "studentrefit";
            return View(model);
        }

        public async Task<IActionResult> Update(int id, StudentDataModel reqModel)
        {
            StudentResponseModle model = await _iStudentApi.UpdateStudent(id, reqModel);

            return Redirect("/studentrefit");
        }

        public async Task<IActionResult> Delete(int id)
        {
            StudentResponseModle model = await _iStudentApi.DeleteStudent(id);

            return Redirect("/studentrefit");
        }

    }
}
