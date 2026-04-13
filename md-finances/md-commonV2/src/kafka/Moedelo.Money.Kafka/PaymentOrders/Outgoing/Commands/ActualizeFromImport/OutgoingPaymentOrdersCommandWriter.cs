using Moedelo.ExecutionContext.Client;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.BankFee;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.Commands.ActualizeFromImport
{
    [InjectAsSingleton]
    public class OutgoingPaymentOrdersCommandWriter : IOutgoingPaymentOrdersCommandWriter
    {
        private const string entityName = OutgoingPaymentOrdersConstants.EntityName;
        private static readonly string topic = OutgoingPaymentOrdersConstants.Command.Topic;

        private readonly ITokenApiClient tokenApiClient;
        private readonly IMoedeloEntityCommandKafkaTopicWriter writer;

        public OutgoingPaymentOrdersCommandWriter(
            ITokenApiClient tokenApiClient,
            IMoedeloEntityCommandKafkaTopicWriter writer)
        {
            this.tokenApiClient = tokenApiClient;
            this.writer = writer;
        }

        public async Task WriteActualizeAsync(int firmId, int userId, ActualizeFromImport commandData)
        {
            var key = firmId.ToString();
            var token = await tokenApiClient.GetFromUserContextAsync(firmId, userId).ConfigureAwait(false);
            await writer.WriteCommandDataAsync(topic, key, entityName, commandData, token).ConfigureAwait(false);
        }
    }
}
