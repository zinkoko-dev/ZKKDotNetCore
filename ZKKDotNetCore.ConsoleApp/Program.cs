using System;
using System.Threading.Tasks;
using ZKKDotNetCore.ConsoleApp.HttpClientExamples;
using ZKKDotNetCore.ConsoleApp.RestClientExamples;

namespace ZKKDotNetCore.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.WriteLine("Press any key to continue... ");
            Console.ReadKey();

            RestClientExample restClientExample = new RestClientExample();
            await restClientExample.Run();

            //HttpClientExample httpClientExamples = new HttpClientExample();
            //await httpClientExamples.Run();

            //CRUDExample cRUDExample = new CRUDExample();
            //cRUDExample.Run();

            //DapperExample dapperExample = new DapperExample();
            //dapperExample.Run();

            //EFCoreExample eFCoreExample = new EFCoreExample();
            //eFCoreExample.Run();
        }
    }
}