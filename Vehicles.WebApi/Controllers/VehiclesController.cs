using Microsoft.AspNetCore.Mvc;
using Vehicles.Model;
using Vehicles.Repository;

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
    public async Task<ActionResult> GetAllAsync()
    {
        VehiclesRepository vehiclesRepository = new VehiclesRepository();
        var vehicles = await vehiclesRepository.GetAllAsync();

        if (vehicles.Any())
        {
            return NotFound();
        }

        return Ok(vehicles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetAsync(Guid id)
    {
        VehiclesRepository vehiclesRepository = new VehiclesRepository();
        var vehicle = await vehiclesRepository.GetAsync(id);

        if (vehicle is null)
        {
            return NotFound();
        }

        return Ok(vehicle);
    }

    [HttpPost]
    public async Task<ActionResult> InsertAsync(Vehicle vehicle)
    {
        VehiclesRepository vehiclesRepository = new VehiclesRepository();

        var added = await vehiclesRepository.InsertAsync(vehicle);

        if (added)
        {
            return BadRequest();
        }

        return Ok("Successfully added");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        try
        {
            var connectionString = _configuration.GetConnectionString("Default");
            using var connection = new NpgsqlConnection(connectionString);

            var commandText = "DELETE FROM \"Vehicle\" WHERE \"Id\" = @Id";

            using var command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@Id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

            await connection.OpenAsync();

            var commits = await command.ExecuteNonQueryAsync();

            await connection.CloseAsync();

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
    public async Task<ActionResult> UpdateAsync(Guid id, Vehicle vehicle)
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


            await connection.OpenAsync();

            var commits = await command.ExecuteNonQueryAsync();

            await connection.CloseAsync();

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
