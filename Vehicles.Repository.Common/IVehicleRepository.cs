using Vehicles.Common;
using Vehicles.Common.Filters;
using Vehicles.Model;

namespace Vehicles.Repository.Common;
public interface IVehicleRepository
{
    Task<bool> DeleteAsync(Guid id);
    Task<List<Vehicle>> GetAllAsync(VehicleFilter filter, Paging paging, Sorting sorting);
    Task<Vehicle?> GetAsync(Guid id);
    Task<bool> InsertAsync(Vehicle vehicle);
    Task<bool> UpdateAsync(Guid id, Vehicle vehicle);
}
