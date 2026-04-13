using System;
using Moedelo.Common.Enums.Enums;
using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.BillingV2.Dto.Billing
{
    public class PaymentHistoryWithExDto
    {
        public int Id { get; set; }

        public int PriceListId { get; set; }

        public string TariffName { get; set; }

        public string ProductPlatform { get; set; }

        public ProductGroups ProductGroups { get; set; }

        public int FirmId { get; set; }

        public string PaymentMethod { get; set; }

        public DateTime? Date { get; set; }

        public decimal Summ { get; set; }

        public long PaymentId { get; set; }

        public bool Success { get; set; }

        public bool IsRefund { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public DateTime? StartDate { get; set; }

        public int SalerId { get; set; }

        public int RegionalPartnerId { get; set; }

        public string Note { get; set; }

        public int PromoCodeId { get; set; }

        public string PromoCode { get; set; }

        public int ReferalId { get; set; }

        public DateTime? IncomingDate { get; set; }

        public DateTime? DocumentDate { get; set; }

        public bool? IsDownload { get; set; }

        public bool Reselling { get; set; }

        public bool Tracked { get; set; }

        public int? OutsourcingTariff { get; set; }

        public int AgentId { get; set; }

        public int PaymentHistoryId { get; set; }

        public string BillNumber { get; set; }

        public DateTime BillDate { get; set; }

        public int SalesChannel { get; set; }

        public int OperationType { get; set; }

        public int? FirstPayId { get; set; }

        public int? FirstPayInGroupId { get; set; }

        public int? PrimaryPaymentId { get; set; }

        public bool IsOutsource { get; set; }

        public decimal? DiscountSum { get; set; }

        public DateTime? RefundDate { get; set; }

        public Guid? SberbankPaymentGuid { get; set; }

        public SberbankPaymentStatus? SberbankPaymentStatus { get; set; }

        public bool IsOneTime { get; set; }

        public bool IsWithAccess { get; set; }
    }
}
