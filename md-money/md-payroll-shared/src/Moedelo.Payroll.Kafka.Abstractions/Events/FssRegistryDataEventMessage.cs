using System;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Payroll.Enums.Allowances;

namespace Moedelo.Payroll.Kafka.Abstractions.Events
{
    public class FssRegistryDataEventMessage : MoedeloKafkaMessageValueBase
    {
        public long Id { get; set; }

        public int FirmId { get; set; }

        public int UserId { get; set; }

        public ChargeType ChargeType { get; set; }

        public int WorkerId { get; set; }

        public EventCudType EventType { get; set; }

        public Guid RequestId { get; set; }
    }
}