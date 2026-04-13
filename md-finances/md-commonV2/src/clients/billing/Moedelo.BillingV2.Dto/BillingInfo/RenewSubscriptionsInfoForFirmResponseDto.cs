using System;

namespace Moedelo.BillingV2.Dto.BillingInfo
{
    public class RenewSubscriptionsInfoForFirmResponseDto
    {
        public int FirmId { get; set; }

        public int OldPriceListId { get; set; }

        public DateTime OldPaymentStartDate { get; set; }

        public DateTime OldPaymentEndDate { get; set; }

        public decimal OldPaymentDiscountSum { get; set; }

        public decimal OldPaymentSum { get; set; }

        public bool IsRenewed { get; set; }

        public DateTime? RenewDate { get; set; }

        public int? NewPriceListId { get; set; }

        public DateTime? NewPaymentStartDate { get; set; }

        public DateTime? NewPaymentEndDate { get; set; }

        public decimal? NewPaymentDiscountSum { get; set; }

        public decimal? NewPaymentSum { get; set; }
    }
}