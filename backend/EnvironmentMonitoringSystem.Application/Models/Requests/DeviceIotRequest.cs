namespace EnvironmentMonitoringSystem.Application.Models.Requests
{
    public class DeviceIotRequest
    {
        public string DeviceName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string CallbackUrl { get; set; } = string.Empty;
    }
}
