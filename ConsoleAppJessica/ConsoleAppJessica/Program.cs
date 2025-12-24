using System;
using Microsoft.Data.SqlClient;

namespace ConsoleAppJessica
{
    class Program
    {
        static string connectionString = @"Server=DESKTOP-B07T8M3;TrustServerCertificate=true;Database=jess_db;Trusted_Connection=True;";

        static void Main(string[] args)
        {
            Console.WriteLine("C# CRUD Operations Example");

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\n1. Insert Student");
                Console.WriteLine("2. Read Students");
                Console.WriteLine("3. Update Student");
                Console.WriteLine("4. Delete Student");
                Console.WriteLine("5. Exit");
                Console.Write("Enter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": InsertStudent(); break;
                    case "2": ReadStudents(); break;
                    case "3": UpdateStudent(); break;
                    case "4": DeleteStudent(); break;
                    case "5": exit = true; break;
                    default: Console.WriteLine("Invalid choice"); break;
                }
            }
        }

        static void InsertStudent()
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Age: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Enter Email: ");
            string email = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Students (Name, Age, Email) VALUES (@Name, @Age, @Email)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@Email", email);

                try
                {
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{rows} row(s) inserted.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error (Insert): " + ex.Message);
                }
            }
        }

        static void ReadStudents()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Students";
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    Console.WriteLine("\nID\tName\tAge\tEmail");
                    Console.WriteLine("----------------------------------------");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["Id"]}\t{reader["Name"]}\t{reader["Age"]}\t{reader["Email"]}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error (Read): " + ex.Message);
                }
            }
        }

        static void UpdateStudent()
        {
            Console.Write("Enter Student Id to Update: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Enter New Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter New Age: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("Enter New Email: ");
            string email = Console.ReadLine();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "UPDATE Students SET Name=@Name, Age=@Age, Email=@Email WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Age", age);
                cmd.Parameters.AddWithValue("@Email", email);

                try
                {
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{rows} row(s) updated.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error (Update): " + ex.Message);
                }
            }
        }

        static void DeleteStudent()
        {
            Console.Write("Enter Student Id to Delete: ");
            int id = int.Parse(Console.ReadLine());

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Students WHERE Id=@Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", id);

                try
                {
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    Console.WriteLine($"{rows} row(s) deleted.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error (Delete): " + ex.Message);
                }
            }
        }
    }
}
