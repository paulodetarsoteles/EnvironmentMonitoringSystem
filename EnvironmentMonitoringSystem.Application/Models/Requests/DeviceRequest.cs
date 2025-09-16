namespace EnvironmentMonitoringSystem.Application.Models.Requests
{
    public class DeviceRequest
    {
        public class Add
        {
            public string DeviceName { get; set; } = string.Empty;
            public string Location { get; set; } = string.Empty;
        }

        public class Update
        {
            public string? DeviceName { get; set; }
            public string? Location { get; set; }
        }
    }
}
