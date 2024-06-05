using Npgsql;
using System.Text;
using Vehicles.Common;
using Vehicles.Model;
using Vehicles.Repository.Common;

namespace Vehicles.Repository;

public class VehicleRepository : IVehicleRepository
{
    private readonly string _connectionString;
    public VehicleRepository(string connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task<List<Vehicle>> GetAllAsync(Filtering filter, Paging paging, Sorting sorting)
    {
        List<Vehicle> vehicles = new List<Vehicle>();

        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            using var command = BuildQuery(filter);
            command.Connection = connection;

            connection.Open();

            var readerAsync = await command.ExecuteReaderAsync();
            if (readerAsync.HasRows)
            {
                while (await readerAsync.ReadAsync())
                {
                    var vehicle = new Vehicle();

                    vehicle.Id = readerAsync.GetGuid(0);
                    vehicle.MakeId = await readerAsync.IsDBNullAsync(1) ? null : readerAsync.GetGuid(1);
                    vehicle.Model = await readerAsync.IsDBNullAsync(2) ? null : readerAsync.GetString(2);
                    vehicle.Color = readerAsync.GetString(3);
                    vehicle.Year = await readerAsync.IsDBNullAsync(4) ? null : readerAsync.GetDateTime(4);
                    vehicle.ForSale = readerAsync.GetBoolean(5);

                    if (!await readerAsync.IsDBNullAsync(1))
                    {
                        vehicle.Make = new Make();

                        vehicle.Make.Id = readerAsync.GetGuid(6);
                        vehicle.Make.Name = readerAsync.GetString(7);
                    }
                    vehicles.Add(vehicle);
                }
            }
            connection.Close();

            return vehicles;
        }
        catch (Exception ex)
        {
            throw new Exception("Data access error: " + ex.Message);
        }
    }

    public async Task<Vehicle?> GetAsync(Guid id)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);

            var commandText = "SELECT * FROM \"Vehicle\" AS v LEFT JOIN \"Make\" AS m ON v.\"MakeId\" = m.\"Id\" WHERE v.\"Id\" = @Id";

            await using var command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@Id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

            await connection.OpenAsync();

            var readerAsync = await command.ExecuteReaderAsync();

            if (readerAsync.HasRows)
            {

                Vehicle vehicle = new Vehicle();

                await readerAsync.ReadAsync();

                vehicle.Id = readerAsync.GetGuid(0);
                vehicle.MakeId = await readerAsync.IsDBNullAsync(1) ? null : readerAsync.GetGuid(1);
                vehicle.Model = await readerAsync.IsDBNullAsync(2) ? null : readerAsync.GetString(2);
                vehicle.Color = readerAsync.GetString(3);
                vehicle.Year = await readerAsync.IsDBNullAsync(4) ? null : readerAsync.GetDateTime(4);
                vehicle.ForSale = readerAsync.GetBoolean(5);

                if (!await readerAsync.IsDBNullAsync(1))
                {
                    vehicle.Make = new Make();

                    vehicle.Make.Id = readerAsync.GetGuid(6);
                    vehicle.Make.Name = readerAsync.GetString(7);
                }

                await connection.CloseAsync();
                return vehicle;
            }

            await connection.CloseAsync();
            return null;

        }
        catch (Exception ex)
        {
            throw new Exception("Data access error: " + ex.Message);
        }
    }

    public async Task<bool> InsertAsync(Vehicle vehicle)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);

            var commandText = "INSERT INTO \"Vehicle\" (\"Id\", \"MakeId\", \"Model\", \"Color\", \"Year\", \"ForSale\") VALUES (@Id, @MakeId, @Model, @Color, @Year, @ForSale)";

            await using var command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@Id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
            command.Parameters.AddWithValue("@MakeId", NpgsqlTypes.NpgsqlDbType.Uuid, vehicle.MakeId is null ? DBNull.Value : vehicle.MakeId);
            command.Parameters.AddWithValue("@Model", NpgsqlTypes.NpgsqlDbType.Varchar, vehicle.Model is null ? DBNull.Value : vehicle.Model);
            command.Parameters.AddWithValue("@Color", NpgsqlTypes.NpgsqlDbType.Varchar, vehicle.Color!);
            command.Parameters.AddWithValue("@Year", NpgsqlTypes.NpgsqlDbType.TimestampTz, vehicle.Year is null ? DBNull.Value : vehicle.Year);
            command.Parameters.AddWithValue("@ForSale", NpgsqlTypes.NpgsqlDbType.Boolean, vehicle.ForSale);

            await connection.OpenAsync();

            var commits = await command.ExecuteNonQueryAsync();

            await connection.CloseAsync();

            if (commits == 0)
            {
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Data access error: " + ex.Message);
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);

            var commandText = "DELETE FROM \"Vehicle\" WHERE \"Id\" = @Id";

            await using var command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@Id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

            await connection.OpenAsync();

            var commits = await command.ExecuteNonQueryAsync();

            await connection.CloseAsync();

            if (commits == 0)
            {
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Data access error: " + ex.Message);
        }
    }

    public async Task<bool> UpdateAsync(Guid id, Vehicle vehicle)
    {
        try
        {
            using var connection = new NpgsqlConnection(_connectionString);

            var commandText = "UPDATE \"Vehicle\" SET \"MakeId\"=@MakeId, \"Model\"=@Model, \"Color\"=@Color, \"Year\"=@Year, \"ForSale\"=@ForSale WHERE \"Id\"=@Id";

            await using var command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@Id", NpgsqlTypes.NpgsqlDbType.Uuid, id);
            command.Parameters.AddWithValue("@MakeId", NpgsqlTypes.NpgsqlDbType.Uuid, vehicle.MakeId is null ? DBNull.Value : vehicle.MakeId);
            command.Parameters.AddWithValue("@Model", NpgsqlTypes.NpgsqlDbType.Varchar, vehicle.Model is null ? DBNull.Value : vehicle.Model);
            command.Parameters.AddWithValue("@Color", NpgsqlTypes.NpgsqlDbType.Varchar, vehicle.Color!);
            command.Parameters.AddWithValue("@Year", NpgsqlTypes.NpgsqlDbType.TimestampTz, vehicle.Year is null ? DBNull.Value : vehicle.Year);
            command.Parameters.AddWithValue("@ForSale", NpgsqlTypes.NpgsqlDbType.Boolean, vehicle.ForSale);

            await connection.OpenAsync();

            var commits = await command.ExecuteNonQueryAsync();

            await connection.CloseAsync();

            if (commits == 0)
            {
                return false;
            }
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Data access error: " + ex.Message);
        }
    }

    private NpgsqlCommand BuildQuery(Filtering filter, Paging paging, Sorting sorting)
    {
        var command = new NpgsqlCommand();

        var stringBuilder = new StringBuilder("SELECT v.\"Id\", v.\"MakeId\", v.\"Model\", v.\"Color\", v.\"Year\", v.\"ForSale\", m.\"Id\", m.\"Name\" FROM \"Vehicle\" AS v LEFT JOIN \"Make\" AS m ON v.\"MakeId\" = m.\"Id\" WHERE 1=1");
        if (filter.Model is not null)
        {
            stringBuilder.Append(" WHERE v.\"Model\" = @Model");
            command.Parameters.AddWithValue("@Model", NpgsqlTypes.NpgsqlDbType.Varchar, filter.Model);
        }
        if (filter.Model is not null)
        {
            stringBuilder.Append(" AND ");
        }

        stringBuilder.Append(" ORDER BY m.\"Name\" = @Name @SortBy");
        command.Parameters.AddWithValue("@Name", NpgsqlTypes.NpgsqlDbType.Varchar, sorting.OrderBy);

        command.CommandText = stringBuilder.ToString();

        return command;
    }
}