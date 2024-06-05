using Vehicles.Common;
using Vehicles.Common.Filters;
using Vehicles.Model;

namespace Vehicles.Service.Common;
public interface IVehicleService
{
    Task<bool> DeleteAsync(Guid id);
    Task<List<Vehicle>> GetAllAsync(VehicleFilter filter, Paging paging, Sorting sorting);
    Task<Vehicle?> GetAsync(Guid id);
    Task<bool> InsertAsync(Vehicle vehicle);
    Task<bool> UpdateAsync(Guid id, Vehicle vehicle);
}
