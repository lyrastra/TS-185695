using System;

namespace Moedelo.BillingV2.Dto.Billing
{
    public class RepeatPaymentResponseDto
    {
        public int FirmId { get; set; }

        public int PaymentId { get; set; }

        public DateTime BillDate { get; set; }

        public string BillNumber { get; set; }
    }
}