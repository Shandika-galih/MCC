/*
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Data;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata.Ecma335;

public class Program
{
    static string connectionString = "Data Source=LAPTOP-254094N7;Database=db_hr;Integrated Security=True;Connect Timeout=30;Encrypt=False";

    static SqlConnection connection;
    public static void Main(string[] args)
    {
        connection = new SqlConnection(connectionString);

        //Getall : Region
        List<Region> regions = GetAllRegion();
        foreach (Region region2 in regions)
        {
            Console.WriteLine("ID: " + region2.Id + ", Name: " + region2.Name);
        }

        //InsertRegion
        Console.WriteLine("Insert");
        Console.Write("Masukan nama region : ");
        string nama = Console.ReadLine();
        int isInsertSucceful = InsertRegion(nama);
        if (isInsertSucceful > 0)
        {
            Console.WriteLine("Data berhasil ditambahkan");
        }
        else
        {
            Console.WriteLine("Data gagal ditambahkan");
        }

        try
        {
            connection.Open();
            Console.WriteLine("connected");
            connection.Close();
        }
        catch
        {
            Console.WriteLine("connection Failed!!");
        }


        // GetByID
        Console.Write("Masukkan ID region yang ingi dicari: ");
        int regionId = Convert.ToInt32(Console.ReadLine());

        Region region = GetRegionById(regionId);
        if (region != null)
        {
            Console.WriteLine("ID: " + region.Id + ", Name: " + region.Name);
        }
        else
        {
            Console.WriteLine("Region not found!");
        }


        // Update
        Console.Write("Masukkan ID region yang ingin diupdate : ");
        int regionIdToUpdate = Convert.ToInt32(Console.ReadLine());

        Console.Write("Masukkan nama baru region : ");
        string updatedName = Console.ReadLine();

        bool isUpdateSuccessful = UpdateRegion(regionIdToUpdate, updatedName);
        if (isUpdateSuccessful)
        {
            Console.WriteLine("Region updated successfully");
        }
        else
        {
            Console.WriteLine("Failed to update region");
        }


        // Delete
        Console.Write("Masukkan ID region yang ingin dihapus : ");
        int regionIdToDelete = Convert.ToInt32(Console.ReadLine());

        bool isDeleteSuccessful = DeleteRegion(regionIdToDelete);
        if (isDeleteSuccessful)
        {
            Console.WriteLine("Region deleted successfully");
        }
        else
        {
            Console.WriteLine("Failed to delete region");
        }
    }
    public static List<Region> GetAllRegion()
    {
        List<Region> regions = new List<Region>();
        try
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM tb_m_regions";

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Region region = new Region();
                region.Id = reader.GetInt32(reader.GetOrdinal("id"));
                region.Name = reader.GetString(reader.GetOrdinal("nama"));

                regions.Add(region);
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
        return regions;
    }

    public static int InsertRegion(string nama)
    {
        int result = 0;
        connection = new SqlConnection(connectionString);

        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();
        try
        {
            ///membuat instan untuk command
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "insert into tb_m_regions (nama) VALUES (@region_name)";
            command.Transaction = transaction;

            //membuat parameter
            SqlParameter pName = new SqlParameter();
            pName.ParameterName = "@region_name";
            pName.Value = nama;
            pName.SqlDbType = SqlDbType.VarChar;

            //membuat parameter ke command
            command.Parameters.Add(pName);

            //menjalankan command
            result = command.ExecuteNonQuery();
            transaction.Commit();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            try { transaction.Rollback(); }
            catch (Exception rollback)
            {
                Console.WriteLine(rollback.Message);
            }
        }
        connection.Close();
        return result;
    }
    public static Region GetRegionById(int id)
    {
        Region region2 = null;
        try
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM tb_m_regions WHERE id = @regionId";

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@regionId";
            parameter.Value = id;
            parameter.SqlDbType = SqlDbType.Int;

            command.Parameters.Add(parameter);

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                region2 = new Region
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    Name = reader.GetString(reader.GetOrdinal("nama"))
                };
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
        return region2;
    }
    public static bool UpdateRegion(int id, string newName)
    {
        bool isUpdateSuccessful = false;
        try
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE tb_m_regions SET nama = @newName WHERE id = @regionId";

            SqlParameter parameterId = new SqlParameter();
            parameterId.ParameterName = "@regionId";
            parameterId.Value = id;
            parameterId.SqlDbType = SqlDbType.Int;

            SqlParameter parameterName = new SqlParameter();
            parameterName.ParameterName = "@newName";
            parameterName.Value = newName;
            parameterName.SqlDbType = SqlDbType.VarChar;

            command.Parameters.Add(parameterId);
            command.Parameters.Add(parameterName);

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
    public static bool DeleteRegion(int id)
    {
        bool isDeleteSuccessful = false;
        try
        {
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM tb_m_regions WHERE id = @regionId";

            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = "@regionId";
            parameter.Value = id;
            parameter.SqlDbType = SqlDbType.Int;

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
public class Region
{
    public int Id { get; set; }
    public string Name { get; set; }
}*/