using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Vehicles.Service.Common;
using Vehicles.WebApi.DTOs.Make;

namespace Vehicles.WebApi.Controllers;

[EnableCors("MyPolicy")]
[Route("api/[controller]")]
[ApiController]
public class MakesController : ControllerBase
{
    private readonly IMakeService _makeService;
    private readonly IMapper _mapper;
    public MakesController(IMakeService makeService, IMapper mapper)
    {
        _makeService = makeService;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var response = await _makeService.GetAllAsync();

        if (response.Success)
        {
            var makes = _mapper.Map<List<MakeDTO>>(response.Data);
            return Ok(makes);
        }
        return NotFound();
    }
}
