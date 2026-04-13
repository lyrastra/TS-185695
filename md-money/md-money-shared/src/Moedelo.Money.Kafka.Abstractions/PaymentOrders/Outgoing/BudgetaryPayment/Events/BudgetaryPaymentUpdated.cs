using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Enums;
using Moedelo.Money.Kafka.Abstractions.Models;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Events
{
    public class BudgetaryPaymentUpdated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int SettlementAccountId { get; set; }

        public decimal Sum { get; set; }

        public string Description { get; set; }

        public BudgetaryAccountCodes AccountCode { get; set; }

        public int? KbkId { get; set; }

        public string KbkNumber { get; set; }

        public KbkPaymentType KbkPaymentType { get; set; }

        public BudgetaryPeriod Period { get; set; }

        public int? TradingObjectId { get; set; }

        public bool ProvideInAccounting { get; set; }

        public bool IsManualTaxPostings { get; set; }

        public bool IsPaid { get; set; }
        
        public IReadOnlyCollection<DocumentLink> CurrencyInvoicesLinks { get; set; }

        /// <summary>
        /// статус плательщика
        /// </summary>
        public BudgetaryPayerStatus PayerStatus { get; set; }

        /// <summary>
        /// основание платежа
        /// </summary>
        public BudgetaryPaymentBase PaymentBase { get; set; }
        
        /// <summary>
        /// дата документа
        /// </summary>
        public string DocumentDate { get; set; }

        /// <summary>
        /// номер документа
        /// </summary>
        public string DocumentNumber { get; set; }

        /// <summary>
        /// код
        /// </summary>
        public string Uin { get; set; }
        
        /// <summary>
        /// получатель платежа
        /// </summary>
        public BudgetaryPaymentRecipient Recipient { get; set; }

        public OperationState OperationState { get; set; }

        public OutsourceState? OutsourceState { get; set; }
    }
}