using System;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Models;

/// <summary>
/// Идентификатор группы консьюмера
/// </summary>
public sealed class KafkaConsumerGroupId
{
    public KafkaConsumerGroupId(string prefix, string groupId)
    {
        if (string.IsNullOrWhiteSpace(prefix))
        {
            throw new ArgumentException(nameof(prefix));
        }
        
        if (string.IsNullOrWhiteSpace(groupId))
        {
            throw new ArgumentException(nameof(groupId));
        }

        this.Prefix = prefix;
        this.Raw = groupId;
        this.IdInKafka = $"{Prefix}.{Raw}";
    }

    /// <summary>
    /// Префикс, используемый в текущем окружении для формирования полного идентификатора
    /// </summary>
    public string Prefix { get; }
    /// <summary>
    /// "Сырой" идентификатор без префикса
    /// ВНИМАНИЕ: для идентификации в Kafka должен использоваться <see cref="IdInKafka"/>
    /// </summary>
    public string Raw { get; }
    /// <summary>
    /// Полный идентификатор группы (включая префикс) для идентификации в Kafka
    /// </summary>
    public string IdInKafka { get; }
}
