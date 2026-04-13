using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;

namespace Moedelo.PaymentOrderImport.Kafka.Abstractions.Rules.Commands
{
    public class ApplyIgnoreNumberCommand : IEntityCommandData
    {
        public int RuleId { get; set; }

        public DateTime StartDate { get; set; }
    }
}
