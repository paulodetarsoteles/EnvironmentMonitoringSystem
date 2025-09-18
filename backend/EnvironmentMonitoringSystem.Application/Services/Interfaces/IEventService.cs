using EnvironmentMonitoringSystem.Application.Models.Requests;
using EnvironmentMonitoringSystem.Application.Models.Responses;
using EnvironmentMonitoringSystem.Domain.Models;

namespace EnvironmentMonitoringSystem.Application.Services.Interfaces
{
    public interface IEventService
    {
        Task<bool> RegisterEventAsync(EventRequest.Receive request);
        Task<List<BaseResponse<Event>>> ListLastEvents();
        Task<Guid> RegisterDeviceOnIot(DeviceRequest.Add request);
        Task<bool> UnregisterDeviceOnIot(Guid deviceId);
    }
}
