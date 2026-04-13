using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;

public interface IConsumingStateMemory
{
    /// <summary>
    /// Группа консьюмера
    /// </summary>
    string ConsumerGroupId { get; }
    /// <summary>
    /// Консьюмеру назначена секция топика
    /// </summary>
    /// <param name="state">сохранённое состояние обработки назначенной секции</param>
    void Assigned(IPartitionConsumingReadOnlyState state);
    /// <summary>
    /// У консьюмера отозвана секция топика
    /// </summary>
    /// <param name="offset">информация об отозванной секции</param>
    void Revoked(KafkaTopicPartitionOffset offset);
    /// <summary>
    /// Сохранить закоммиченное смещение
    /// </summary>
    /// <param name="partitionOffset">смещение в секции топика</param>
    /// <param name="messageKey">ключ сообщение по данному смещению</param>
    /// <returns>итоговое состояние секции топика после выполнения операции</returns>
    IPartitionConsumingReadOnlyState CommitOffset(KafkaTopicPartitionOffset partitionOffset, string messageKey);
    /// <summary>
    /// Сместить закоммиченное смещение в секции топика на указанную позицию.
    /// ВНИМАНИЕ: допустимо только если в рамках текущей сессии не было пропущено ни одного сообщения
    /// </summary>
    /// <param name="partitionOffset">смещенеие в секции топика</param>
    /// <returns>итоговое состояние секции топика после выполнения операции</returns>
    IPartitionConsumingReadOnlyState MoveCommittedOffsetTo(KafkaTopicPartitionOffset partitionOffset);
    IPartitionConsumingReadOnlyState ProcessMessage(KafkaTopicPartitionOffset offset, string messageKey);
    IPartitionConsumingReadOnlyState SkipMessage(KafkaTopicPartitionOffset offset, string messageKey);
    IPartitionConsumingReadOnlyState GetPartitionState(KafkaTopicPartition partition);
    int CountUniqueSkippedMessageKeys(KafkaTopicPartition partition);
    bool IsMessageAlreadyProcessed(KafkaTopicPartitionOffset offset);
    /// <summary>
    /// Есть ли хоть одно сообщение, обработка которого пропущена
    /// после закоммиченного смещения
    /// </summary>
    /// <param name="partition">секция топика</param>
    /// <returns>true - есть пропущенные сообщения, false - нет таких сообщений</returns>
    bool HasNonEmptyOffsetMapMessage(KafkaTopicPartition partition);
    bool HasAnySkippedMessageBefore(KafkaTopicPartitionOffset offset);
    bool HasAnySkippedMessageWithKey(KafkaTopicPartition offset, string messageKey);
    PartitionConsumingSessionState MessageIsConsumed(KafkaTopicPartitionOffset offset, string messageKey);
}
