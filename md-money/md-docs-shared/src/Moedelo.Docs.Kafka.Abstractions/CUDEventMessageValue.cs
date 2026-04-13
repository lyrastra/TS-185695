using Moedelo.Common.Kafka.Abstractions;
using Moedelo.Common.Kafka.Abstractions.Base;

namespace Moedelo.Docs.Kafka.Abstractions
{
    public sealed class DocsCudEventMessageValue : MoedeloKafkaMessageValueBase
    {
        public DocsCudEventType EventType { get; set; }

        public string EventDataJson { get; set; }
    }

    public enum DocsCudEventType
    {
        Created = 1,
        Updated = 2,
        Deleted = 3,
        SignStatusUpdated = 4
    }
}