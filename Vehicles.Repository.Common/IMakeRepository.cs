using Vehicles.Model;

namespace Vehicles.Repository.Common;
public interface IMakeRepository
{
    Task<ApiResponse<List<Make>>> GetAllAsync();
    Task<ApiResponse<Make>> GetAsync(Guid id);
    Task<ApiResponse<Make>> GetByNameAsync(string name);
    Task<ApiResponse<Make>> InsertAsync(Make make);
}
