using System;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Models;

/// <summary>
/// Информация о названии топика
/// </summary>
public class KafkaTopicName
{
    public KafkaTopicName(string rawName, string nameInKafka)
    {
        if (string.IsNullOrWhiteSpace(rawName))
        {
            throw new ArgumentException(nameof(rawName));
        }
        if (string.IsNullOrWhiteSpace(nameInKafka))
        {
            throw new ArgumentException(nameof(nameInKafka));
        }

        Raw = rawName;
        NameInKafka = nameInKafka;
    }

    /// <summary>
    /// "Сырое" (логическое) название топика. Без учёта особенностей окружения
    /// ВНИМАНИЕ: может не совпадать с реальным названием топика в кафке (<see cref="NameInKafka"/> 
    /// </summary>
    public string Raw { get; }
    /// <summary>
    /// Название топика в кафке
    /// </summary>
    public string NameInKafka { get; }
}
