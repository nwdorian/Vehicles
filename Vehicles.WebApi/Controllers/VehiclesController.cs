using Microsoft.AspNetCore.Mvc;
using Npgsql;
using Vehicles.WebApi.Models;

namespace Vehicles.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VehiclesController : ControllerBase
{
    private readonly IConfiguration _configuration;
    public VehiclesController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<ActionResult> GetAsync()
    {
        try
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            var connectionString = _configuration.GetConnectionString("Default");
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

            if (vehicles.Count == 0)
            {
                return NotFound();
            }

            return Ok(vehicles);

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> Get(Guid id)
    {
        try
        {
            Vehicle vehicle = new Vehicle();
            var vehicleFound = false;
            var connectionString = _configuration.GetConnectionString("Default");
            using var connection = new NpgsqlConnection(connectionString);

            var commandText = "SELECT * FROM \"Vehicle\" WHERE \"Id\" = @Id";

            using var command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@Id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

            await connection.OpenAsync();

            var readerAsync = await command.ExecuteReaderAsync();

            if (readerAsync.HasRows)
            {
                if (await readerAsync.ReadAsync())
                {
                    vehicle.Id = Guid.Parse(readerAsync[0].ToString());
                    vehicle.MakeId = Guid.TryParse(readerAsync[1].ToString(), out var result) ? result : null;
                    vehicle.Model = readerAsync[2].ToString();
                    vehicle.Color = readerAsync[3].ToString();
                    vehicle.Year = DateTime.Parse(readerAsync[4].ToString());
                    vehicle.ForSale = bool.Parse(readerAsync[5].ToString());
                    vehicleFound = true;
                }
            }

            await connection.CloseAsync();

            if (vehicleFound == false)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Insert(Vehicle vehicle)
    {
        try
        {
            var connectionString = _configuration.GetConnectionString("Default");
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
                return BadRequest();
            }

            return Ok("Successfully added");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(Guid id)
    {
        try
        {
            var connectionString = _configuration.GetConnectionString("Default");
            using var connection = new NpgsqlConnection(connectionString);

            var commandText = "DELETE FROM \"Vehicle\" WHERE \"Id\" = @Id";

            using var command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@Id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

            connection.Open();

            var commits = command.ExecuteNonQuery();

            connection.Close();

            if (commits == 0)
            {
                return BadRequest();
            }

            return Ok("Successfully deleted");

        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public ActionResult Update(Guid id, Vehicle vehicle)
    {
        try
        {
            var connectionString = _configuration.GetConnectionString("Default");
            using var connection = new NpgsqlConnection(connectionString);

            var commandText = "UPDATE \"Vehicle\" SET \"MakeId\"=@MakeId, \"Model\"=@Model, \"Color\"=@Color, \"Year\"=@Year, \"ForSale\"=@ForSale WHERE \"Id\"=@Id";

            using var command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@Id", NpgsqlTypes.NpgsqlDbType.Uuid, id);
            command.Parameters.AddWithValue("@MakeId", NpgsqlTypes.NpgsqlDbType.Uuid, vehicle.MakeId is null ? DBNull.Value : vehicle.MakeId);
            command.Parameters.AddWithValue("@Model", NpgsqlTypes.NpgsqlDbType.Varchar, vehicle.Model);
            command.Parameters.AddWithValue("@Color", NpgsqlTypes.NpgsqlDbType.Varchar, vehicle.Color);
            command.Parameters.AddWithValue("@Year", NpgsqlTypes.NpgsqlDbType.TimestampTz, vehicle.Year);
            command.Parameters.AddWithValue("@ForSale", vehicle.ForSale);


            connection.Open();

            var commits = command.ExecuteNonQuery();

            connection.Close();

            if (commits == 0)
            {
                return BadRequest();
            }

            return Ok("Successfully updated!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
