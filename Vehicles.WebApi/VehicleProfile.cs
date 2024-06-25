using AutoMapper;
using Vehicles.Model;
using Vehicles.WebApi.DTOs.Make;
using Vehicles.WebApi.DTOs.Vehicle;

namespace Vehicles.WebApi;

public class VehicleProfile : Profile
{
    public VehicleProfile()
    {
        CreateMap<Vehicle, VehicleReadDTO>().ReverseMap();
        CreateMap<Vehicle, VehicleCreateDTO>().ReverseMap();
        CreateMap<Make, MakeDTO>().ReverseMap();
    }
}
