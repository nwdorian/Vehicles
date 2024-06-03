﻿using Microsoft.AspNetCore.Mvc;
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
    public ActionResult Get()
    {
        try
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            var connectionString = _configuration.GetConnectionString("Default");
            using var connection = new NpgsqlConnection(connectionString);

            var commandText = "SELECT * FROM \"Vehicle\"";

            using var command = new NpgsqlCommand(commandText, connection);

            connection.Open();

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                while (reader.Read())
                {
                    vehicles.Add(new Vehicle
                    {
                        Id = Guid.Parse(reader[0].ToString()),
                        MakeId = Guid.TryParse(reader[1].ToString(), out var result) ? result : null,
                        Model = reader[2].ToString(),
                        Color = reader[3].ToString(),
                        Year = DateTime.Parse(reader[4].ToString()),
                        ForSale = bool.Parse(reader[5].ToString())
                    });
                }
            }

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
    public ActionResult Get(Guid id)
    {
        try
        {
            Vehicle vehicle = new Vehicle();
            var connectionString = _configuration.GetConnectionString("Default");
            using var connection = new NpgsqlConnection(connectionString);

            var commandText = "SELECT * FROM \"Vehicle\" WHERE \"Id\" = @Id";

            using var command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@Id", NpgsqlTypes.NpgsqlDbType.Uuid, id);

            connection.Open();

            var reader = command.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    vehicle.Id = Guid.Parse(reader[0].ToString());
                    vehicle.MakeId = Guid.TryParse(reader[1].ToString(), out var result) ? result : null;
                    vehicle.Model = reader[2].ToString();
                    vehicle.Color = reader[3].ToString();
                    vehicle.Year = DateTime.Parse(reader[4].ToString());
                    vehicle.ForSale = bool.Parse(reader[5].ToString());
                }
            }

            connection.Close();

            if (vehicle == null)
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
    public ActionResult Insert(Vehicle vehicle)
    {
        try
        {
            var connectionString = _configuration.GetConnectionString("Default");
            using var connection = new NpgsqlConnection(connectionString);

            var commandText = "INSERT INTO \"Vehicle\" (\"Id\", \"MakeId\", \"Model\", \"Color\", \"Year\", \"ForSale\") VALUES (@Id, @MakeId, @Model, @Color, @Year, @ForSale)";

            using var command = new NpgsqlCommand(commandText, connection);

            command.Parameters.AddWithValue("@Id", NpgsqlTypes.NpgsqlDbType.Uuid, Guid.NewGuid());
            command.Parameters.AddWithValue("@MakeId", NpgsqlTypes.NpgsqlDbType.Uuid, vehicle.MakeId is null ? DBNull.Value : vehicle.MakeId);
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

            if (commits == -1)
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
}
