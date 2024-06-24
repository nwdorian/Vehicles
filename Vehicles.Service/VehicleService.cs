using Vehicles.Common;
using Vehicles.Common.Filters;
using Vehicles.Model;
using Vehicles.Repository.Common;
using Vehicles.Service.Common;

namespace Vehicles.Service;
public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleService(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
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

        existingVehicle.DateUpdated = DateTime.Now;

        if (!string.IsNullOrEmpty(vehicle.Model))
        {
            existingVehicle.Model = vehicle.Model;
        }
        if (!string.IsNullOrEmpty(vehicle.Color))
        {
            existingVehicle.Color = vehicle.Color;
        }

        return await _vehicleRepository.UpdateAsync(id, existingVehicle);
    }
}
