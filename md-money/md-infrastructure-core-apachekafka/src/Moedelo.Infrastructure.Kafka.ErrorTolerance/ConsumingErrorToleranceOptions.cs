using System.ComponentModel.DataAnnotations;

namespace Moedelo.Infrastructure.Kafka.ErrorTolerance;

public sealed class ConsumingErrorToleranceOptions : IErrorToleratedKafkaConsumerOptions
{
    /// <summary>
    /// Максимальное количество уникальных ключей сообщений, обработка которых может быть поставлено на паузу.
    /// При превышении этого значения обработка останавливается полностью. 
    /// </summary>
    [Range(2, 128)]
    public int MaxPausedMessageKeys { get; set; } = 32;

    /// <summary>
    /// Максимальное смещение, на которое может быть продвинута обработка после закоммиченного смещения.
    /// Определяет максимальное количество сообщений, находящихся после закоммиченного смещения,
    /// для которых будет сделана попытка обработки.
    /// При превышении этого значения обработка останавливается полностью 
    /// </summary>
    [Range(128, 32000)]
    public int MaxOffsetMapDepth { get; set; } = 32000;

    /// <summary>
    /// Максимальный период времени, который может продолжаться обработка сообщений после последнего успешного коммита смещения в секции.
    /// При превышении этого значения обработка останавливается полностью.  
    /// </summary>
    public TimeSpan ErrorToleranceTimeSpan { get; set; } = TimeSpan.FromDays(2);

    public Type? KafkaConsumerMessageMemoryRepositoryType { get; set; }
}
