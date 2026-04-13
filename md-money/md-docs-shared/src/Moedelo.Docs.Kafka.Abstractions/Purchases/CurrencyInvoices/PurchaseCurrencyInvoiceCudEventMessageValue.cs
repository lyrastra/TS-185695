using Moedelo.Common.Kafka.Abstractions.Base;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.CurrencyInvoices
{
    public sealed class PurchaseCurrencyInvoiceCudEventMessageValue : MoedeloKafkaMessageValueBase
    {
        public CUDEventType EventType { get; set; }

        public string EventDataJson { get; set; }
    }

    public enum CUDEventType
    {
        Created = 1,
        Updated = 2,
        Deleted = 3,
    }
}