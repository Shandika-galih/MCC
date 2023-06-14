using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace database;

public class Program
{
    static string connectionString = "Data Source=LAPTOP-254094N7;Database=db_hr;Integrated Security=True;Connect Timeout=30;Encrypt=False";
    static SqlConnection connection;

    public static void Main(string[] args)
    {
        connection = new SqlConnection(connectionString);

        // GetAllCountries
        List<Country> countries = GetAllCountries();
        foreach (Country country2 in countries)
        {
            Console.WriteLine("ID: " + country2.Id + ", Name: " + country2.Name + ", Region ID: " + country2.RegionId);
        }

        // InsertCountry
        Console.WriteLine("Insert");
        Console.Write("Enter country name: ");
        string countryName = Console.ReadLine();
        Console.Write("Enter country ID: ");
        string countryID = Console.ReadLine();
        Console.Write("Enter region ID: ");
        int regionId = Convert.ToInt32(Console.ReadLine());

        int insertResult = InsertCountry(countryID, countryName, regionId);
        if (insertResult > 0)
        {
            Console.WriteLine("Data successfully inserted");
        }
        else
        {
            Console.WriteLine("Failed to insert data");
        }

        // GetCountryById
        Console.Write("Enter country ID: ");
        string countryId = Console.ReadLine();
        Country country = GetCountryById(countryId);
        if (country != null)
        {
            Console.WriteLine("ID: " + country.Id + ", Name: " + country.Name + ", Region ID: " + country.RegionId);
        }
        else
        {
            Console.WriteLine("Country not found!");
        }

        // UpdateCountry
        Console.Write("Enter country ID to update: ");
        string countryIdToUpdate = Console.ReadLine();
        Console.Write("Enter new country name: ");
        string updatedCountryName = Console.ReadLine();
        Console.Write("Enter new region ID: ");
        int updatedRegionId = Convert.ToInt32(Console.ReadLine());
        bool isUpdateSuccessful = UpdateCountry(countryIdToUpdate, updatedCountryName, updatedRegionId);
        if (isUpdateSuccessful)
        {
            Console.WriteLine("Country updated successfully");
        }
        else
        {
            Console.WriteLine("Failed to update country");
        }

        // DeleteCountry
        Console.Write("Enter country ID to delete: ");
        string countryIdToDelete = Console.ReadLine();
        bool isDeleteSuccessful = DeleteCountry(countryIdToDelete);
        if (isDeleteSuccessful)
        {
            Console.WriteLine("Country deleted successfully");
        }
        else
        {
            Console.WriteLine("Failed to delete country");
        }
    }

    public static List<Country> GetAllCountries()
    {
        List<Country> countries = new List<Country>();
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM tb_m_countries";
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Country country = new Country();
                country.Id = reader.GetString(reader.GetOrdinal("id"));
                country.Name = reader.GetString(reader.GetOrdinal("nama"));
                country.RegionId = reader.GetInt32(reader.GetOrdinal("region_id"));
                countries.Add(country);
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
        return countries;
    }

    public static int InsertCountry(string countryId, string countryName, int regionId)
    {
        int result = 0;
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO tb_m_countries (id, nama, region_id) VALUES (@countryId, @countryName, @regionId)";
            SqlParameter parameterName = new SqlParameter();
            parameterName.ParameterName = "@countryName";
            parameterName.Value = countryName;
            SqlParameter parameterCountryID = new SqlParameter();
            parameterCountryID.ParameterName = "@countryId";
            parameterCountryID.Value = countryId;
            SqlParameter parameterRegionId = new SqlParameter();
            parameterRegionId.ParameterName = "@regionId";
            parameterRegionId.Value = regionId;
            parameterRegionId.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(parameterCountryID);
            command.Parameters.Add(parameterName);
            command.Parameters.Add(parameterRegionId);
            result = command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return result;
    }

    public static Country GetCountryById(string id)
    {
        Country country = null;
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM tb_m_countries WHERE id = @countryId";
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@countryId";
            parameter.Value = id;
            parameter.SqlDbType = SqlDbType.Char;
            command.Parameters.Add(parameter);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                country = new Country();
                country.Id = reader.GetString(reader.GetOrdinal("id"));
                country.Name = reader.GetString(reader.GetOrdinal("nama"));
                country.RegionId = reader.GetInt32(reader.GetOrdinal("region_id"));
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
        return country;
    }

    public static bool UpdateCountry(string id, string newName, int newRegionId)
    {
        bool isUpdateSuccessful = false;
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE tb_m_countries SET nama = @newName, region_id = @newRegionId WHERE id = @countryId";
            SqlParameter parameterId = new SqlParameter();
            parameterId.ParameterName = "@countryId";
            parameterId.Value = id;
            parameterId.SqlDbType = SqlDbType.Char;
            SqlParameter parameterName = new SqlParameter();
            parameterName.ParameterName = "@newName";
            parameterName.Value = newName;
            parameterName.SqlDbType = SqlDbType.VarChar;
            SqlParameter parameterRegionId = new SqlParameter();
            parameterRegionId.ParameterName = "@newRegionId";
            parameterRegionId.Value = newRegionId;
            parameterRegionId.SqlDbType = SqlDbType.Int;
            command.Parameters.Add(parameterId);
            command.Parameters.Add(parameterName);
            command.Parameters.Add(parameterRegionId);
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                isUpdateSuccessful = true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return isUpdateSuccessful;
    }

    public static bool DeleteCountry(string id)
    {
        bool isDeleteSuccessful = false;
        try
        {
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM tb_m_countries WHERE id = @countryId";
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@countryId";
            parameter.Value = id;
            parameter.SqlDbType = SqlDbType.Char;
            command.Parameters.Add(parameter);
            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                isDeleteSuccessful = true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            connection.Close();
        }
        return isDeleteSuccessful;
    }
}

public class Country
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int RegionId { get; set; }
}
