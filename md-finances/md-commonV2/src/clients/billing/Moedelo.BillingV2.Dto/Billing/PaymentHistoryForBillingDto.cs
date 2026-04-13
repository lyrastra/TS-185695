using System;
using System.Collections.Generic;
using Moedelo.BillingV2.Dto.Billing.PaymentPositions;

namespace Moedelo.BillingV2.Dto.Billing
{
    public class PaymentHistoryForBillingDto
    {
        public int Id { get; set; }

        public string ExternalPaymentId { get; set; }

        public int PriceListId { get; set; }

        public int OutsourcePriceListId { get; set; }

        public string PriceListName { get; set; }

        public string OutsourceListName { get; set; }

        public decimal Sum { get; set; }

        public decimal DiscountSum { get; set; }

        public DateTime? IncomingDate { get; set; }

        public DateTime? DocumentDate { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsSuccess { get; set; }

        public bool IsRefund { get; set; }

        public DateTime? RefundDate { get; set; }

        public bool IsReselling { get; set; }

        public bool IsDownloaded { get; set; }

        public string BillNumber { get; set; }

        public DateTime? BillDate { get; set; }

        public string PaymentMethod { get; set; }

        public int PartnerUserId { get; set; }

        public string PartnerUserLogin { get; set; }

        public string PartnerName { get; set; }

        public int SalerUserId { get; set; }

        public string SalerUserLogin { get; set; }

        public int PromoCodeId { get; set; }

        public string PromoCodeName { get; set; }

        public string Note { get; set; }

        public int FirstPaymentId { get; set; }

        public bool IsOneTime { get; set; }

        public int? PrimaryPaymentId { get; set; }

        public List<PaymentPositionDto> Positions { get; set; }
    }
}