using System;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Models
{
    /// <summary>
    /// Настройки консьюмера apache kafka
    /// </summary>
    public sealed class KafkaConsumerConfig
    {
        public KafkaConsumerConfig(
            string brokerEndpoints,
            KafkaConsumerGroupId groupId, 
            KafkaTopicName topicName)
        {
            if (string.IsNullOrWhiteSpace(brokerEndpoints))
            {
                throw new ArgumentException(nameof(brokerEndpoints));
            }

            BrokerEndpoints = brokerEndpoints;
            GroupId = groupId;
            TopicName = topicName;
        }
        
        public string BrokerEndpoints { get; }

        /// <summary>
        /// Идентификатор группы консьюмера
        /// </summary>
        public KafkaConsumerGroupId GroupId { get; }

        /// <summary>
        /// Название топика
        /// </summary>
        public KafkaTopicName TopicName { get; }

        public AutoOffsetResetType ResetType { get; init; } = AutoOffsetResetType.Latest;

        public KafkaConsumerConfigOptionalParams OptionalParams { get; init; } = new();

        public int? MaxCountOfIgnoringConsumeExceptionsInRow { get; init; } = null;

        public ExtraOptions ExtraOptions { get; init; } = new();

        public enum AutoOffsetResetType
        {
            Latest = 0,
            Earliest = 1,
        }
    }
}