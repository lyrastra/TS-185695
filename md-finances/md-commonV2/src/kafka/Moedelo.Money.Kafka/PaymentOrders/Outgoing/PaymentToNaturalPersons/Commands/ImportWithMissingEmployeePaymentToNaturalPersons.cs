using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using System;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.PaymentToNaturalPersons.Commands
{
    public class ImportWithMissingEmployeePaymentToNaturalPersons : IEntityCommandData
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public string Description { get; set; }

        public decimal PaymentSum { get; set; }

        public PaymentToNaturalPersonsType PaymentType { get; set; }

        public int SettlementAccountId { get; set; }

        public string SourceFileId { get; set; }

        public int? ImportRuleId { get; set; }
    }
}
