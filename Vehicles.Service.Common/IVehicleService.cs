using Vehicles.Model;

namespace Vehicles.Service.Common;
public interface IVehicleService
{
    Task<bool> DeleteAsync(Guid id);
    Task<List<Vehicle>> GetAllAsync();
    Task<Vehicle?> GetAsync(Guid id);
    Task<bool> InsertAsync(Vehicle vehicle);
    Task<bool> UpdateAsync(Guid id, Vehicle vehicle);
}
