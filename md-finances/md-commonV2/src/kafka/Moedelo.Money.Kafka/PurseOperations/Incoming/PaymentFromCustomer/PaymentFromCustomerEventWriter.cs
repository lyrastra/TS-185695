using Moedelo.ExecutionContext.Client;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.PurseOperations.Incoming.PaymentFromCustomer.Events;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PurseOperations.Incoming.PaymentFromCustomer
{
    [InjectAsSingleton]
    public class PaymentFromCustomerEventWriter : IPaymentFromCustomerEventWriter
    {
        // todo: Вниминие! Очереди не созданы.
        private const string entityName = PaymentFromCustomerConstants.EntityName;
        private static readonly string topic = PaymentFromCustomerConstants.Event.Topic;
        
        private readonly ITokenApiClient tokenApiClient;
        private readonly IMoedeloEntityEventKafkaTopicWriter writer;

        public PaymentFromCustomerEventWriter(
            ITokenApiClient tokenApiClient,
            IMoedeloEntityEventKafkaTopicWriter writer)
        {
            this.tokenApiClient = tokenApiClient;
            this.writer = writer;
        }

        public async Task WritePaymentFromCustomerCreatedAsync(int firmId, int userId, PaymentFromCustomerCreated eventData)
        {
            var key = eventData.DocumentBaseId.ToString();
            var token = await tokenApiClient.GetFromUserContextAsync(firmId, userId).ConfigureAwait(false);
            await writer.WriteEventDataAsync(topic, key, entityName, eventData, token).ConfigureAwait(false);
        }

        public async Task WritePaymentFromCustomerUpdatedAsync(int firmId, int userId, PaymentFromCustomerUpdated eventData)
        {
            var key = eventData.DocumentBaseId.ToString();
            var token = await tokenApiClient.GetFromUserContextAsync(firmId, userId).ConfigureAwait(false);
            await writer.WriteEventDataAsync(topic, key, entityName, eventData, token).ConfigureAwait(false);
        }

        public async Task WritePaymentFromCustomerDeletedAsync(int firmId, int userId, PaymentFromCustomerDeleted eventData)
        {
            var key = eventData.DocumentBaseId.ToString();
            var token = await tokenApiClient.GetFromUserContextAsync(firmId, userId).ConfigureAwait(false);
            await writer.WriteEventDataAsync(topic, key, entityName, eventData, token).ConfigureAwait(false);
        }
    }
}
