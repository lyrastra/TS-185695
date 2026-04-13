using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.PaymentImport.Kafka.File.Events;
using System.Threading.Tasks;

namespace Moedelo.PaymentImport.Kafka.File
{
    [InjectAsSingleton]
    public class FileImportEventWriter : IFileImportEventWriter
    {
        private const string entityName = FileImportConstants.EntityName;
        private static readonly string topic = FileImportConstants.Event.Topic;

        private readonly IMoedeloEntityEventKafkaTopicWriter writer;

        public FileImportEventWriter(
            IMoedeloEntityEventKafkaTopicWriter writer)
        {
            this.writer = writer;
        }

        public Task WriteFileImportCompletedAsync(string key, string token, FileImportCompleted eventData)
        {
            return writer.WriteEventDataAsync(topic, key, entityName, eventData, token);
        }

        public Task WriteFileParsingCompletedAsync(string key, string token, FileParsingCompleted eventData)
        {
            return writer.WriteEventDataAsync(topic, key, entityName, eventData, token);
        }

        public Task WriteFileParsingFaiedAsync(string key, string token, FileParsingFailed eventData)
        {
            return writer.WriteEventDataAsync(topic, key, entityName, eventData, token);
        }
    }
}
