using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Payroll.Kafka.Abstractions.Events
{
    public class WorkContractWithDebtEvent : IEntityEventData
    {
        public DateTime ChargeDate { get; set; }
        public string EventDescription { get; set; }
        public string Message { get; set; }
        public int FirmId { get; set; }
    }
}