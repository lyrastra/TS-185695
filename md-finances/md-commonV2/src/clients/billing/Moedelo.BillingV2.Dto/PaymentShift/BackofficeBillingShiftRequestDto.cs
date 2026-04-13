using System;

namespace Moedelo.BillingV2.Dto.PaymentShift
{
    public class BackofficeBillingShiftRequestDto
    {
        public int PaymentHistoryId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
