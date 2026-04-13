using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Billing.Kafka.Abstractions.Payments.Events
{
    public class PaymentEventData : IEntityEventData
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public int FirmId { get; set; }
        public int PriceListId { get; set; }
        public int? OutsourcingPriceListId { get; set; }
        public bool Success { get; set; }
        public bool IsRefund { get; set; }
        public bool Reselling { get; set; }
        public bool IsOneTime { get; set; }
        public string PaymentMethod { get; set; }
        public int PromoCodeId { get; set; }
        public decimal Summ { get; set; }
        public DateTime DateRegistration { get; set; }
        public DateTime DatePayment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public bool IsFirstPayment { get; set; }
        public decimal PriceSum { get; set; }
        public int PercentDiscount { get; set; }
        public int TariffId { get; set; }
        public string TariffName { get; set; }
        public string ProductGroup { get; set; }
        public string ProductPlatform { get; set; }
        public long PaymentId { get; set; }
        public int SellerId { get; set; }
        public bool IsProlongPayment { get; set; }
    }
}