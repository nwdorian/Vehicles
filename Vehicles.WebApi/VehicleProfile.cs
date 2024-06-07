using AutoMapper;
using Vehicles.Model;
using Vehicles.WebApi.Models;

namespace Vehicles.WebApi;

public class VehicleProfile : Profile
{
    public VehicleProfile()
    {
        CreateMap<Vehicle, VehicleGetRest>()
        .ForMember(dest => dest.Make,
        opt => opt.MapFrom(src => src.Make.Name));
    }
}
