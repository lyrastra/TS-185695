using System;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Models
{
    public class KafkaMessage<T>
        where T : KafkaMessageValueBase
    {
        public string TopicName { get; }

        public string Key { get; }

        public T Value { get; }

        public KafkaMessage(string topicName, string key, T value)
        {
            if (string.IsNullOrWhiteSpace(topicName))
            {
                throw new ArgumentException("Не задано имя целевого топика для отправки сообщения", nameof(topicName));
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException(
                    "Значение ключа сообщения требуется заполнить. Оно будет использовано для выбора секции топика, в которую будет записано сообщение",
                    nameof(key));
            }

            TopicName = topicName;
            Key = key;
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}