using Moedelo.Common.Kafka.Abstractions.Entities.Messages;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands
{
    public interface IMoedeloEntityCommandKafkaTopicReader : IMoedeloEntityMessageKafkaTopicReader<MoedeloEntityCommandKafkaMessageValue>
    {
    }
}