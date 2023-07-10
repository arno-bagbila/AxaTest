using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace AxaTest.DAL;

public class CityDAL : ICityDAL
{
    private readonly string? _connectionString;  
    
    public CityDAL(IConfiguration iconfiguration)
    {
        _connectionString = iconfiguration.GetConnectionString("Default");
    }
    
    public Task<List<string?>> GetCities(string searchString)
    {
        List<string?> cities = new();
        using SqlConnection con = new(_connectionString);
        using SqlCommand cmd = new SqlCommand("SP_GET_CITIES_BY_NAME", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Input", SqlDbType.NVarChar).Value = searchString;
        con.Open();
        using SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            cities.Add(reader["Name"].ToString());
        }
        
        return Task.FromResult(cities);
    }
}