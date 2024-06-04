using Vehicles.Model;
using Vehicles.Repository;
using Vehicles.Service.Common;

namespace Vehicles.Service;
public class VehicleService : IVehicleService
{
    public async Task<List<Vehicle>> GetAllAsync()
    {
        VehicleRepository vehicleRepository = new VehicleRepository();

        return await vehicleRepository.GetAllAsync();
    }

    public async Task<Vehicle?> GetAsync(Guid id)
    {
        VehicleRepository vehicleRepository = new VehicleRepository();

        return await vehicleRepository.GetAsync(id);
    }

    public async Task<bool> InsertAsync(Vehicle vehicle)
    {
        VehicleRepository vehicleRepository = new VehicleRepository();

        return await vehicleRepository.InsertAsync(vehicle);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        VehicleRepository vehicleRepository = new VehicleRepository();

        return await vehicleRepository.DeleteAsync(id);
    }

    public async Task<bool> UpdateAsync(Guid id, Vehicle vehicle)
    {
        VehicleRepository vehicleRepository = new VehicleRepository();

        return await vehicleRepository.UpdateAsync(id, vehicle);
    }
}
