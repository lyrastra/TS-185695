using Moedelo.ExecutionContext.Client;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.InfrastructureV2.Json;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.LoanIssue.Events;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.LoanIssue
{
    [InjectAsSingleton]
    public class LoanIssueEventWriter : ILoanIssueEventWriter
    {
        private readonly ITokenApiClient tokenApiClient;
        private readonly IMoedeloKafkaTopicWriter writer;

        public LoanIssueEventWriter(
            ITokenApiClient tokenApiClient,
            IMoedeloKafkaTopicWriter writer)
        {
            this.tokenApiClient = tokenApiClient;
            this.writer = writer;
        }

        public async Task WriteAsync(int firmId, int userId, LoanIssueUpdatedMessage message)
        {
            var token = await tokenApiClient.GetFromUserContextAsync(firmId, userId).ConfigureAwait(false);
            var key = firmId.ToString();
            var value = new CUDEventMessageValue
            {
                Token = token,
                EventType = CUDEventType.Updated,
                EventDataJson = message.ToJsonString()
            };
            var topic = LoanIssueConstants.Event.Topic;
            await writer.WriteAsync(topic, key, value).ConfigureAwait(false);
        }
    }
}
