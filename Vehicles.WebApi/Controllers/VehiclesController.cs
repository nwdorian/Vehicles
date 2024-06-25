using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Vehicles.Common;
using Vehicles.Common.Filters;
using Vehicles.Model;
using Vehicles.Service.Common;
using Vehicles.WebApi.DTOs.Vehicle;

namespace Vehicles.WebApi.Controllers;
[EnableCors("MyPolicy")]
[Route("api/[controller]")]
[ApiController]
public class VehiclesController : ControllerBase
{
    private readonly IVehicleService _vehicleService;
    private readonly IMapper _mapper;

    public VehiclesController(IVehicleService vehicleService, IMapper mapper)
    {
        _vehicleService = vehicleService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] VehicleFilter filter, [FromQuery] Paging paging, [FromQuery] Sorting sorting)
    {
        var response = await _vehicleService.GetAllAsync(filter, paging, sorting);

        if (response.Success)
        {
            var vehicles = _mapper.Map<List<VehicleReadDTO>>(response.Data);
            return Ok(vehicles);
        }

        return NotFound();

    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var response = await _vehicleService.GetAsync(id);

        if (response.Success)
        {
            var vehicle = _mapper.Map<VehicleReadDTO>(response.Data);
            return Ok(vehicle);
        }

        return NotFound();

    }

    [HttpPost]
    public async Task<IActionResult> InsertAsync(VehicleCreateDTO vehicleDto)
    {
        var vehicle = _mapper.Map<Vehicle>(vehicleDto);

        var response = await _vehicleService.InsertAsync(vehicle);

        if (response.Success)
        {
            return CreatedAtAction("Get", new { id = vehicle.Id }, _mapper.Map<VehicleReadDTO>(response.Data));
        }

        return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var response = await _vehicleService.DeleteAsync(id);

        if (response)
        {
            return Ok("Successfully deleted");
        }
        return BadRequest("Id doesn't exist");

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, VehicleCreateDTO vehicleDto)
    {
        var vehicle = _mapper.Map<Vehicle>(vehicleDto);

        var response = await _vehicleService.UpdateAsync(id, vehicle);

        if (response.Success)
        {
            return Ok("Successfully updated!");
        }
        return BadRequest(response.Message);

    }
}
