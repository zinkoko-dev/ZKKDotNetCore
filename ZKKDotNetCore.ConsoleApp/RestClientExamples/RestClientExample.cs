using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ZKKDotNetCore.ConsoleApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ZKKDotNetCore.ConsoleApp.RestClientExamples
{
    public class RestClientExample
    {
        public async Task Run()
        {
            //await Read();
            //await Create("test","test","test");
            //await Edit(26);
            //await UpDate(45, "testnew", "testnew", "testnew");
            await Delete(26);
        }

        private async Task Read()
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest("https://localhost:7201/api/Student", Method.Get);
            //RestResponse response = await client.GetAsync(request);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content;
                //Json to C# object
                //SearilizeObject => C# to json
                //DeserilizeObject => json to C#
                StudentListResponseModel model = JsonConvert.DeserializeObject<StudentListResponseModel>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
            }
        }

        private async Task Create(string name, string city, string gender)
        {
            StudentDataModel studentDataModel = new StudentDataModel()
            {
                Student_Name = name,
                Student_City = city,
                Student_Gender = gender
            };

            RestClient client = new RestClient();
            RestRequest request = new RestRequest("https://localhost:7201/api/Student", Method.Post);
            request.AddBody(studentDataModel);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content;
                StudentResponseModle model = JsonConvert.DeserializeObject<StudentResponseModle>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
            }
        }

        private async Task Edit(int id)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"https://localhost:7201/api/Student/{id}", Method.Get);
            //RestResponse response = await client.GetAsync(request);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content;
                //Json to C# object
                //SearilizeObject => C# to json
                //DeserilizeObject => json to C#
                StudentListResponseModel model = JsonConvert.DeserializeObject<StudentListResponseModel>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
            }
        }

        private async Task UpDate(int id, string name, string city, string gender)
        {
            StudentDataModel studentDataModel = new StudentDataModel()
            {
                Student_Name = name,
                Student_City = city,
                Student_Gender = gender
            };

            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"https://localhost:7201/api/Student/{id}", Method.Put);
            request.AddBody(studentDataModel);
            //RestResponse response = await client.GetAsync(request);
            RestResponse response = await client.ExecuteAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content;
                StudentResponseModle model = JsonConvert.DeserializeObject<StudentResponseModle>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
            }
        }

        private async Task Delete(int id)
        {
            RestClient client = new RestClient();
            RestRequest request = new RestRequest($"https://localhost:7201/api/Student/{id}", Method.Delete);
            //RestResponse response = await client.GetAsync(request);
            RestResponse response = await client.ExecuteAsync(request);
            
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content;
                StudentResponseModle model = JsonConvert.DeserializeObject<StudentResponseModle>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
            }
        }
    }
}