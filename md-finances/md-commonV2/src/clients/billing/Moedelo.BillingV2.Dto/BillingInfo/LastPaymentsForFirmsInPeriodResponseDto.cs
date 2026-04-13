using System;

namespace Moedelo.BillingV2.Dto.BillingInfo
{
    public class LastPaymentsForFirmsInPeriodResponseDto
    {
        public int FirmId { get; set; }

        public int PaymentId { get; set; }

        public int PriceListId { get; set; }

        public DateTime Date { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}