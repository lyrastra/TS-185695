using System;
using Moedelo.InfrastructureV2.Domain.Models.ApacheKafka;

namespace Moedelo.InfrastructureV2.ApacheKafka.Abstractions
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
                throw new ArgumentException(nameof(topicName));
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException(nameof(key));
            }

            if (value == null)
            {
                throw new ArgumentException(nameof(value));
            }

            TopicName = topicName;
            Key = key;
            Value = value;
        }
    }
}