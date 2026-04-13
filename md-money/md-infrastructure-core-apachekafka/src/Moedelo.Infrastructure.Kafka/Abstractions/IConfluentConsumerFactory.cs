using Confluent.Kafka;

namespace Moedelo.Infrastructure.Kafka.Abstractions;

/// <summary>
/// Фабрика для создания экземпляров IConsumer
/// NOTE: приходится делать этот интерфейс публичным, чтобы генерировать мок для него в юнит-тестах
/// иначе Mock почему-то не может создать мок-объект
/// </summary>
public interface IConfluentConsumerFactory
{
    IConsumer<string, string> Create(ConsumerConfig consumerConfig, ConfluentConsumerEventHandlers handlers);
}