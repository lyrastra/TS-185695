using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Business.Abstractions.Events
{
    public sealed class TaxationSystemChangedEventMessageValue : MoedeloKafkaMessageValueBase
    {
        public long DocumentBaseId { get; set; }

        public TaxationSystemType TaxationSystemType { get; set; }

        public Guid Guid { get; set; }
    }
}
