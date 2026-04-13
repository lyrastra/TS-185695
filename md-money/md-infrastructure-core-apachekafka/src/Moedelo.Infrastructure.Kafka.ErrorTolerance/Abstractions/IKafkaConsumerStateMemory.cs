using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance.Abstractions;

public interface IKafkaConsumerStateMemory
{
    /// <summary>
    /// Консьюмеру была назначена секция топика для обработки
    /// </summary>
    /// <param name="partition">информация о назначенной секции</param>
    void PartitionAssigned(KafkaTopicPartition partition);

    /// <summary>
    /// У консьюмера была отозвана секция
    /// </summary>
    /// <param name="partitionOffset">информация об отозванной секции</param>
    void PartitionRevoked(KafkaTopicPartitionOffset partitionOffset);

    /// <summary>
    /// Сообщение получено из кафки и готовится к обработке
    /// </summary>
    /// <param name="messageOffset">смещение сообщения в секции топика</param>
    /// <param name="messageKey">ключ сообщения</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<ConsumingSideEffect> MessageIsConsumedAsync(KafkaTopicPartitionOffset messageOffset, string messageKey,
        CancellationToken cancellationToken);

    /// <summary>
    /// Было ли уже обработано сообщение в указанной позиции секции топика
    /// </summary>
    /// <param name="messageOffset">смещение сообщения в секции топика</param>
    /// <returns>true - сообщение обработано, false - иначе</returns>
    ValueTask<bool> IsMessageAlreadyProcessedAsync(KafkaTopicPartitionOffset messageOffset);

    /// <summary>
    /// Сохранить закоммиченное смещение.
    /// Все сообщения до этого смещения и включая его считаются обработанными.
    /// </summary>
    /// <param name="messageOffset">смещение в секции топика</param>
    /// <param name="messageKey">ключ сообщения, находящегося по данному смещениею</param>
    ValueTask SetCommittedOffsetAsync(KafkaTopicPartitionOffset messageOffset, string messageKey);

    /// <summary>
    /// Пометить сообщение как пропущенное
    /// </summary>
    /// <param name="messageOffset">смещение сообщения в секции топика</param>
    /// <param name="messageKey">ключ сообщения</param>
    ValueTask MarkMessageAsSkippedAsync(KafkaTopicPartitionOffset messageOffset, string messageKey);

    /// <summary>
    /// Пометить сообщение как обработанное
    /// </summary>
    /// <param name="messageOffset">смещение сообщения в секции топика</param>
    /// <param name="messageKey">ключ сообщения</param>
    ValueTask MarkMessageAsProcessedAsync(KafkaTopicPartitionOffset messageOffset, string messageKey);
    ValueTask<bool> HasSkippedMessagesBeforeAsync(KafkaTopicPartitionOffset messageOffset);
    ValueTask<bool> HasAnySkippedMessageWithKeyAsync(KafkaTopicPartition partition, string messageKey);
    ValueTask<PartitionMemoryState> GetPartitionStateAsync(KafkaTopicPartition partition);
}