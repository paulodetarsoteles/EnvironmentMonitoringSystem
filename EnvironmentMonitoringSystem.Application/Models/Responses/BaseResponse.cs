namespace EnvironmentMonitoringSystem.Application.Models.Responses
{
    public class BaseResponse<T>
    {
        public bool Success { get; set; } = true;
        public List<string> ErrorMessages { get; set; } = [];
        public T? Data { get; set; }
    }
}
