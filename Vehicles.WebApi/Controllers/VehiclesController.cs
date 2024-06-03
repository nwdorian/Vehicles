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

    [HttpPost]
    public ActionResult Insert(Vehicle vehicle)
    {
        try
        {
            var connectionString = _configuration.GetConnectionString("Default");

            using var connection = new NpgsqlConnection(connectionString);

            var commandText = "INSERT INTO \"Vehicle\" (\"Id\", \"MakeId\", \"Model\", \"Color\", \"Year\", \"ForSale\") VALUES (@Id, @MakeId, @Model, @Color, @Year, @ForSale)";

            using var command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@Id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
            command.Parameters.AddWithValue("@MakeId", NpgsqlTypes.NpgsqlDbType.Uuid, vehicle.MakeId);
            command.Parameters.AddWithValue("@Model", vehicle.Model.ToString());
            command.Parameters.AddWithValue("@Color", vehicle.Color.ToString());
            command.Parameters.AddWithValue("@Year", NpgsqlTypes.NpgsqlDbType.TimestampTz, vehicle.Year);
            command.Parameters.AddWithValue("@ForSale", vehicle.ForSale);

            connection.Open();

            var commits = command.ExecuteNonQuery();

            connection.Close();

            if (commits == -1)
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
}
