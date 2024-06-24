using Vehicles.Common;
using Vehicles.Common.Filters;
using Vehicles.Model;

namespace Vehicles.Service.Common;
public interface IVehicleService
{
    Task<ApiResponse<List<Vehicle>>> GetAllAsync(VehicleFilter filter, Paging paging, Sorting sorting);
    Task<ApiResponse<Vehicle>> GetAsync(Guid id);
    Task<ApiResponse<Vehicle>> InsertAsync(Vehicle vehicle);
    Task<ApiResponse<Vehicle>> UpdateAsync(Guid id, Vehicle vehicle);
    Task<bool> DeleteAsync(Guid id);

}
