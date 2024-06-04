using Vehicles.Model;
using Vehicles.Repository;

namespace Vehicles.Service;
public class VehicleService
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
}
