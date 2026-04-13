using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using System;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.PaymentToAccountablePerson.Commands
{
    public class ImportWithMissingEmployeePaymentToAccountablePerson : IEntityCommandData
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public string SourceFileId { get; set; }

        public int[] ImportRuleIds { get; set; }

        public bool IsIgnoreNumber { get; set; }
    }
}
