using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyPaymentToSupplier.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    [InjectAsSingleton]
    public class CurrencyPaymentToSupplierCommandWriter : ICurrencyPaymentToSupplierCommandWriter
    {
        private const string entityName = CurrencyPaymentToSupplierConstants.EntityName;
        private static readonly string topic = CurrencyPaymentToSupplierConstants.Command.Topic;

        private readonly IMoedeloEntityCommandKafkaTopicWriter writer;

        public CurrencyPaymentToSupplierCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter writer)
        {
            this.writer = writer;
        }

        public Task WriteImportAsync(string key, string token, ImportCurrencyPaymentToSupplier commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateCurrencyPaymentToSupplier commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportWithMissingContractorAsync(string key, string token, ImportWithMissingMissingContractorCurrencyPaymentToSupplier commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportWithMissingExchangeRateAsync(string key, string token, ImportWithMissingMissingExchangeRateCurrencyPaymentToSupplier commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }
    }
}
