using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http;
using System.Reflection;
using System.Text;
using ZKKDotNetCore.MvcApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ZKKDotNetCore.MvcApp.Controllers
{
    public class StudentRestClientController : Controller
    {
        private readonly RestClient _restClient;

        public StudentRestClientController(RestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<IActionResult> Index()
        {
            StudentListResponseModel model = new StudentListResponseModel();
            RestRequest request = new RestRequest("/api/Student", Method.Get);
            //RestResponse response = await client.GetAsync(request);
            RestResponse response = await _restClient.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                //Json to C# object
                //SearilizeObject => C# to json
                //DeserilizeObject => json to C#
                model = JsonConvert.DeserializeObject<StudentListResponseModel>(jsonStr)!;
            }

            TempData["ControllerName"] = "StudentRestClient";
            return View("~/Views/StudentRefit/Index.cshtml", model);
        }

        public IActionResult CreateForm()
        {
            TempData["ControllerName"] = "StudentRestClient";
            return View("~/Views/StudentRefit/Create.cshtml");
        }

        public async Task<IActionResult> Create(StudentDataModel reqModel)
        {
            RestRequest request = new RestRequest("/api/Student", Method.Post);
            request.AddBody(reqModel);
            RestResponse response = await _restClient.ExecuteAsync(request);

            return Redirect("/StudentRestClient");
        }

    }
}
