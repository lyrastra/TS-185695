using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.PaymentOrders.Import.Events;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Import
{
    [InjectAsSingleton]
    public class PaymentOrderImportEventWriter : IPaymentOrderImportEventWriter
    {
        private const string entityName = PaymentOrderImportConstants.EntityName;
        private static readonly string topic = PaymentOrderImportConstants.Event.Topic;

        private readonly IMoedeloEntityEventKafkaTopicWriter writer;

        public PaymentOrderImportEventWriter(
            IMoedeloEntityEventKafkaTopicWriter writer)
        {
            this.writer = writer;
        }

        public Task WriteImportFaiedAsync(string key, string token, ImportFailed eventData)
        {
            return writer.WriteEventDataAsync(topic, key, entityName, eventData, token);
        }
    }
}
