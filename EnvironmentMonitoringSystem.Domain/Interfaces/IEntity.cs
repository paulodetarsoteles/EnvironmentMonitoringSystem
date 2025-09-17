namespace EnvironmentMonitoringSystem.Domain.Interfaces
{
    public interface IEntity
    {
        Guid Id { get; set; }
        public DateTime? DeletionDate { get; set; }
    }
}
