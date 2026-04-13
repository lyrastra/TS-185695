using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencyPaymentFromCustomer.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    [InjectAsSingleton]
    public class CurrencyPaymentFromCustomerCommandWriter : ICurrencyPaymentFromCustomerCommandWriter
    {
        private const string entityName = CurrencyPaymentFromCustomerConstants.EntityName;
        private static readonly string topic = CurrencyPaymentFromCustomerConstants.Command.Topic;

        private readonly IMoedeloEntityCommandKafkaTopicWriter writer;

        public CurrencyPaymentFromCustomerCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter writer)
        {
            this.writer = writer;
        }

        public Task WriteImportAsync(string key, string token, ImportCurrencyPaymentFromCustomer commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateCurrencyPaymentFromCustomer commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportWithMissingContractorAsync(string key, string token, ImportWithMissingContractorCurrencyPaymentFromCustomer commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportWithMissingExchangeRateAsync(string key, string token, ImportWithMissingExchangeRateCurrencyPaymentFromCustomer commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }
    }
}
