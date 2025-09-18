using EnvironmentMonitoringSystem.Application.Models.Requests;
using EnvironmentMonitoringSystem.Application.Services;
using EnvironmentMonitoringSystem.Application.Services.Interfaces;
using EnvironmentMonitoringSystem.Domain.Models;
using EnvironmentMonitoringSystem.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;

namespace EnvironmentMonitoringSystem.Test.Services
{
    public class DeviceServiceTests
    {
        [Fact]
        public async Task CreateDevice_Success()
        {
            // Arrange
            var mockRepo = new Mock<IDeviceRepository>();
            var mockEventService = new Mock<IEventService>();
            var mockConfig = new Mock<IConfiguration>();

            var deviceId = Guid.NewGuid();

            mockEventService.Setup(x => x.RegisterDeviceOnIot(It.IsAny<DeviceRequest.Add>()))
                .ReturnsAsync(deviceId);

            mockRepo.Setup(x => x.Add(It.IsAny<Device>())).ReturnsAsync(true);

            var service = new DeviceService(mockRepo.Object, mockEventService.Object);

            var request = new DeviceRequest.Add { DeviceName = "Teste", Location = "Lab" };

            // Act
            var result = await service.CreateAsync(request);

            // Assert
            Assert.True(result.Success);
            Assert.True(result.Data);
        }

        [Fact]
        public async Task CreateDevice_Failure_DeviceNotRegistered()
        {
            // Arrange
            var mockRepo = new Mock<IDeviceRepository>();
            var mockEventService = new Mock<IEventService>();
            var deviceId = Guid.Empty;

            mockEventService.Setup(x => x.RegisterDeviceOnIot(It.IsAny<DeviceRequest.Add>()))
                .ReturnsAsync(deviceId); 

            var service = new DeviceService(mockRepo.Object, mockEventService.Object);
            var request = new DeviceRequest.Add { DeviceName = "Teste", Location = "Lab" };

            // Act
            var result = await service.CreateAsync(request);

            // Assert
            Assert.False(result.Success);
            Assert.Contains("Erro ao registrar o dispositivo no IoT", result.ErrorMessages);
        }

        [Fact]
        public async Task CreateDevice_Fail()
        {
            // Arrange
            var mockRepo = new Mock<IDeviceRepository>();
            var mockEventService = new Mock<IEventService>();
            var mockConfig = new Mock<IConfiguration>();

            var deviceId = Guid.NewGuid();

            mockEventService.Setup(x => x.RegisterDeviceOnIot(It.IsAny<DeviceRequest.Add>()))
                .ReturnsAsync(deviceId);

            mockRepo.Setup(x => x.Add(It.IsAny<Device>())).ReturnsAsync(true);

            var service = new DeviceService(mockRepo.Object, mockEventService.Object);

            var request = new DeviceRequest.Add { DeviceName = string.Empty, Location = string.Empty };

            // Act
            var result = await service.CreateAsync(request);

            // Assert
            Assert.False(result.Success);
            Assert.False(result.Data);
        }
    }
}
