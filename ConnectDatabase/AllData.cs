/*using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

public class Program
{
    static string connectionString = "Data Source=LAPTOP-254094N7;Database=db_hr;Integrated Security=True;Connect Timeout=30;Encrypt=False";
    static SqlConnection connection;

    public static void Main(string[] args)
    {
        connection = new SqlConnection(connectionString);

        List<Department> departments = GetAllDepartments();
        foreach (Department department in departments)
        {
            Console.WriteLine("ID: " + department.Id + ", Name: " + department.Name + ", Location ID: " + department.LocationId + ", Manager ID: " + department.ManagerId);
        }

        List<Employee> employees = GetAllEmployees();
        foreach (Employee employee in employees)
        {
            Console.WriteLine("ID: " + employee.Id + ", First Name: " + employee.FirstName + ", Last Name: " + employee.LastName + ", Email: " + employee.Email + ", Phone Number: " + employee.PhoneNumber + ", Hire Date: " + employee.HireDate.ToString("yyyy-MM-dd") + ", Salary: " + employee.Salary + ", Commission: " + employee.Commission + ", Manager ID: " + employee.ManagerId + ", Job ID: " + employee.JobId + ", Department ID: " + employee.DepartmentId);
        }


        List<Job> jobs = GetAllJobs();
        foreach (Job job in jobs)
        {
            Console.WriteLine("ID: " + job.Id + ", Title: " + job.Title);
        }

        List<Location> locations = GetAllLocations();
        foreach (Location location in locations)
        {
            Console.WriteLine("ID: " + location.Id + ", Street Address: " + location.StreetAddress + ", Postal Code: " + location.PostalCode + ", City: " + location.City + ", State Province: " + location.StateProvince + ", Country ID: " + location.CountryId);
        }


        List<History> histories = GetAllHistories();
        foreach (History history in histories)
        {
            Console.WriteLine("Start Date: " + history.StartDate + ", Employee ID: " + history.EmployeeId + ", End Date: " + history.EndDate + ", Department ID: " + history.DepartmentId + ", Job ID: " + history.JobId);
        }
    }

    public static List<Department> GetAllDepartments()
    {
        List<Department> departments = new List<Department>();
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM tb_m_departements ORDER BY id";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Department department = new Department();
                department.Id = reader.GetInt32(reader.GetOrdinal("id"));
                department.Name = reader.GetString(reader.GetOrdinal("nama"));
                department.LocationId = reader.GetInt32(reader.GetOrdinal("location_id"));
                department.ManagerId = reader.GetInt32(reader.GetOrdinal("manager_id"));
                departments.Add(department);
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return departments;
    }

    public static List<Employee> GetAllEmployees()
    {
        List<Employee> employees = new List<Employee>();
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM tb_m_employees ORDER BY id";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Employee employee = new Employee();
                employee.Id = reader.GetInt32(reader.GetOrdinal("id"));
                employee.FirstName = reader.GetString(reader.GetOrdinal("first_name"));
                employee.LastName = reader.GetString(reader.GetOrdinal("last_name"));
                employee.Email = reader.GetString(reader.GetOrdinal("email"));
                employee.PhoneNumber = reader.GetString(reader.GetOrdinal("phone_number"));
                employee.HireDate = reader.GetDateTime(reader.GetOrdinal("hire_date"));
                employee.Salary = reader.GetInt32(reader.GetOrdinal("salary"));

                employee.ManagerId = reader.GetInt32(reader.GetOrdinal("manager_id"));
                employee.JobId = reader.GetString(reader.GetOrdinal("job_id"));
                employee.DepartmentId = reader.GetInt32(reader.GetOrdinal("departement_id"));
                employees.Add(employee);
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return employees;
    }

    public static List<Job> GetAllJobs()
    {
        List<Job> jobs = new List<Job>();
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM tb_m_jobs ORDER BY id";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Job job = new Job();
                job.Id = reader.GetString(reader.GetOrdinal("id"));
                job.Title = reader.GetString(reader.GetOrdinal("title"));
                jobs.Add(job);
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return jobs;
    }

    public static List<Location> GetAllLocations()
    {
        List<Location> locations = new List<Location>();
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM tb_m_location ORDER BY id";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Location location = new Location();
                location.Id = reader.GetInt32(reader.GetOrdinal("id"));
                location.StreetAddress = reader.GetString(reader.GetOrdinal("street_address"));
                location.PostalCode = reader.GetString(reader.GetOrdinal("postal_code"));
                location.City = reader.GetString(reader.GetOrdinal("city"));
                location.StateProvince = reader.GetString(reader.GetOrdinal("state_province"));
                location.CountryId = reader.GetString(reader.GetOrdinal("country_id"));
                locations.Add(location);
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return locations;
    }

    public static List<History> GetAllHistories()
    {
        List<History> histories = new List<History>();
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM tb_tr_histories ORDER BY start_date";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                History history = new History();
                history.StartDate = reader.GetDateTime(reader.GetOrdinal("start_date"));
                history.EmployeeId = reader.GetInt32(reader.GetOrdinal("employee_id"));
                history.EndDate = reader.GetDateTime(reader.GetOrdinal("end_date"));
                history.DepartmentId = reader.GetInt32(reader.GetOrdinal("departement_id"));
                history.JobId = reader.GetString(reader.GetOrdinal("job_id"));
                histories.Add(history);
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return histories;
    }
}

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int LocationId { get; set; }
    public int ManagerId { get; set; }
}

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime HireDate { get; set; }
    public int Salary { get; set; }
    public decimal Commission { get; set; }
    public int ManagerId { get; set; }
    public string JobId { get; set; }
    public int DepartmentId { get; set; }
}

public class Job
{
    public string Id { get; set; }
    public string Title { get; set; }
}

public class Location
{
    public int Id { get; set; }
    public string StreetAddress { get; set; }
    public string PostalCode { get; set; }
    public string City { get; set; }
    public string StateProvince { get; set; }
    public string CountryId { get; set; }
}
public class History
{
    public DateTime StartDate { get; set; }
    public int EmployeeId { get; set; }
    public DateTime EndDate { get; set; }
    public int DepartmentId { get; set; }
    public string JobId { get; set; }
}
*/