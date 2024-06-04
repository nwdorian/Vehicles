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
        VehicleRepository vehicleRepository = new VehicleRepository();
        var vehicles = await vehicleRepository.GetAllAsync();

        if (!vehicles.Any())
        {
            return NotFound();
        }

        return Ok(vehicles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetAsync(Guid id)
    {
        VehicleRepository vehicleRepository = new VehicleRepository();
        var vehicle = await vehicleRepository.GetAsync(id);

        if (vehicle is null)
        {
            return NotFound();
        }

        return Ok(vehicle);
    }

    [HttpPost]
    public async Task<ActionResult> InsertAsync(Vehicle vehicle)
    {
        VehicleRepository vehicleRepository = new VehicleRepository();

        var added = await vehicleRepository.InsertAsync(vehicle);

        if (!added)
        {
            return BadRequest();
        }

        return Ok("Successfully added");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        VehicleRepository vehicleRepository = new VehicleRepository();
        var deleted = await vehicleRepository.DeleteAsync(id);

        if (!deleted)
        {
            return BadRequest();
        }

        return Ok("Successfully deleted");

    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(Guid id, Vehicle vehicle)
    {
        VehicleRepository vehicleRepository = new VehicleRepository();
        var updated = await vehicleRepository.UpdateAsync(id, vehicle);

        if (!updated)
        {
            return BadRequest();
        }

        return Ok("Successfully updated!");

    }
}
