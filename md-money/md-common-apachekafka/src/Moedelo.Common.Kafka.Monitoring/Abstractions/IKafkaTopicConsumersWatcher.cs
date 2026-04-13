namespace Moedelo.Common.Kafka.Monitoring.Abstractions;

/// <summary>
/// Интерфейс нужен только для того, чтобы внедрить его как зависимость.
/// Вся работа делается в реализации в конструкторе и деструкторе
/// Чтобы не делать публичным класс реализации, был вынесен пустой публичный интерфейс
/// </summary>
public interface IKafkaTopicConsumersWatcher;
