using System;
using Moedelo.Common.Enums.Enums.Billing;

namespace Moedelo.BillingV2.Dto.Billing
{
    public class PaymentHistoryExDto
    {
        public int PaymentHistoryId { get; set; }
        public string BillNumber { get; set; }
        public DateTime BillDate { get; set; }
        public int SalesChannel { get; set; }
        public BillingOperationType OperationType { get; set; }
        public bool IsOutsource { get; set; }
        public int? FirstPayId { get; set; }
        public int? PrimaryPaymentId { get; set; }
    }
}