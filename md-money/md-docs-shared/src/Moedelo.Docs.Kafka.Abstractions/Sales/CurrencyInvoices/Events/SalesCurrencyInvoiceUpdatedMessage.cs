using System;
using System.Collections.Generic;
using Moedelo.Docs.Kafka.Abstractions.Sales.CurrencyInvoices.Models;

namespace Moedelo.Docs.Kafka.Abstractions.Sales.CurrencyInvoices.Events
{
    public class SalesCurrencyInvoiceUpdatedMessage
    {
        public long DocumentBaseId { get; set; }
        
        public string DocumentNumber { get; set; }
        
        public DateTime DocumentDate { get; set; }
        
        public decimal Sum { get; set; }
        
        public long ContractBaseId { get; set; }
        
        public int KontragentId { get; set; }
        
        public List<PaymentLink> PaymentLinks { get; set; }
        
        public long StockId { get; set; }
        
        public int SettlementAccountId { get; set; }
        
        public List<SalesCurrencyInvoiceItemMessage> Items { get; set; }
        
        public bool ProvideInAccounting { get; set; }
    }
}