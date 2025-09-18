using EnvironmentMonitoringSystem.Application.Models.Requests;
using EnvironmentMonitoringSystem.Application.Services;
using EnvironmentMonitoringSystem.Domain.Models;
using EnvironmentMonitoringSystem.Infrastructure.Repositories.Interfaces;
using Moq;

namespace EnvironmentMonitoringSystem.Test.Services
{
    public class EventServiceTests
    {
        [Fact]
        public async Task RegisterEvent_Success()
        {
            // Arrange
            var mockRepo = new Mock<IEventRepository>();
            mockRepo.Setup(x => x.Add(It.IsAny<Event>())).ReturnsAsync(true);

            var service = new EventService(mockRepo.Object, null, null);

            var request = new EventRequest.Receive
            {
                DeviceId = Guid.NewGuid(),
                Timestamp = DateTime.UtcNow,
                Temperature = 25,
                Humidity = 50,
                IsAlarm = false
            };

            // Act
            var result = await service.RegisterEventAsync(request);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RegisterEvent_Failure_DeviceNotFound()
        {
            // Arrange
            var mockRepo = new Mock<IEventRepository>();
            mockRepo.Setup(x => x.Add(It.IsAny<Event>())).ThrowsAsync(new Exception("Device not found"));

            var service = new EventService(mockRepo.Object, null, null);

            var request = new EventRequest.Receive
            {
                DeviceId = Guid.NewGuid(),
                Timestamp = DateTime.UtcNow,
                Temperature = 25,
                Humidity = 50,
                IsAlarm = false
            };

            // Act
            var result = await service.RegisterEventAsync(request);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task RegisterEvent_Failure_InvalidDeviceId()
        {
            // Arrange
            var mockRepo = new Mock<IEventRepository>();
            mockRepo.Setup(x => x.Add(It.IsAny<Event>())).ThrowsAsync(new Exception("Invalid device id"));

            var service = new EventService(mockRepo.Object, null, null);

            var request = new EventRequest.Receive
            {
                DeviceId = Guid.Empty,
                Timestamp = DateTime.UtcNow,
                Temperature = 0,
                Humidity = 50,
                IsAlarm = false
            };

            // Act
            var result = await service.RegisterEventAsync(request);

            // Assert
            Assert.False(result);
        }
    }
}
