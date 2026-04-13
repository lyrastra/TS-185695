using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Common.Kafka.Abstractions.Entities.Events.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.PaymentOrderImport.Kafka.Abstractions;
using Moedelo.PaymentOrderImport.Kafka.Abstractions.Movements;
using Moedelo.PaymentOrderImport.Kafka.Abstractions.Movements.Events;
using System.Threading.Tasks;

namespace Moedelo.PaymentOrderImport.Kafka.Movements
{
    [InjectAsSingleton(typeof(IMovementDocumentEventWriter))]
    class MovementDocumentEventWriter : IMovementDocumentEventWriter
    {
        private readonly string TopicName = ImportTopics.Movement.Document.Event.Topic;
        private readonly string EntityName = ImportTopics.Movement.Document.EntityName;

        private readonly IMoedeloEntityEventKafkaTopicWriter moedeloEntityEventKafkaTopicWriter;
        private readonly MoedeloEntityEventKafkaMessageDefinitionBuilder definitionBuilder;

        public MovementDocumentEventWriter(
            IMoedeloEntityEventKafkaTopicWriter moedeloEntityEventKafkaTopicWriter)
        {
            this.moedeloEntityEventKafkaTopicWriter = moedeloEntityEventKafkaTopicWriter;

            definitionBuilder = MoedeloEntityEventKafkaMessageDefinitionBuilder.For(TopicName, EntityName);
        }

        public Task WriteDocumentImportCompletedAsync(DocumentImportCompleted eventData)
        {
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.SourceFileId, eventData);
            return moedeloEntityEventKafkaTopicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteDocumentImportFailedAsync(DocumentImportFailed eventData)
        {
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.SourceFileId, eventData);
            return moedeloEntityEventKafkaTopicWriter.WriteEventDataAsync(eventDefinition);
        }

        public Task WriteDocumentImportSkippedAsync(DocumentImportSkipped eventData)
        {
            var eventDefinition = definitionBuilder.CreateEventDefinition(eventData.SourceFileId, eventData);
            return moedeloEntityEventKafkaTopicWriter.WriteEventDataAsync(eventDefinition);
        }
    }
}