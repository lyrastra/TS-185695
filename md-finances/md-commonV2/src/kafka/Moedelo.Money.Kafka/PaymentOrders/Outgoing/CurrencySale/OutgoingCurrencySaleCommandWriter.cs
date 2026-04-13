using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencySale.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencySale
{
    [InjectAsSingleton]
    public class OutgoingCurrencySaleCommandWriter : IOutgoingCurrencySaleCommandWriter
    {
        private const string entityName = OutgoingCurrencySaleConstants.EntityName;
        private static readonly string topic = OutgoingCurrencySaleConstants.Command.Topic;

        private readonly IMoedeloEntityCommandKafkaTopicWriter writer;

        public OutgoingCurrencySaleCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter writer)
        {
            this.writer = writer;
        }

        public Task WriteImportAsync(string key, string token, ImportOutgoingCurrencySale commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateOutgoingCurrencySale commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportWithMissingExchangeRateAsync(string key, string token, ImportWithMissingMissingExchangeRateOutgoingCurrencySale commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }
    }
}
