using Moedelo.Common.Kafka.Abstractions.Entities.Messages;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Events
{
    public interface IMoedeloEntityEventKafkaTopicReader  : IMoedeloEntityMessageKafkaTopicReader<MoedeloEntityEventKafkaMessageValue>
    {
    }
}
