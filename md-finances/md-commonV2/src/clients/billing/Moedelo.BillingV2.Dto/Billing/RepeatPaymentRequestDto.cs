using System;

namespace Moedelo.BillingV2.Dto.Billing
{
    public class RepeatPaymentRequestDto
    {
        public int FirmId { get; set; }

        public bool IsOutsource { get; set; }

        public int PriceListId { get; set; }

        public int OutsourcingTariff { get; set; }

        public decimal PriceListPrice { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int PromoCodeId { get; set; }

        public int SalerId { get; set; }
        
        public int RegionalPartnerId { get; set; }
    }
}