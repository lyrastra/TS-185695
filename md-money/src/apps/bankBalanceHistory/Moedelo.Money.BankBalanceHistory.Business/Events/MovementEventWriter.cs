using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.BankBalanceHistory.Business.Abstractions.Events;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.BankBalanceHistory.Business.Events
{
    [InjectAsSingleton(typeof(IMovementEventWriter))]
    internal sealed class MovementEventWriter : MoedeloKafkaTopicWriterBase, IMovementEventWriter
    {
        private readonly string Topic = MoneyTopics.BankBalanceHistory.Movement.Event.Topic;
        private readonly string EntityName = MoneyTopics.BankBalanceHistory.Movement.EntityName;

        private readonly IMoedeloEntityEventKafkaTopicWriter topicWriter;
        private readonly MoedeloEntityEventKafkaMessageDefinitionBuilder definitionBuilder;

        public MovementEventWriter(
            IMoedeloEntityEventKafkaTopicWriter topicWriter,
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
            this.topicWriter = topicWriter;
            definitionBuilder = MoedeloEntityEventKafkaMessageDefinitionBuilder.For(Topic, EntityName);
        }

        public Task WriteProcessedEventAsync(MovementProcessedEvent processedEvent)
        {
            var eventData = MovementMapper.MapToMovementProcessed(processedEvent);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.FirmId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }
    }
}
