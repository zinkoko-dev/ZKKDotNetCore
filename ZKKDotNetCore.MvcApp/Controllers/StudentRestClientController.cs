using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using ZKKDotNetCore.MvcApp.Models;

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
            return View("~/Views/StudentRefit/Index.cshtml", model);
        }
    }
}
