using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Business.Abstractions.Commands
{
    public sealed class ChangeTaxationSystemCommandMessageValue : MoedeloKafkaMessageValueBase
    {
        public IReadOnlyCollection<long> DocumentBaseIds { get; set; }

        public TaxationSystemType TaxationSystemType { get; set; }

        public Guid Guid { get; set; }
    }
}
