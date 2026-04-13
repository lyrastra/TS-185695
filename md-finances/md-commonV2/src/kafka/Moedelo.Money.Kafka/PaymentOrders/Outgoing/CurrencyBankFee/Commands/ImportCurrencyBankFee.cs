using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using System;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyBankFee.Commands
{
    public class ImportCurrencyBankFee : IEntityCommandData
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Сумма платежа в валюте
        /// </summary>
        public decimal Sum { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Итог валютной операции в рублях
        /// </summary>
        public decimal TotalSum { get; set; }

        public string SourceFileId { get; set; }

        public int? ImportRuleId { get; set; }
    }
}
