using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyPurchase.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyPurchase
{
    [InjectAsSingleton]
    public class OutgoingCurrencyPurchaseCommandWriter : IOutgoingCurrencyPurchaseCommandWriter
    {
        private const string entityName = OutgoingCurrencyPurchaseConstants.EntityName;
        private static readonly string topic = OutgoingCurrencyPurchaseConstants.Command.Topic;

        private readonly IMoedeloEntityCommandKafkaTopicWriter writer;

        public OutgoingCurrencyPurchaseCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter writer)
        {
            this.writer = writer;
        }

        public Task WriteImportAsync(string key, string token, ImportOutgoingCurrencyPurchase commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateOutgoingCurrencyPurchase commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportWithMissingCurrencySettlementAccountAsync(string key, string token, ImportWithMissingCurrencySettlementAccountOutgoingCurrencyPurchase commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportWithMissingExchangeRateAsync(string key, string token, ImportWithMissingMissingExchangeRateOutgoingCurrencyPurchase commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }
    }
}
