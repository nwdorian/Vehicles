using Vehicles.Model;
using Vehicles.Repository.Common;
using Vehicles.Service.Common;

namespace Vehicles.Service;
public class MakeService : IMakeService
{
    private readonly IMakeRepository _makeRepository;
    public MakeService(IMakeRepository makeRepository)
    {
        _makeRepository = makeRepository;
    }
    public async Task<ApiResponse<List<Make>>> GetAllAsync()
    {
        return await _makeRepository.GetAllAsync();
    }
}
