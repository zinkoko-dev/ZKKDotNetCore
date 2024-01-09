using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;
using ZKKDotNetCore.MvcApp.Models;
using static System.Net.Mime.MediaTypeNames;

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

            TempData["ControllerName"] = "studenthttpclient";
            return View("~/Views/StudentRefit/Index.cshtml", model);
        }

        public IActionResult CreateForm()
        {
            TempData["ControllerName"] = "studenthttpclient";
            return View("~/Views/StudentRefit/Create.cshtml");
        }

        public async Task<IActionResult> Create(StudentDataModel reqModel)
        {
            string studentJson = JsonConvert.SerializeObject(reqModel);
            HttpContent content = new StringContent(studentJson, Encoding.UTF8, Application.Json);

            var response = await _httpClient.PostAsync($"/api/Student", content);

            return Redirect("/StudentHttpClient");
        }

        public async Task<IActionResult> Edit(int id)
        {
            StudentResponseModle model = new StudentResponseModle();
            var response = await _httpClient.GetAsync($"/api/Student/{id}");

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<StudentResponseModle>(jsonStr)!;
            }

            TempData["ControllerName"] = "studenthttpclient";
            return View("~/Views/StudentRefit/Edit.cshtml", model);
        }

        public async Task<IActionResult> Update(int id, StudentDataModel reqModel)
        {
            string studentJson = JsonConvert.SerializeObject(reqModel);
            HttpContent content = new StringContent(studentJson, Encoding.UTF8, Application.Json);

            var response = await _httpClient.PutAsync($"/api/Student/{id}", content);

            return Redirect("/StudentHttpClient");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/Student/{id}");

            return Redirect("/StudentHttpClient");
        }
    }
}
