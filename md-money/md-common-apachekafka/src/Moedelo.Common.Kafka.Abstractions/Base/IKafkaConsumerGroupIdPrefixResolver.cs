namespace Moedelo.Common.Kafka.Abstractions.Base;

/// <summary>
/// Разрешатель префикса группы консьюмера кафки
/// </summary>
public interface IKafkaConsumerGroupIdPrefixResolver
{
    string GetGroupIdPrefix();
}
