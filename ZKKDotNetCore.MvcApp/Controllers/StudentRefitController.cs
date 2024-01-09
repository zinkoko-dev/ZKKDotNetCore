using Microsoft.AspNetCore.Mvc;
using ZKKDotNetCore.MvcApp.Interfaces;

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
            return View(model);
        }
    }
}
