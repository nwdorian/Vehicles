using Microsoft.AspNetCore.Mvc;
using Vehicles.Common;
using Vehicles.Common.Filters;
using Vehicles.Model;
using Vehicles.Service.Common;

namespace Vehicles.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _vehicleService;
    public VehiclesController(IVehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }
    [HttpGet]
    public async Task<ActionResult> GetAllAsync([FromQuery] VehicleFilter filter, [FromQuery] Paging paging, string orderBy = "Make", string sortOrder = "ASC")
    {
        Sorting sorting = new Sorting(orderBy, sortOrder);

        var vehicles = await _vehicleService.GetAllAsync(filter, paging, sorting);

        if (!vehicles.Any())
        {
            return NotFound();
        }

        return Ok(vehicles);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetAsync(Guid id)
    {
        var vehicle = await _vehicleService.GetAsync(id);

        if (vehicle is null)
        {
            return NotFound();
        }

        return Ok(vehicle);
    }

    [HttpPost]
    public async Task<ActionResult> InsertAsync(Vehicle vehicle)
    {
        var added = await _vehicleService.InsertAsync(vehicle);

        if (!added)
        {
            return BadRequest();
        }

        return Ok("Successfully added");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        var deleted = await _vehicleService.DeleteAsync(id);

        if (!deleted)
        {
            return BadRequest();
        }

        return Ok("Successfully deleted");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(Guid id, Vehicle vehicle)
    {
        var updated = await _vehicleService.UpdateAsync(id, vehicle);

        if (!updated)
        {
            return BadRequest();
        }

        return Ok("Successfully updated!");
    }
}
