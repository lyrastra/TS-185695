using System;
using System.Collections.Generic;
using Moedelo.Docs.Kafka.Abstractions.Purchases.CurrencyInvoices.Models;
using Moedelo.Requisites.Enums.SettlementAccounts;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.CurrencyInvoices.Messages
{
    public class PurchaseCurrencyInvoiceUpdatedMessage
    {
        public long DocumentBaseId { get; set; }
        
        public string DocumentNumber { get; set; }
        
        public DateTime DocumentDate { get; set; }
        
        public decimal Sum { get; set; }
        
        public int KontragentId { get; set; }
        
        public List<PaymentLink> PaymentLinks { get; set; }
        
        public PaymentLink BudgetaryPaymentLink { get; set; }
        
        public long StockId { get; set; }
        
        public DateTime AccountingDate { get; set; }
        
        public Currency Currency { get; set; }
        
        public bool IsEaeunionCountry { get; set; }
        
        public List<PurchaseCurrencyInvoiceItemMessage> Items { get; set; }
        
        public bool ProvideInAccounting { get; set; }
        
        public List<PaymentLink> OldPaymentLinks { get; set; }
        
        /// <summary>
        /// Флаг, нужно ли перепровести связанные с инвойсом валютные платежи.
        /// При изменении инвойса - перепроводить связанные платежи нужно.
        /// При перепроведении инвойса - не нужно
        /// </summary>
        public bool NeedReprovideLinkedDocuments { get; set; }
    }
}