using System.Threading.Tasks;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.Topics;
using Moedelo.Money.Registry.Business.Abstractions;
using Moedelo.Money.Registry.Business.Mappers;
using Moedelo.Money.Registry.Domain.Models;

namespace Moedelo.Money.Registry.Business
{
    [InjectAsSingleton(typeof(IRegistryOperationEventWriter))]
    internal sealed class RegistryOperationEventWriter : MoedeloKafkaTopicWriterBase, IRegistryOperationEventWriter
    {
        private readonly string Topic = MoneyTopics.Registry.Operation.Event.Topic;
        private readonly string EntityName = MoneyTopics.Registry.Operation.EntityName;

        private readonly IMoedeloEntityEventKafkaTopicWriter topicWriter;
        private readonly MoedeloEntityEventKafkaMessageDefinitionBuilder definitionBuilder;

        public RegistryOperationEventWriter(
            IMoedeloEntityEventKafkaTopicWriter topicWriter,
            IMoedeloKafkaTopicWriterBaseDependencies dependencies)
            : base(dependencies)
        {
            this.topicWriter = topicWriter;

            definitionBuilder = MoedeloEntityEventKafkaMessageDefinitionBuilder.For(Topic, EntityName);
        }

        public Task WriteCreatedEventAsync(CreateMoneyOperationCommand command)
        {
            var eventData = RegistryMapper.MapToCreatedEvent(command);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteUpdatedEventAsync(UpdateMoneyOperationCommand command)
        {
            var eventData = RegistryMapper.MapToUpdatedEvent(command);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteDeletedEventAsync(DeleteMoneyOperationCommand command)
        {
            var eventData = RegistryMapper.MapToDeletedEvent(command);
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.DocumentBaseId.ToString(), eventData);
            return topicWriter.WriteEventDataAsync(eventDefinition);
        }
    }
}
