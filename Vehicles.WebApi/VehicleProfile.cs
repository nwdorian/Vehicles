using AutoMapper;
using Vehicles.Model;
using Vehicles.WebApi.Models;

namespace Vehicles.WebApi;

public class VehicleProfile : Profile
{
    public VehicleProfile()
    {
        CreateMap<Vehicle, VehicleDTO>().ReverseMap();
        CreateMap<Make, MakeDTO>().ReverseMap();
    }
}
