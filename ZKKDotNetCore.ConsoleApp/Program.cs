using System;
using ZKKDotNetCore.ConsoleApp.CRUDExamples;
using ZKKDotNetCore.ConsoleApp.DapperExamples;

namespace ZKKDotNetCore.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //CRUDExample cRUDExample = new CRUDExample();
            //cRUDExample.Run();

            DapperExample dapperExample = new DapperExample();
            dapperExample.Run();

            Console.WriteLine("Press any key to continue... ");
            Console.ReadKey();
        }
    }
}