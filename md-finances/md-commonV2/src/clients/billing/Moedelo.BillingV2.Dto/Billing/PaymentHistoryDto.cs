using System;
using Moedelo.Common.Enums.Enums.Integration;

namespace Moedelo.BillingV2.Dto.Billing
{
    public class PaymentHistoryDto
    {
        public int Id { get; set; }

        public int FirmId { get; set; }

        public int PriceListId { get; set; }

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

        public int ReferalId { get; set; }

        public DateTime? IncomingDate { get; set; }

        public DateTime? DocumentDate { get; set; }

        public bool? IsDownload { get; set; }

        public bool Reselling { get; set; }

        public bool Tracked { get; set; }

        public int? OutsourcingTariff { get; set; }

        public int AgentId { get; set; }

        public decimal? DiscountSum { get; set; }

        public DateTime? RefundDate { get; set; }
        
        //возможно, не заполняется в PaymentHistoryApiClient
        public string Positions { get; set; }

        public Guid? SberbankPaymentGuid { get; set; }

        public SberbankPaymentStatus? SberbankPaymentStatus { get; set; }

        /// <summary>
        /// Триал ли?. 
        /// </summary>
        public bool IsTrial => PaymentMethod == "trial";

        /// <summary>
        /// Триал или Демо?. 
        /// </summary>
        public bool IsTrialOrDemo => PaymentMethod == "trial" || PaymentMethod == "demo";

    }
}
