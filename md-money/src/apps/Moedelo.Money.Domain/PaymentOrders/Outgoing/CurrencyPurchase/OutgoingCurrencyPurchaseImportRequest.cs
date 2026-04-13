using Moedelo.Money.Domain.TaxPostings;
using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.Domain.PaymentOrders.Outgoing.CurrencyPurchase
{
    public class OutgoingCurrencyPurchaseImportRequest
    {
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int SettlementAccountId { get; set; }

        /// <summary>
        /// Может быть null, только при OperationState = 11
        /// </summary>
        public int? ToSettlementAccountId { get; set; }

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

        public long? DuplicateId { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }

        public int? ImportRuleId { get; set; }

        public int? ImportLogId { get; set; }
    }
}