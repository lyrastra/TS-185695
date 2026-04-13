using System;
using Moedelo.Money.Enums;

namespace Moedelo.Money.ApiClient.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Models
{
    public class OrderTaxWidgetResponse
    {
        public long DocumentBaseId { get; set; }
        public DateTime Date { get; set; }
        public decimal Sum { get; set; }
        public BudgetaryPeriodType PeriodType { get; set; }
        public int PeriodNumber { get; set; }
    }
}