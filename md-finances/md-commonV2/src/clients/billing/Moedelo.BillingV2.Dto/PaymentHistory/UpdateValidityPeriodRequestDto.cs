using System;

namespace Moedelo.BillingV2.Dto.PaymentHistory
{
    public class UpdateValidityPeriodRequestDto
    {
        public int PaymentHistoryId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}