using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using System;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.TransferFromAccount.Commands
{
    public class ImportDuplicateTransferFromAccount : IEntityCommandData
    {
        public long DuplicateId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int FromSettlementAccountId { get; set; }

        public int SettlementAccountId { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public string SourceFileId { get; set; }

        public int? ImportRuleId { get; set; }
    }
}
