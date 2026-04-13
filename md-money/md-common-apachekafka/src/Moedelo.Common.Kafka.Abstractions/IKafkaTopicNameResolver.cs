namespace Moedelo.Common.Kafka.Abstractions
{
    public interface IKafkaTopicNameResolver
    {
        string GetTopicPrefix();
        string GetTopicFullName(string baseTopicName);
    }
}
