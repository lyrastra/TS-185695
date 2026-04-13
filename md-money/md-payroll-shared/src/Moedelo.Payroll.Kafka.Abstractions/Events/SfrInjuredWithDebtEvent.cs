using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Payroll.Kafka.Abstractions.Events
{
    public class SfrInjuredWithDebtEvent : IEntityEventData
    {
        public string EventDescription { get; set; }
        public string Message { get; set; }
        public int FirmId { get; set; }
    }
}