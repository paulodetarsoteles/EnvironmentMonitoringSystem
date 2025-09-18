using EnvironmentMonitoringSystem.Application.Models.Requests;
using EnvironmentMonitoringSystem.Application.Models.Responses;
using EnvironmentMonitoringSystem.Domain.Models;

namespace EnvironmentMonitoringSystem.Application.Services.Interfaces
{
    public interface IDeviceService
    {
        Task<BaseResponse<List<DeviceListResponse>>> ListAsync();
        Task<BaseResponse<Device>> GetByIdAsync(Guid id);
        Task<BaseResponse<bool>> CreateAsync(DeviceRequest.Add request);
        Task<BaseResponse<bool>> UpdateAsync(Guid id, DeviceRequest.Update request);
        Task<BaseResponse<bool>> DeleteAsync(Guid id);
    }
}
