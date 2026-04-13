using Moedelo.Money.Enums;
using System;

namespace Moedelo.Money.PaymentOrders.Domain.Models.Snapshot
{
    public class BudgetaryFields
    {
        public BudgetaryPeriod Period { get; set; }
        public string Kbk { get; set; }
        public BudgetaryPayerStatus PayerStatus { get; set; }
        public BudgetaryPaymentBase PaymentBase { get; set; }
        public string DocNumber { get; set; }
        public string DocDate { get; set; }
        public BudgetaryPaymentType PaymentType { get; set; }
        public string CodeUin { get; set; }
        public string PayerKpp { get; set; }
    }
}