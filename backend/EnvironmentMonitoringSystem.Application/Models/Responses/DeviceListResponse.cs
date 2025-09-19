namespace EnvironmentMonitoringSystem.Application.Models.Responses
{
    public class DeviceListResponse
    {
        public Guid Id { get; set; }
        public string DeviceName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
