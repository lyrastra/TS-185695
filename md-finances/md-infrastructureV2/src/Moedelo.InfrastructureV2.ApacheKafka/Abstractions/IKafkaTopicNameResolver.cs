namespace Moedelo.InfrastructureV2.ApacheKafka.Abstractions
{
    public interface IKafkaTopicNameResolver
    {
        string GetTopicFullName(string baseTopicName);
    }
}
