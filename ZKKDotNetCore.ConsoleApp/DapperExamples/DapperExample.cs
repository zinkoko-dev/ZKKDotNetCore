using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using ZKKDotNetCore.ConsoleApp.Models;

namespace ZKKDotNetCore.ConsoleApp.DapperExamples
{
    public class DapperExample
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "ZKKDotNetCore",
            UserID = "sa",
            Password = "sasa",
            TrustServerCertificate = true
        };

        public void Run()
        {
            //Read();
            Read(2, 5);
            //Create("Jhon Wick", "Ygn", "Male");
            //Edit(9);
            //Update(9, "Zin Ko Ko", "Pathein", "Male");
            //Delete(9);
        }

        private void Read()
        {
            string query = "SELECT * FROM Tbl_Student order by Student_Id desc";
            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            List<StudentDataModel> lst = db.Query<StudentDataModel>(query).ToList();

            foreach (var item in lst)
            {
                Console.WriteLine(item.Student_Id);
                Console.WriteLine(item.Student_Name);
                Console.WriteLine(item.Student_City);
                Console.WriteLine(item.Student_Gender);
            }
        }

        private void Read(int pageNo, int pageSize = 10)
        {
            int skipRowCount = (pageNo - 1) * pageSize;

            string query = @"SELECT * FROM Tbl_Student ORDER BY Student_Id DESC
                             OFFSET @SkipRowCount ROWS FETCH NEXT @PageSize ROWS ONLY";

            PaginationDataModel blog = new PaginationDataModel()
            {
                pageSize = pageSize,
                skipRowCount = skipRowCount
            };

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);

            List<StudentDataModel> lst = db.Query<StudentDataModel>(query, blog).ToList();

            foreach (var item in lst)
            {
                Console.WriteLine(item.Student_Id);
                Console.WriteLine(item.Student_Name);
                Console.WriteLine(item.Student_City);
                Console.WriteLine(item.Student_Gender);
            }
        }

        private void Edit(int id)
        {
            string query = "SELECT * FROM [Tbl_Student] WHERE [Student_Id] = @Student_Id";

            StudentDataModel blog = new StudentDataModel()
            {
                Student_Id = id,
            };

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            StudentDataModel item = db.Query<StudentDataModel>(query, blog).FirstOrDefault();

            if (item == null)
            {
                Console.WriteLine("No Data found");
                return;
            }

            Console.WriteLine(item.Student_Id);
            Console.WriteLine(item.Student_Name);
            Console.WriteLine(item.Student_City);
            Console.WriteLine(item.Student_Gender);
        }

        public void Create(string name, string city, string gender)
        {
            string query = $@"insert into [dbo].[Tbl_Student]
                            ([Student_Name],[Student_City],[Student_Gender])
                            values
                            (@Student_Name, @Student_City, @Student_Gender);";

            StudentDataModel blog = new StudentDataModel()
            {
                Student_Name = name,
                Student_City = city,
                Student_Gender = gender,
            };

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            var result = db.Execute(query, blog);
            string message = result > 0 ? "Saving Successful !!" : "Saving Fail !!";

            Console.WriteLine(message);
        }

        private void Update(int id, string name, string city, string gender)
        {
            string query = @"UPDATE [dbo].[Tbl_Student]
                             SET
                             [Student_Name] = @Student_Name,
                             [Student_City] = @Student_City,
                             [Student_Gender] = @Student_Gender
                             WHERE
                             [Student_Id] = @Student_Id";

            StudentDataModel blog = new StudentDataModel()
            {
                Student_Id = id,
                Student_Name = name,
                Student_City = city,
                Student_Gender = gender,
            };

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            var result = db.Execute(query, blog);

            string message = result > 0 ? "Update Successful !!" : "Error While Update !!";

            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Student] WHERE [Student_Id] = @Student_Id";

            StudentDataModel item = new StudentDataModel()
            {
                Student_Id = id,
            };

            using IDbConnection db = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            var result = db.Execute(query, item);

            string message = result > 0 ? "Delete Successful !!" : "Error While Delete !!";

            Console.WriteLine(message);
        }
    }
}
