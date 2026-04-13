using System;
using System.Collections.Generic;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Billing.Kafka.Abstractions.Payments.Events
{
    public class PaymentHistoryChangeEventData : IEntityEventData
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
            public bool IsDownload { get; set; }
            public IReadOnlyCollection<PaymentPosition> PaymentPositions { get; set; }

            // пока тут не все поля из dbo.PaymentHistory
        }
        
        public class PaymentPosition
        {
            public string Type { get; set; }

            public string OneTimeType { get; set; }

            public decimal Price { get; set; }

            public decimal MinPrice { get; set; }

            public string Name { get; set; }

            public bool HasNds { get; set; }
            
            public bool IsExcludedFrom1C { get; set; }

            public decimal RegionalRatio { get; set; } = 1.0m;

            public string ProductCode { get; set; } = null;

            public string ProductConfigurationCode { get; set; }

            public DateTime? StartDate { get; set; } = null;

            public DateTime? EndDate { get; set; } = null;

            public string NameForEmail { get; set; }
            
            public decimal? NormativePrice { get; set; }
            
            public IReadOnlyCollection<PaymentPositionSeller> Sellers { get; set; }
        }
        
        public class PaymentPositionSeller
        {
            public int UserId { get; set; }

            public decimal ShareSum { get; set; }
        }
    }
}