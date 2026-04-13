using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.CurrencyPayments.Models
{
    public class CurrencyPaymentOrderDto
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }
        
        public string Number { get; set; }

        public decimal Sum { get; set; }
        
        public decimal TotalSum { get; set; }

        public int SettlementAccountId { get; set; }

        public MoneyDirection Direction { get; set; }

        public OperationType OperationType { get; set; }
        
        public bool ProvideInAccounting { get; set; }
        
        public bool IsPaid { get; set; }
        
        public bool IsBadOperationState { get; set; }
    }
}