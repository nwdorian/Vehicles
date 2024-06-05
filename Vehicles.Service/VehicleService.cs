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
    public async Task<List<Vehicle>> GetAllAsync(VehicleFilter filter, Paging paging, Sorting sorting)
    {
        return await _vehicleRepository.GetAllAsync(filter, paging, sorting);
    }

    public async Task<Vehicle?> GetAsync(Guid id)
    {
        return await _vehicleRepository.GetAsync(id);
    }

    public async Task<bool> InsertAsync(Vehicle vehicle)
    {
        return await _vehicleRepository.InsertAsync(vehicle);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _vehicleRepository.DeleteAsync(id);
    }

    public async Task<bool> UpdateAsync(Guid id, Vehicle vehicle)
    {
        return await _vehicleRepository.UpdateAsync(id, vehicle);
    }
}
