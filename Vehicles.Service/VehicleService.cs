using Vehicles.Common;
using Vehicles.Common.Filters;
using Vehicles.Model;
using Vehicles.Repository.Common;
using Vehicles.Service.Common;

namespace Vehicles.Service;
public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IMakeRepository _makeRepository;
    public VehicleService(IVehicleRepository vehicleRepository, IMakeRepository makeRepository)
    {
        _vehicleRepository = vehicleRepository;
        _makeRepository = makeRepository;
    }
    public async Task<ApiResponse<List<Vehicle>>> GetAllAsync(VehicleFilter filter, Paging paging, Sorting sorting)
    {
        return await _vehicleRepository.GetAllAsync(filter, paging, sorting);
    }

    public async Task<ApiResponse<Vehicle>> GetAsync(Guid id)
    {
        return await _vehicleRepository.GetAsync(id);
    }

    public async Task<ApiResponse<Vehicle>> InsertAsync(Vehicle vehicle)
    {
        var response = await _makeRepository.GetByNameAsync(vehicle.Make.Name);
        if (response.Success)
        {
            vehicle.MakeId = response.Data.Id;
        }
        else
        {
            var make = new Make();
            make.Id = Guid.NewGuid();
            make.Name = vehicle.Make.Name.ToTitleCase();
            make.IsActive = true;
            make.DateCreated = DateTime.Now;
            make.DateUpdated = DateTime.Now;
            await _makeRepository.InsertAsync(make);
            vehicle.MakeId = make.Id;
        }

        vehicle.Id = Guid.NewGuid();
        vehicle.IsActive = true;
        vehicle.DateCreated = DateTime.Now;
        vehicle.DateUpdated = DateTime.Now;
        return await _vehicleRepository.InsertAsync(vehicle);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _vehicleRepository.DeleteAsync(id);
    }

    public async Task<ApiResponse<Vehicle>> UpdateAsync(Guid id, Vehicle vehicle)
    {
        var response = await _vehicleRepository.GetAsync(id);
        if (!response.Success)
        {
            response.Message = "Vehicle not found";
            return response;
        }

        var existingVehicle = response.Data;

        existingVehicle.MakeId = vehicle.MakeId;
        if (!string.IsNullOrEmpty(vehicle.Model))
        {
            existingVehicle.Model = vehicle.Model;
        }
        if (!string.IsNullOrEmpty(vehicle.Color))
        {
            existingVehicle.Color = vehicle.Color;
        }
        existingVehicle.Year = vehicle.Year;
        existingVehicle.ForSale = vehicle.ForSale;
        existingVehicle.DateUpdated = DateTime.Now;

        return await _vehicleRepository.UpdateAsync(id, existingVehicle);
    }
}
