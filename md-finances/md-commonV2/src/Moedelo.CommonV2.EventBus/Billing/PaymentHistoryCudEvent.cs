using System;

namespace Moedelo.CommonV2.EventBus.Billing
{
    public class PaymentHistoryCudEvent
    {
        public OperationType Operation { get; set; }
        public PaymentHistory PreviousState { get; set; }
        public PaymentHistory NewState { get; set; }

        public enum OperationType
        {
            NoOperation = 0,
            Insert = 1,
            Update = 2,
            Delete = 3 // этого события пока нет (надо доделывать)
        }

        public class PaymentHistory
        {
            public int Id { get; set; }
            public int FirmId { get; set; }
            public int PriceListId { get; set; }
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
            public bool Success { get; set; }

            // пока тут не все поля из dbo.PaymentHistory
        }
    }
}