namespace Moedelo.Common.Kafka.Abstractions.Base
{
    public interface IMoedeloKafkaMessageDefinition
    {
        string TopicName { get; set; }

        string KeyMessage { get; set; }
    }
}