using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ZKKDotNetCore.MvcApp.Models;

namespace ZKKDotNetCore.MvcApp.Controllers
{
    public class StudentHttpClientController : Controller
    {
        private readonly HttpClient _httpClient;

        public StudentHttpClientController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            StudentListResponseModel model = new StudentListResponseModel();
            var response = await _httpClient.GetAsync("/api/Student");

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<StudentListResponseModel>(jsonStr)!;
            }
            return View("~/Views/StudentRefit/Index.cshtml", model);
        }
    }
}
