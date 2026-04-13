using Moedelo.Common.Kafka.Abstractions.Base;

namespace Moedelo.ReceiptStatement.Kafka.Abstractions
{
    public sealed class CUDEventMessageValue : MoedeloKafkaMessageValueBase
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
