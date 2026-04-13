using Moedelo.ExecutionContext.Client;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.InfrastructureV2.Json;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.LoanReturn.Events;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.LoanReturn
{
    [InjectAsSingleton]
    public class LoanReturnEventWriter : ILoanReturnEventWriter
    {
        private readonly ITokenApiClient tokenApiClient;
        private readonly IMoedeloKafkaTopicWriter writer;

        public LoanReturnEventWriter(
            ITokenApiClient tokenApiClient,
            IMoedeloKafkaTopicWriter writer)
        {
            this.tokenApiClient = tokenApiClient;
            this.writer = writer;
        }

        public async Task WriteAsync(int firmId, int userId, LoanReturnUpdatedMessage message)
        {
            var topic = LoanReturnConstants.Event.Topic;
            var token = await tokenApiClient.GetFromUserContextAsync(firmId, userId).ConfigureAwait(false);
            var key = firmId.ToString();
            var value = new CUDEventMessageValue
            {
                Token = token,
                EventType = CUDEventType.Updated,
                EventDataJson = message.ToJsonString()
            };
            await writer.WriteAsync(topic, key, value).ConfigureAwait(false);
        }
    }
}
