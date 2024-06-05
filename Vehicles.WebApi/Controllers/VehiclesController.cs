using Microsoft.AspNetCore.Mvc;
using Vehicles.Common;
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
    public async Task<ActionResult> GetAllAsync(Guid? makeId = null, string? model = null, string? color = null, DateTime? startDate = null, DateTime? endDate = null, bool? forSale = null, string searchQuery = "", int pageSize = 10, int pageNumber = 1, string orderBy = "Model", string sortOrder = "ASC")
    {
        Filtering filter = new Filtering();
        filter.MakeId = makeId;
        filter.Model = model;
        filter.Color = color;
        filter.StartDate = startDate;
        filter.EndDate = endDate;
        filter.ForSale = forSale;
        filter.SearchQuery = searchQuery;

        Paging paging = new Paging();
        paging.PageSize = pageSize;
        paging.PageNumber = pageNumber;

        Sorting sorting = new Sorting();
        sorting.OrderBy = orderBy;
        sorting.SortOrder = sortOrder;

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
