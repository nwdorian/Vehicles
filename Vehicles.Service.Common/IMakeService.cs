using Vehicles.Model;

namespace Vehicles.Service.Common;
public interface IMakeService
{
    Task<ApiResponse<List<Make>>> GetAllAsync();
}
