using System;

namespace Moedelo.BillingV2.Dto.FirmBillingState
{
    public class FirmBillingDatesDto
    {
        public DateTime FirstPaymentStartDate { get; set; }
        public DateTime CurrentPaymentStartDate { get; set; }
        public DateTime CurrentPaymentExpiryDate { get; set; }
        public DateTime LastPaidDate { get; set; }
    }
}
