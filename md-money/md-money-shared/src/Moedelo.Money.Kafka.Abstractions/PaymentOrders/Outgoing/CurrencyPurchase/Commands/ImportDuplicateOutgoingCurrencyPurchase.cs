using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Common;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase.Commands
{
    public class ImportDuplicateOutgoingCurrencyPurchase : IEntityCommandData
    {
        public long DuplicateId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int SettlementAccountId { get; set; }

        public int ToSettlementAccountId { get; set; }

        /// <summary>
        /// Сумма платежа в рублях
        /// </summary>
        public decimal Sum { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Курс валюты на дату документа
        /// </summary>
        public decimal ExchangeRate { get; set; }

        /// <summary>
        /// Курсовая разница от курса ЦБ
        /// </summary>
        public decimal ExchangeRateDiff { get; set; }

        /// <summary>
        /// Итог валютной операции в валюте
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
