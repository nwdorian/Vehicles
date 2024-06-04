using Microsoft.AspNetCore.Mvc;
using Vehicles.Model;
using Vehicles.Service;

namespace Vehicles.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VehiclesController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAllAsync()
    {
        VehicleService vehicleService = new VehicleService();
        var vehicles = await vehicleService.GetAllAsync();

        if (!vehicles.Any())
        {
            return NotFound();
        }

        return Ok(vehicles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetAsync(Guid id)
    {
        VehicleService vehicleService = new VehicleService();
        var vehicle = await vehicleService.GetAsync(id);

        if (vehicle is null)
        {
            return NotFound();
        }

        return Ok(vehicle);
    }

    [HttpPost]
    public async Task<ActionResult> InsertAsync(Vehicle vehicle)
    {
        VehicleService vehicleService = new VehicleService();
        var added = await vehicleService.InsertAsync(vehicle);

        if (!added)
        {
            return BadRequest();
        }

        return Ok("Successfully added");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        VehicleService vehicleService = new VehicleService();
        var deleted = await vehicleService.DeleteAsync(id);

        if (!deleted)
        {
            return BadRequest();
        }

        return Ok("Successfully deleted");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(Guid id, Vehicle vehicle)
    {
        VehicleService vehicleService = new VehicleService();
        var updated = await vehicleService.UpdateAsync(id, vehicle);

        if (!updated)
        {
            return BadRequest();
        }

        return Ok("Successfully updated!");
    }
}
