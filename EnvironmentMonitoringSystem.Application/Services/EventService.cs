using EnvironmentMonitoringSystem.Application.Models.Requests;
using EnvironmentMonitoringSystem.Application.Models.Responses;
using EnvironmentMonitoringSystem.Application.Services.Interfaces;
using EnvironmentMonitoringSystem.Domain.Models;
using EnvironmentMonitoringSystem.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace EnvironmentMonitoringSystem.Application.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public EventService(IEventRepository eventRepository, IConfiguration configuration, HttpClient httpClient)
        {
            _eventRepository = eventRepository;
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<bool> RegisterEventAsync(EventRequest.Receive request)
        {
            try
            {
                var newEvent = new Event
                {
                    DeviceId = request.DeviceId,
                    TimeStamp = request.Timestamp,
                    Humidity = request.Humidity,
                    Temperature = request.Temperature,
                    IsAlarm = request.IsAlarm,
                    DeletionDate = null
                };

                var hasEventSaved = await _eventRepository.Add(newEvent);

                return hasEventSaved;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<BaseResponse<Event>>> ListLastEvents()
        {
            try
            {
                var results = await _eventRepository.ListLastEvents();

                if (results == null || results.Count == 0) return new List<BaseResponse<Event>>
                {
                    new() 
                    {
                        Data = null,
                        ErrorMessages = [],
                        Success = true
                    }
                };

                var response = results.Select(e => new BaseResponse<Event>
                {
                    Data = e,
                    ErrorMessages = [],
                    Success = true
                }).ToList();

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao recuperar lista dos últimos eventos");
            }
        }

        public async Task<Guid> RegisterDeviceOnIot(DeviceRequest.Add request)
        {
            string? urlCallBack = _configuration.GetSection("UrlCallBack").Value ??
                    throw new Exception("Erro ao capturar url no arquivo de configuração.");

            string? UrlApiIot = _configuration.GetSection("UrlApiIot").Value ??
                    throw new Exception("Erro ao capturar url no arquivo de configuração.");

            UrlApiIot = UrlApiIot.EndsWith("/") ? UrlApiIot + "register" : UrlApiIot + "/register";

            var deviceIOtRequest = new DeviceIotRequest
            {
                DeviceName = request.DeviceName,
                Location = request.Location,
                CallbackUrl = urlCallBack
            };

            var response = await _httpClient.PostAsJsonAsync(UrlApiIot, deviceIOtRequest);

            if (response == null || response.IsSuccessStatusCode != true) 
                throw new Exception("Erro ao cadastrar dispositivo na API Iot");

            var resultContent = await response.Content.ReadFromJsonAsync<DeviceIotResponse>();

            if (resultContent == null || resultContent.IntegrationId == null || resultContent.IntegrationId == Guid.Empty) 
                throw new Exception("Erro ao receber dados do dispositivo cadastrado na API Iot");

            return resultContent.IntegrationId.Value;
        }

        public async Task<bool> UnregisterDeviceOnIot(Guid deviceId)
        {
            string? UrlApiIot = _configuration.GetSection("UrlApiIot").Value ??
                    throw new Exception("Erro ao capturar url no arquivo de configuração.");

            UrlApiIot = UrlApiIot.EndsWith("/") ? UrlApiIot + "unregister" : UrlApiIot + "/unregister"; 
            UrlApiIot += $"/{deviceId}";

            var response = await _httpClient.DeleteAsync(UrlApiIot);

            if (response == null || response.IsSuccessStatusCode != true)
                throw new Exception("Erro ao cadastrar dispositivo na API Iot");

            return true;
        }
    }
}
