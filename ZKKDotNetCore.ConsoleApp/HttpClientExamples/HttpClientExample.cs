using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ZKKDotNetCore.ConsoleApp.Models;
using static System.Net.Mime.MediaTypeNames;

namespace ZKKDotNetCore.ConsoleApp.HttpClientExamples
{
    public class HttpClientExample
    {
        public async Task Run()
        {
            //await Create("test","test","test");
            //await UpDate(25, "testnew", "testnew", "testnew");
            await Delete(25);
        }

        private async Task Read()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync("https://localhost:7201/api/Student");

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                //Json to C# object
                //SearilizeObject => C# to json
                //DeserilizeObject => json to C#
                StudentListResponseModel model = JsonConvert.DeserializeObject<StudentListResponseModel>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
            }
        }

        private async Task Edit(int id)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync($"https://localhost:7201/api/Student/{id}");

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                StudentResponseModle model = JsonConvert.DeserializeObject<StudentResponseModle>(jsonStr);
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
            string studentJson = JsonConvert.SerializeObject(studentDataModel);
            HttpContent content = new StringContent(studentJson, Encoding.UTF8, Application.Json);

            HttpClient client = new HttpClient();
            var response = await client.PostAsync($"https://localhost:7201/api/Student",content);

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                StudentResponseModle model = JsonConvert.DeserializeObject<StudentResponseModle>(jsonStr);
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

            string studentJson = JsonConvert.SerializeObject(studentDataModel);
            HttpContent content = new StringContent(studentJson, Encoding.UTF8, Application.Json);

            HttpClient client = new HttpClient();
            var response = await client.PutAsync($"https://localhost:7201/api/Student/{id}", content);

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                StudentResponseModle model = JsonConvert.DeserializeObject<StudentResponseModle>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
            }
        }

        private async Task Delete(int id)
        {
            HttpClient client = new HttpClient();
            var response = await client.DeleteAsync($"https://localhost:7201/api/Student/{id}");

            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                StudentResponseModle model = JsonConvert.DeserializeObject<StudentResponseModle>(jsonStr);
                Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
            }
        }
    }
}
