using EnvironmentMonitoringSystem.Application.Models.Requests;
using EnvironmentMonitoringSystem.Application.Models.Responses;
using EnvironmentMonitoringSystem.Application.Services.Interfaces;
using EnvironmentMonitoringSystem.Domain.Models;
using EnvironmentMonitoringSystem.Infrastructure.Repositories.Interfaces;

namespace EnvironmentMonitoringSystem.Application.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IEventService _eventService;
        private readonly List<string> notifications = [];

        public DeviceService(IDeviceRepository deviceRepository, IEventService eventService)
        {
            _deviceRepository = deviceRepository;
            _eventService = eventService;
        }

        public async Task<BaseResponse<List<Device>>> ListAsync()
        {
            var devices = await _deviceRepository.List();

            if (devices == null || !devices.Any())
            {
                return new BaseResponse<List<Device>>
                {
                    Success = false,
                    ErrorMessages = ["Nenhum dispositivo encontrado"]
                };
            }

            return new BaseResponse<List<Device>>
            {
                Data = devices,
                Success = true,
                ErrorMessages = []
            };
        }

        public async Task<BaseResponse<Device>> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new BaseResponse<Device>
                {
                    Success = false,
                    ErrorMessages = ["ID inválido"]
                };
            }

            var device = await _deviceRepository.GetById(id);

            if (device == null)
            {
                return new BaseResponse<Device>
                {
                    Success = false,
                    ErrorMessages = ["Dispositivo não encontrado"]
                };
            }

            return new BaseResponse<Device>
            {
                Data = device,
                Success = true,
                ErrorMessages = []
            };
        }

        public async Task<BaseResponse<bool>> CreateAsync(DeviceRequest.Add request)
        {
            try
            {
                if (request == null)
                {
                    return new BaseResponse<bool>
                    {
                        Success = false,
                        ErrorMessages = ["Requisição inválida"]
                    };
                }

                if (IsValidRequestAdd(request) == false)
                {
                    return new BaseResponse<bool>
                    {
                        Success = false,
                        ErrorMessages = notifications
                    };
                }

                var deviceId = await _eventService.RegisterDeviceOnIot(request);

                if (deviceId == Guid.Empty)
                {
                    return new BaseResponse<bool>
                    {
                        Success = false,
                        ErrorMessages = ["Erro ao registrar o dispositivo no IoT"]
                    };
                }

                var hasSaved = await _deviceRepository.Add(new Device
                {
                    Id = deviceId,
                    DeviceName = request.DeviceName,
                    Location = request.Location,
                    CreationDate = DateTime.UtcNow,
                    LastUpdate = null,
                    DeletionDate = null
                });

                if (hasSaved == false)
                {
                    return new BaseResponse<bool>
                    {
                        Success = false,
                        ErrorMessages = ["Erro ao salvar o dispositivo"]
                    };
                }

                return new BaseResponse<bool>
                {
                    Data = true,
                    Success = true,
                    ErrorMessages = []
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Success = false,
                    ErrorMessages = [ex.Message]
                };
            }
        }

        public async Task<BaseResponse<bool>> UpdateAsync(Guid id, DeviceRequest.Update request)
        {
            if (id == Guid.Empty)
            {
                return new BaseResponse<bool>
                {
                    Success = false,
                    ErrorMessages = ["ID inválido"]
                };
            }

            if (request == null)
            {
                return new BaseResponse<bool>
                {
                    Success = false,
                    ErrorMessages = ["Requisição inválida"]
                };
            }

            if (IsValidRequestUpdate(request) == false)
            {
                return new BaseResponse<bool>
                {
                    Success = false,
                    ErrorMessages = notifications
                };
            }

            var existingDevice = await _deviceRepository.GetById(id);

            if (existingDevice == null)
            {
                return new BaseResponse<bool>
                {
                    Success = false,
                    ErrorMessages = ["Dispositivo não encontrado"]
                };
            }

            existingDevice.DeviceName = request.DeviceName ?? existingDevice.DeviceName;
            existingDevice.Location = request.Location ?? existingDevice.Location;
            existingDevice.LastUpdate = DateTime.UtcNow;

            var hasUpdated = await _deviceRepository.Update(existingDevice);

            if (hasUpdated == false)
            {
                return new BaseResponse<bool>
                {
                    Success = false,
                    ErrorMessages = ["Erro ao atualizar o dispositivo"]
                };
            }

            return new BaseResponse<bool>
            {
                Data = true,
                Success = true,
                ErrorMessages = []
            };
        }

        public async Task<BaseResponse<bool>> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return new BaseResponse<bool>
                {
                    Success = false,
                    ErrorMessages = ["ID inválido"]
                };
            }

            var existingDevice = await _deviceRepository.GetById(id);

            if (existingDevice == null)
            {
                return new BaseResponse<bool>
                {
                    Success = false,
                    ErrorMessages = ["Dispositivo não encontrado"]
                };
            }

            var hasUnregistered = await _eventService.UnregisterDeviceOnIot(id);

            if (hasUnregistered == false) 
            {
                return new BaseResponse<bool>
                {
                    Success = false,
                    ErrorMessages = ["Erro ao remover o dispositivo do IoT"]
                };
            }

            existingDevice.DeletionDate = DateTime.UtcNow;

            var hasDeleted = await _deviceRepository.Delete(existingDevice);

            if (hasDeleted == false)
            {
                return new BaseResponse<bool>
                {
                    Success = false,
                    ErrorMessages = ["Erro ao deletar o dispositivo"]
                };
            }

            return new BaseResponse<bool>
            {
                Data = true,
                Success = true,
                ErrorMessages = []
            };
        }

        private bool IsValidRequestAdd(DeviceRequest.Add request)
        {
            if (string.IsNullOrEmpty(request.DeviceName) || request.DeviceName.Length < 1)
            {
                notifications.Add("Nome do dispositivo inválido");
            }
            if (string.IsNullOrEmpty(request.Location) || request.Location.Length < 1)
            {
                notifications.Add("Localização do dispositivo inválida");
            }
            if (notifications.Any()) return false;

            return true;
        }

        private bool IsValidRequestUpdate(DeviceRequest.Update request)
        {
            if (!string.IsNullOrEmpty(request.DeviceName) && request.DeviceName.Length < 1)
            {
                notifications.Add("Nome do dispositivo inválido");
            }
            if (!string.IsNullOrEmpty(request.DeviceName) && request.Location.Length < 1)
            {
                notifications.Add("Localização do dispositivo inválida");
            }
            if (notifications.Any()) return false;

            return true;
        }
    }
}
