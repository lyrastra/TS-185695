using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencySale.Events
{
    public class OutgoingCurrencySaleCreated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int SettlementAccountId { get; set; }

        public int ToSettlementAccountId { get; set; }

        /// <summary>
        /// Сумма платежа в валюте
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
        /// Итог валютной операции в рублях
        /// </summary>
        public decimal TotalSum { get; set; }

        public bool ProvideInAccounting { get; set; }

        public bool IsManualTaxPostings { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        /// <summary>
        /// Признак: нужно ли зафиксировать номер платежа в нумерации
        /// </summary>
        public bool IsSaveNumeration { get; set; }

        /// <summary>
        /// Идентификаторы применённых правил импорта
        /// </summary>
        public int[] ImportRuleIds { get; set; }

        /// <summary>
        /// Идентификатор лога импорта
        /// </summary>
        public int? ImportLogId { get; set; }
    }
}