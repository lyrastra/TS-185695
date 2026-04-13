using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencySale.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencySale
{
    [InjectAsSingleton]
    public class IncomingCurrencySaleCommandWriter : IIncomingCurrencySaleCommandWriter
    {
        private const string entityName = IncomingCurrencySaleConstants.EntityName;
        private static readonly string topic = IncomingCurrencySaleConstants.Command.Topic;

        private readonly IMoedeloEntityCommandKafkaTopicWriter writer;

        public IncomingCurrencySaleCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter writer)
        {
            this.writer = writer;
        }

        public Task WriteImportAsync(string key, string token, ImportIncomingCurrencySale commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);

        }

        public Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateIncomingCurrencySale commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);

        }

        public Task WriteImportWithMissingCurrencySettlementAccountAsync(string key, string token, ImportWithMissingCurrencySettlementAccountIncomingCurrencySale commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);

        }
    }
}
