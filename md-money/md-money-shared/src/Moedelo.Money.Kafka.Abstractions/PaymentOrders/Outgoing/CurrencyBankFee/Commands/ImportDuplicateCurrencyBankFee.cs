using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Common;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee.Commands
{
    public class ImportDuplicateCurrencyBankFee : IEntityCommandData
    {
        public long DuplicateId { get; set; }

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

        public int ImportId { get; set; }

        

        /// <summary>
        /// Правило импорта применённое к операции
        /// </summary>
        public int? ImportRuleId { get; set; }

        public int? ImportLogId { get; set; }

        /// <summary>
        /// Признак: на обработке в Ауте ("жёлтая таблица")
        /// </summary>
        public bool NeedOutsourceProcessing { get; set; }
    }
}
