using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using System;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyPaymentToSupplier.Commands
{
    public class ImportWithMissingMissingContractorCurrencyPaymentToSupplier : IEntityCommandData
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public decimal TotalSum { get; set; }

        public string Description { get; set; }

        public int SettlementAccountId { get; set; }

        public string SourceFileId { get; set; }

        public int? ImportRuleId { get; set; }
    }
}
