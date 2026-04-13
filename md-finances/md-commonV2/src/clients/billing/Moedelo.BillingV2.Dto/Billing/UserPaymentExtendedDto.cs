using System;


namespace Moedelo.BillingV2.Dto.Billing
{
    public class UserPaymentExtendedDto
    {
        public int Id { get; set; }

        public int FirmId { get; set; }

        public string PaymentMethod { get; set; }

        public decimal Summ { get; set; }

        public bool Success { get; set; }

        public DateTime? PaymentDate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public int PriceListId { get; set; }

        public int Tariff { get; set; }

        public string TariffFullName { get; set; }

        public int PromoCodeId { get; set; }

        public string Note { get; set; }

        public int? OutsourcingTariff { get; set; }

        public bool Tracked { get; set; }

        public bool? IsBiz { get; set; }

        public bool? IsPro { get; set; }

        public bool IsMobileTariff { get; set; }

        public bool IsWithAccess { get; set; }
    }
}