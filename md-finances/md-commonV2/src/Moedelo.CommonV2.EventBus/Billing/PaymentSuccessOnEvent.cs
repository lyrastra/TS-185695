using System;
using Moedelo.Common.Enums.Enums;

namespace Moedelo.CommonV2.EventBus.Billing
{
    public class PaymentSuccessOnEvent
    {
        public class FirmInfo
        {
            public int Id { get; set; }
            public int MainUserId { get; set; }
            public bool IsInternal { get; set; }
        }
        
        public class PriceList
        {
            public int Id { get; set; }
        }

        public class PaymentTariff
        {
            /// <summary> Идентификатор </summary>
            public int Id { get; set; }

            public ProductGroups ProductGroup { get; set; }
            public bool IsOneTime { get; set; }
            public bool IsWithAccess { get; set; }
        }

        public class PaymentHistory
        {
            public int Id { get; set; }
            public int? OutsourcingPriceListId { get; set; }
            public string PaymentMethod { get; set; }
            public decimal Sum { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? ExpirationDate { get; set; }
            public DateTime? RefundDate { get; set; }
            public DateTime? Date { get; set; }
            public DateTime? IncomingDate { get; set; }
            public int PromoCodeId { get; set; }
            public int SellerId { get; set; }
            public int RegionalPartnerId { get; set; }
            public bool IsRefund { get; set; }
            public bool Reselling { get; set; }
            public long PaymentId { get; set; }
        }

        public class PaymentHistoryEx
        {
            public int Id { get; set; }
            public string BillNumber { get; set; }
        }

        public class PaymentInfo
        {
            public PaymentHistory Payment { get; set; }
            public PaymentHistoryEx PaymentBill { get; set; }
            public PriceList PriceList { get; set; }
            public PaymentTariff Tariff { get; set; }
        }
        
        public int UserId { get; set; }

        public FirmInfo Firm { get; set; }

        /// <summary>
        /// переключаемый платёж
        /// </summary>
        public PaymentInfo SwitchOn { get; set; }

        /// <summary>
        /// это первый реальный платёж (до него были только триальные либо технические платежи)
        /// внимание: платёж по одноразовой услуге тоже считается реальным
        /// </summary>
        public bool IsFirstRealPayment { get; set; }

        /// <summary>
        /// Тип платежа в отчёте продаж: true - продление, false - новый
        /// </summary>
        public bool IsProlongPayment { get; set; }
    }
}