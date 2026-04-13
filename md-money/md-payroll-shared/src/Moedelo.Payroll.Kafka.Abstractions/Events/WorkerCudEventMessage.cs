using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Payroll.Kafka.Abstractions.Extensions;

namespace Moedelo.Payroll.Kafka.Abstractions.Events
{
    public class WorkerCudEventMessage : IEntityEventData, IWorkerFio
    {
        public int Id { get; set; }

        public int FirmId { get; set; }

        public int UserId { get; set; }

        public bool IsStaff { get; set; }
        
        public EventCudType EventType { get; set; }

        public string Inn { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }
    }
}