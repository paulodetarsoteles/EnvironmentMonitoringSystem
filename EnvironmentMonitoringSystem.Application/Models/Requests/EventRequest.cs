namespace EnvironmentMonitoringSystem.Application.Models.Requests
{
    public class EventRequest
    {
        public class Receive
        {
            public Guid DeviceId { get; set; }
            public DateTime Timestamp { get; set; }
            public double Temperature { get; set; }
            public double Humidity { get; set; }
            public bool IsAlarm { get; set; }
        }
    }
}
