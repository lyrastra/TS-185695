using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencyPurchase.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencyPurchase
{
    [InjectAsSingleton]
    public class IncomingCurrencyPurchaseCommandWriter : IIncomingCurrencyPurchaseCommandWriter
    {
        private const string entityName = IncomingCurrencyPurchaseConstants.EntityName;
        private static readonly string topic = IncomingCurrencyPurchaseConstants.Command.Topic;

        private readonly IMoedeloEntityCommandKafkaTopicWriter writer;

        public IncomingCurrencyPurchaseCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter writer)
        {
            this.writer = writer;
        }

        public Task WriteImportAsync(string key, string token, ImportIncomingCurrencyPurchase commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateIncomingCurrencyPurchase commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }
    }
}
