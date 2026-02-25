using Microsoft.Data.SqlClient;
using System.Data;

namespace First
{
    public class DisconnectedArchitecture
    {
        public static void Main()
        {
            string cs = "Data Source=localhost;Initial Catalog=TrainingDB;Integrated Security=True;TrustServerCertificate=True;";

            SelectData(cs);
            //InsertData(cs);
            UpdateData(cs);
            //DeleteData(cs);
            SelectData(cs);
        }

        static void SelectData(string cs)
        {
            Console.WriteLine("\n=== SELECT (Reading Data) ===");
            string sql = "SELECT EmployeeId, FullName, Department, Salary FROM dbo.Employees";
            DataSet ds = new DataSet();

            using (var con = new SqlConnection(cs))
            using (var cmd = new SqlCommand(sql, con))
            {
                con.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                adapter.Fill(ds);
            }

            ds.WriteXml("TestData.xml");

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                Console.WriteLine($"ID: {row["EmployeeId"]}, Name: {row["FullName"]}, Dept: {row["Department"]}, Salary: {row["Salary"]}");
            }
        }

        static void InsertData(string cs)
        {
            Console.WriteLine("\n=== INSERT (Adding New Record) ===");
            string sql = "SELECT EmployeeId, FullName, Department, Salary FROM dbo.Employees";
            DataSet ds = new DataSet();

            using (var con = new SqlConnection(cs))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                adapter.Fill(ds, "Employees");

                // Add new row to DataTable
                DataTable table = ds.Tables["Employees"];
                DataRow newRow = table.NewRow();
                newRow["FullName"] = "John Doe";
                newRow["Department"] = "IT";
                newRow["Salary"] = 60000;
                table.Rows.Add(newRow);

                // Update database
                int rows = adapter.Update(ds, "Employees");
                Console.WriteLine($"{rows} row(s) inserted");
            }
        }

        static void UpdateData(string cs)
        {
            Console.WriteLine("\n=== UPDATE (Modifying Existing Record) ===");
            string sql = "SELECT EmployeeId, FullName, Department, Salary FROM dbo.Employees";
            DataSet ds = new DataSet();

            Console.Write("Enter Employee ID to update: ");
            int empId = int.Parse(Console.ReadLine() ?? "0");

            using (var con = new SqlConnection(cs))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                adapter.Fill(ds, "Employees");

                DataTable table = ds.Tables["Employees"];

                // Find the employee by ID
                DataRow[] foundRows = table.Select($"EmployeeId = {empId}");

                if (foundRows.Length > 0) +
                {
                    DataRow row = foundRows[0];
                    Console.WriteLine($"\nCurrent Details:");
                    Console.WriteLine($"ID: {row["EmployeeId"]}, Name: {row["FullName"]}, Dept: {row["Department"]}, Salary: {row["Salary"]}");

                    Console.Write("\nUpdate Name? (y/n): ");
                    if (Console.ReadLine()?.ToLower() == "y")
                    {
                        Console.Write("Enter new Name: ");
                        row["FullName"] = Console.ReadLine() ?? "";
                    }

                    Console.Write("Update Department? (y/n): ");
                    if (Console.ReadLine()?.ToLower() == "y")
                    {
                        Console.Write("Enter new Department: ");
                        row["Department"] = Console.ReadLine() ?? "";
                    }

                    Console.Write("Update Salary? (y/n): ");
                    if (Console.ReadLine()?.ToLower() == "y")
                    {
                        Console.Write("Enter new Salary: ");
                        row["Salary"] = decimal.Parse(Console.ReadLine() ?? "0");
                    }

                    // Update database
                    int rows = adapter.Update(ds, "Employees");
                    Console.WriteLine($"{rows} row(s) updated");
                }
                else
                {
                    Console.WriteLine($"Employee with ID {empId} not found!");
                }
            }
        }

        static void DeleteData(string cs)
        {
            Console.WriteLine("\n=== DELETE (Removing Record) ===");
            string sql = "SELECT EmployeeId, FullName, Department, Salary FROM dbo.Employees WHERE FullName = 'John Doe'";
            DataSet ds = new DataSet();

            using (var con = new SqlConnection(cs))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
                SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                adapter.Fill(ds, "Employees");

                DataTable table = ds.Tables["Employees"];

                // Delete all rows that match (John Doe)
                if (table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        Console.WriteLine($"Deleting: {row["FullName"]} from {row["Department"]}");
                        row.Delete();
                    }
                }

                // Update database
                int rows = adapter.Update(ds, "Employees");
                Console.WriteLine($"✅ {rows} row(s) deleted");
            }
        }
    }
}