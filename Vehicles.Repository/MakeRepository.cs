using Npgsql;
using Vehicles.Model;
using Vehicles.Repository.Common;

namespace Vehicles.Repository;
public class MakeRepository : IMakeRepository
{
    private readonly string _connectionString = "Host=localhost:5432;Username=postgres;Password=admin;Database=VehiclesDb";
    public async Task<ApiResponse<List<Make>>> GetAllAsync()
    {
        var response = new ApiResponse<List<Make>>();
        try
        {
            var makes = new List<Make>();

            using var connection = new NpgsqlConnection(_connectionString);
            var commandText = "SELECT \"Id\",\"Name\", \"IsActive\", \"DateCreated\", \"DateUpdated\" FROM \"Make\"";
            using var command = new NpgsqlCommand(commandText, connection);

            connection.Open();
            using var readerAsync = await command.ExecuteReaderAsync();
            while (await readerAsync.ReadAsync())
            {
                var make = new Make();

                make.Id = readerAsync.GetGuid(0);
                make.Name = readerAsync.GetString(1);
                make.IsActive = readerAsync.GetBoolean(2);
                make.DateCreated = readerAsync.GetDateTime(3);
                make.DateUpdated = readerAsync.GetDateTime(4);
                makes.Add(make);
            }
            connection.Close();

            response.Success = true;
            response.Data = makes;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error in Make Repository GetAllAsync {ex.Message}";
        }
        return response;

    }

    public async Task<ApiResponse<Make>> GetAsync(Guid id)
    {
        var response = new ApiResponse<Make>();
        try
        {
            var make = new Make();

            using var connection = new NpgsqlConnection(_connectionString);
            var commandText = "SELECT \"Id\",\"Name\", \"IsActive\", \"DateCreated\", \"DateUpdated\" FROM \"Make\" WHERE \"Id\" = @Id";
            using var command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@Id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

            connection.Open();
            using var readerAsync = await command.ExecuteReaderAsync();
            if (readerAsync.HasRows)
            {
                await readerAsync.ReadAsync();

                make.Id = readerAsync.GetGuid(0);
                make.Name = readerAsync.GetString(1);
                make.IsActive = readerAsync.GetBoolean(2);
                make.DateCreated = readerAsync.GetDateTime(3);
                make.DateUpdated = readerAsync.GetDateTime(4);
            }
            connection.Close();

            response.Success = true;
            response.Data = make;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error in Make Repository GetAsync {ex.Message}";
        }
        return response;

    }

    public async Task<ApiResponse<Make>> GetByNameAsync(string name)
    {
        var response = new ApiResponse<Make>();
        try
        {
            var make = new Make();

            using var connection = new NpgsqlConnection(_connectionString);
            var commandText = "SELECT \"Id\",\"Name\", \"IsActive\", \"DateCreated\", \"DateUpdated\" FROM \"Make\" WHERE \"Name\" = @Name";
            using var command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@Name", NpgsqlTypes.NpgsqlDbType.Uuid, name);

            connection.Open();
            using var readerAsync = await command.ExecuteReaderAsync();
            if (readerAsync.HasRows)
            {
                await readerAsync.ReadAsync();

                make.Id = readerAsync.GetGuid(0);
                make.Name = readerAsync.GetString(1);
                make.IsActive = readerAsync.GetBoolean(2);
                make.DateCreated = readerAsync.GetDateTime(3);
                make.DateUpdated = readerAsync.GetDateTime(4);
            }
            connection.Close();

            response.Success = true;
            response.Data = make;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error in Make Repository GetByNameAsync {ex.Message}";
        }
        return response;
    }

    public async Task<ApiResponse<Make>> InsertAsync(Make make)
    {
        var response = new ApiResponse<Make>();

        try
        {
            using var connection = new NpgsqlConnection(_connectionString);
            var commandText = "INSERT INTO \"Make\" (\"Id\", \"Name\", \"IsActive\", \"DateCreated\", \"DateUpdated\") VALUES (@Id, @Name, @IsActive, @DateCreated, @DateUpdated)";
            using var command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@Id", make.Id);
            command.Parameters.AddWithValue("@Name", make.Name is null ? "" : make.Name);
            command.Parameters.AddWithValue("@IsActive", make.IsActive);
            command.Parameters.AddWithValue("@DateCreated", make.DateCreated);
            command.Parameters.AddWithValue("@DateUpdated", make.DateUpdated);

            connection.Open();
            await command.ExecuteNonQueryAsync();
            connection.Close();

            response.Success = true;
            response.Data = make;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = $"Error in Make Repository InsertAsync {ex.Message}";
        }
        return response;
    }
}
