using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Business.Abstractions.Commands.PurseOperations
{
    public sealed class PurseOperationChangeTaxationSystemCommandMessageValue : MoedeloKafkaMessageValueBase
    {
        public long DocumentBaseId { get; set; }

        public TaxationSystemType TaxationSystemType { get; set; }

        public Guid Guid { get; set; }
    }
}
