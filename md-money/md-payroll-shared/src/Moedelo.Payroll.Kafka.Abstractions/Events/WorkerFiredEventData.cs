using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Payroll.Kafka.Abstractions.Events
{
    public class WorkerFiredEventData : IEntityEventData
    {
        public int FirmId { get; set; }

        public int UserId { get; set; }

        public int WorkerId { get; set; }

        public string WorkerShortName { get; set; }

        public DateTime FiredDate { get; set; }

        public bool HasAlimentDeductions { get; set; }
    }
}