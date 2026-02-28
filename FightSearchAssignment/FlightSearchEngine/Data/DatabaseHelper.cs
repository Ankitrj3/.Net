using System.Data;
using Microsoft.Data.SqlClient;
using FlightSearchEngine.Models;

namespace FlightSearchEngine.Data;

public class DatabaseHelper
{
    private readonly string _connectionString;

    public DatabaseHelper(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found in appsettings.json.");
    }

    public async Task<List<string>> GetSourcesAsync()
    {
        var sources = new List<string>();

        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand("sp_GetSources", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
            sources.Add(reader["Source"].ToString()!);

        return sources;
    }

    public async Task<List<string>> GetDestinationsAsync()
    {
        var destinations = new List<string>();

        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand("sp_GetDestinations", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
            destinations.Add(reader["Destination"].ToString()!);

        return destinations;
    }

    public async Task<List<FlightResult>> SearchFlightsAsync(string source, string destination, int persons)
    {
        var list = new List<FlightResult>();

        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand("sp_SearchFlights", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Source", source);
        cmd.Parameters.AddWithValue("@Destination", destination);
        cmd.Parameters.AddWithValue("@Persons", persons);

        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(new FlightResult
            {
                FlightId = (int)reader["FlightId"],
                FlightName = reader["FlightName"].ToString()!,
                FlightType = reader["FlightType"].ToString()!,
                Source = reader["Source"].ToString()!,
                Destination = reader["Destination"].ToString()!,
                TotalCost = Convert.ToDecimal(reader["TotalCost"])
            });
        }

        return list;
    }

    public async Task<List<FlightHotelResult>> SearchFlightsWithHotelsAsync(string source, string destination, int persons)
    {
        var list = new List<FlightHotelResult>();

        using var conn = new SqlConnection(_connectionString);
        using var cmd = new SqlCommand("sp_SearchFlightsWithHotels", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Source", source);
        cmd.Parameters.AddWithValue("@Destination", destination);
        cmd.Parameters.AddWithValue("@Persons", persons);

        await conn.OpenAsync();
        using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            list.Add(new FlightHotelResult
            {
                FlightId = (int)reader["FlightId"],
                FlightName = reader["FlightName"].ToString()!,
                Source = reader["Source"].ToString()!,
                Destination = reader["Destination"].ToString()!,
                HotelName = reader["HotelName"].ToString()!,
                TotalCost = Convert.ToDecimal(reader["TotalCost"])
            });
        }

        return list;
    }
}