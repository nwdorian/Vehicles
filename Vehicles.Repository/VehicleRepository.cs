using Npgsql;
using Vehicles.Model;

namespace Vehicles.Repository;
public class VehicleRepository
{
    public async Task<List<Vehicle>> GetAllAsync()
    {
        List<Vehicle> vehicles = new List<Vehicle>();

        try
        {
            var connectionString = @"Host=localhost:5432;Username=postgres;Password=postgresadmin;Database=VehiclesDb";
            using var connection = new NpgsqlConnection(connectionString);

            var commandText = "SELECT * FROM \"Vehicle\"";

            using var command = new NpgsqlCommand(commandText, connection);

            await connection.OpenAsync();

            var readerAsync = await command.ExecuteReaderAsync();

            if (readerAsync.HasRows)
            {
                while (await readerAsync.ReadAsync())
                {
                    vehicles.Add(new Vehicle
                    {
                        Id = Guid.Parse(readerAsync[0].ToString()),
                        MakeId = Guid.TryParse(readerAsync[1].ToString(), out var result) ? result : null,
                        Model = readerAsync[2].ToString(),
                        Color = readerAsync[3].ToString(),
                        Year = DateTime.Parse(readerAsync[4].ToString()),
                        ForSale = bool.Parse(readerAsync[5].ToString())
                    });
                }
            }
            await connection.CloseAsync();

            return vehicles;
        }
        catch (Exception ex)
        {
            throw new Exception("Data access error" + ex.Message);
        }
    }

    public async Task<Vehicle?> GetAsync(Guid id)
    {
        try
        {
            var connectionString = @"Host=localhost:5432;Username=postgres;Password=postgresadmin;Database=VehiclesDb";
            using var connection = new NpgsqlConnection(connectionString);

            var commandText = "SELECT * FROM \"Vehicle\" WHERE \"Id\" = @Id";

            using var command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@Id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

            await connection.OpenAsync();

            var readerAsync = await command.ExecuteReaderAsync();

            if (readerAsync.HasRows)
            {

                Vehicle vehicle = new Vehicle();

                await readerAsync.ReadAsync();

                vehicle.Id = Guid.Parse(readerAsync[0].ToString());
                vehicle.MakeId = Guid.TryParse(readerAsync[1].ToString(), out var result) ? result : null;
                vehicle.Model = readerAsync[2].ToString();
                vehicle.Color = readerAsync[3].ToString();
                vehicle.Year = DateTime.Parse(readerAsync[4].ToString());
                vehicle.ForSale = bool.Parse(readerAsync[5].ToString());
                //TODO debug and check if after return goes out of if statement
                await connection.CloseAsync();
                return vehicle;
            }

            await connection.CloseAsync();
            return null;

        }
        catch (Exception ex)
        {
            throw new Exception("Data access error" + ex.Message);
        }
    }

    public async Task<bool> InsertAsync(Vehicle vehicle)
    {
        try
        {
            var connectionString = @"Host=localhost:5432;Username=postgres;Password=postgresadmin;Database=VehiclesDb";
            using var connection = new NpgsqlConnection(connectionString);

            var commandText = "INSERT INTO \"Vehicle\" (\"Id\", \"MakeId\", \"Model\", \"Color\", \"Year\", \"ForSale\") VALUES (@Id, @MakeId, @Model, @Color, @Year, @ForSale)";

            using var command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@Id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
            command.Parameters.AddWithValue("@MakeId", NpgsqlTypes.NpgsqlDbType.Uuid, vehicle.MakeId is null ? DBNull.Value : vehicle.MakeId);
            command.Parameters.AddWithValue("@Model", vehicle.Model);
            command.Parameters.AddWithValue("@Color", vehicle.Color);
            command.Parameters.AddWithValue("@Year", NpgsqlTypes.NpgsqlDbType.TimestampTz, vehicle.Year);
            command.Parameters.AddWithValue("@ForSale", vehicle.ForSale);

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
            throw new Exception("Data access error" + ex.Message);
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var connectionString = @"Host=localhost:5432;Username=postgres;Password=postgresadmin;Database=VehiclesDb";
            using var connection = new NpgsqlConnection(connectionString);

            var commandText = "DELETE FROM \"Vehicle\" WHERE \"Id\" = @Id";

            using var command = new NpgsqlCommand(commandText, connection);

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
            throw new Exception("Data access error" + ex.Message);
        }
    }

    public async Task<bool> UpdateAsync(Guid id, Vehicle vehicle)
    {
        try
        {
            var connectionString = @"Host=localhost:5432;Username=postgres;Password=postgresadmin;Database=VehiclesDb";
            using var connection = new NpgsqlConnection(connectionString);

            var commandText = "UPDATE \"Vehicle\" SET \"MakeId\"=@MakeId, \"Model\"=@Model, \"Color\"=@Color, \"Year\"=@Year, \"ForSale\"=@ForSale WHERE \"Id\"=@Id";

            using var command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@Id", NpgsqlTypes.NpgsqlDbType.Uuid, id);
            command.Parameters.AddWithValue("@MakeId", NpgsqlTypes.NpgsqlDbType.Uuid, vehicle.MakeId is null ? DBNull.Value : vehicle.MakeId);
            command.Parameters.AddWithValue("@Model", NpgsqlTypes.NpgsqlDbType.Varchar, vehicle.Model);
            command.Parameters.AddWithValue("@Color", NpgsqlTypes.NpgsqlDbType.Varchar, vehicle.Color);
            command.Parameters.AddWithValue("@Year", NpgsqlTypes.NpgsqlDbType.TimestampTz, vehicle.Year);
            command.Parameters.AddWithValue("@ForSale", vehicle.ForSale);


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
            throw new Exception("Data access error" + ex.Message);
        }
    }
}
