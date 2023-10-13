using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZKKDotNetCore.ConsoleApp.Models;

namespace ZKKDotNetCore.ConsoleApp.EFCoreExamples
{
    public class EFCoreExample
    {
        public void Run()
        {
            //Read();
            //Create("Zin Ko Ko", "Ygn", "Male");
            //Edit(10);
            //Update(12, "test name", "test city", "test gender");
            //Delete(12);
        }

        private void Read()
        {
            AppDbContext db = new AppDbContext();
            var lst = db.Students.OrderByDescending(x => x.Student_Id).ToList();

            foreach (var item in lst)
            {
                Console.WriteLine(item.Student_Id);
                Console.WriteLine(item.Student_Name);
                Console.WriteLine(item.Student_City);
                Console.WriteLine(item.Student_Gender);
            }
        }

        private void Create(string name, string city, string gender)
        {
            StudentDataModel student = new StudentDataModel()
            {
                Student_Name = name,
                Student_City = city,
                Student_Gender = gender
            };

            AppDbContext db = new AppDbContext();
            db.Students.Add(student);
            var result = db.SaveChanges();

            string message = result > 0 ? "Save Successful !!" : "Error Successful !!";
            Console.WriteLine(message);
        }

        private void Edit(int id)
        {
            AppDbContext db = new AppDbContext();
            //db.Students.Where(x => x.Student_Id == id).FirstOrDefault();
            StudentDataModel item = db.Students.FirstOrDefault(x => x.Student_Id == id);
            //if (item is null)
            if (item == null)
            {
                Console.WriteLine("Data not found !!");
                return;
            }

            Console.WriteLine(item.Student_Id);
            Console.WriteLine(item.Student_Name);
            Console.WriteLine(item.Student_City);
            Console.WriteLine(item.Student_Gender);
        }

        private void Update(int id, string name, string city, string gender)
        {
            AppDbContext db = new AppDbContext();
            StudentDataModel item = db.Students.FirstOrDefault(x => x.Student_Id == id);

            if (item == null)
            {
                Console.WriteLine("No data found !!");
                return;
            }

            item.Student_Name = name;
            item.Student_City = city;
            item.Student_Gender = gender;

            var result = db.SaveChanges();
            string message = result > 0 ? "Update Successful !!" : "Update Error!!";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            AppDbContext db = new AppDbContext();
            StudentDataModel item = db.Students.FirstOrDefault(x => x.Student_Id == id);

            if (item == null)
            {
                Console.WriteLine("No Data Found");
                return;
            }

            db.Students.Remove(item);
            var result = db.SaveChanges();

            String message = result > 0 ? "Delete Successful !!" : "Delete Error !!";
            Console.WriteLine(message);
        }
    }
}