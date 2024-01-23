using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ZKKDotNetCore.ConsoleApp.CRUDExamples
{
    public class CRUDExample
    {

        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = ".",
            InitialCatalog = "ZKKDotNetCore",
            UserID = "sa",
            Password = "sasa"
        };

        public void Run()
        {
            //Read

            Read();

            //Pagination

            Read(1);

            //Create Student

            Console.WriteLine("You want to add student ? Please enter \"y\" or \"n\"");
            string userInput = Console.ReadLine();

            if (userInput.Equals("y"))
            {
                Console.Write("Enter Student Name : ");
                string name = Console.ReadLine();

                Console.Write("Enter Student City : ");
                string city = Console.ReadLine();

                Console.Write("Enter Student Gender : ");
                string gender = Console.ReadLine();

                Create(name, city, gender);
            }

            //Update Student

            Console.WriteLine("You want to update student? Please enter \"y\" or \"n\"");
            string userInput2 = Console.ReadLine() ;

            if (userInput2.Equals("y"))
            {
                Console.Write("Enter Student Id : ");
                string idStr = Console.ReadLine();
                int id = int.Parse(idStr);

                Console.Write("Enter Student Name : ");
                string name = Console.ReadLine();

                Console.Write("Enter Student City : ");
                string city = Console.ReadLine();

                Console.Write("Enter Student Gender : ");
                string gender = Console.ReadLine();

                Update(id, name, city, gender);
            }

            //Delete Student

            Console.WriteLine("You want to delete student? Please enter \"y\" or \"n\"");
            string userInput3 = Console.ReadLine();

            if (userInput3.Equals("y"))
            {
                Console.Write("Enter Student Id : ");
                string idStr = Console.ReadLine();
                int id = int.Parse(idStr);

                Delete(id);
            }
        }

        private void Read()
        {
            string query = "select * from tbl_student";

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["Student_Id"].ToString());
                Console.WriteLine(dr["Student_Name"].ToString());
                Console.WriteLine(dr["Student_City"].ToString());
                Console.WriteLine(dr["Student_Gender"].ToString());
            }
        }

        private void Read(int pageNo, int pageSize = 10)
        {
            int skipRowCount = (pageNo - 1) * pageSize;

            string query = @"SELECT * FROM Tbl_Student ORDER BY Student_Id DESC
                             OFFSET @SkipRowCount ROWS FETCH NEXT @PageSize ROWS ONLY";

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@SkipRowCount", skipRowCount);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["Student_Id"].ToString());
                Console.WriteLine(dr["Student_Name"].ToString());
                Console.WriteLine(dr["Student_City"].ToString());
                Console.WriteLine(dr["Student_Gender"].ToString());
            }
        }

        private void Edit(int id)
        {
            string query = "SELECT * FROM [Tbl_Student] WHERE [Student_Id] = @Student_Id";

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Student_Id", id);

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No Data found");
                return;
            }

            DataRow dr = dt.Rows[0];
            Console.WriteLine(dr["Student_Id"].ToString());
            Console.WriteLine(dr["Student_Name"].ToString());
            Console.WriteLine(dr["Student_City"].ToString());
            Console.WriteLine(dr["Student_Gender"].ToString());

        }

        private void Create(string name, string city, string gender)
        {
            string query = $@"insert into [dbo].Tbl_Student
                            ([Student_Name],[Student_City],[Student_Gender])
                            values
                            (@Student_Name, @Student_City, @Student_Gender);";

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@Student_Name", name);
            cmd.Parameters.AddWithValue("@Student_City", city);
            cmd.Parameters.AddWithValue("@Student_Gender", gender);

            int result = cmd.ExecuteNonQuery();

            string message = result > 0 ? "Saving Successful !!" : "Error While Saving !!";

            connection.Close();

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

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@Student_Id", id);
            cmd.Parameters.AddWithValue("@Student_Name", name);
            cmd.Parameters.AddWithValue("@Student_City", city);
            cmd.Parameters.AddWithValue("@Student_Gender", gender);

            int result = cmd.ExecuteNonQuery();

            string message = result > 0 ? "Update Successful !!" : "Error While Update !!";

            connection.Close();

            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            string query = @"DELETE FROM [dbo].[Tbl_Student] WHERE [Student_Id] = @Student_Id";

            SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Student_Id", id);

            int result = cmd.ExecuteNonQuery();

            string message = result > 0 ? "Delete Successful !!" : "Error While Delete !!";

            connection.Close();

            Console.WriteLine(message);
        }
    }
}