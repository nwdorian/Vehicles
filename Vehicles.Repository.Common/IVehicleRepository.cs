using Vehicles.Model;

namespace Vehicles.Repository.Common;
public interface IVehicleRepository
{
    Task<bool> DeleteAsync(Guid id);
    Task<List<Vehicle>> GetAllAsync();
    Task<Vehicle?> GetAsync(Guid id);
    Task<bool> InsertAsync(Vehicle vehicle);
    Task<bool> UpdateAsync(Guid id, Vehicle vehicle);
}
