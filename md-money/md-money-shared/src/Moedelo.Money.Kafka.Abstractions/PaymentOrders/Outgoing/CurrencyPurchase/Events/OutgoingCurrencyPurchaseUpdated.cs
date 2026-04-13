using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase.Events
{
    public class OutgoingCurrencyPurchaseUpdated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }
        
        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int SettlementAccountId { get; set; }

        public int ToSettlementAccountId { get; set; }

        /// <summary>
        /// Сумма платежа в рублях
        /// </summary>
        public decimal Sum { get; set; }

        public string Description { get; set; }

        public bool ProvideInAccounting { get; set; }

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
        
        /// <summary>
        /// Признак: проводки отредактированы вручную
        /// </summary>
        public bool IsManualTaxPostings { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }
    }
}