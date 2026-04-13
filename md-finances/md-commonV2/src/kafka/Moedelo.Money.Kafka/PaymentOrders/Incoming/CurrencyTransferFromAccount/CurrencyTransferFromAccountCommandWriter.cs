using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencyTransferFromAccount.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    [InjectAsSingleton]
    public class CurrencyTransferFromAccountCommandWriter : ICurrencyTransferFromAccountCommandWriter
    {
        private const string entityName = CurrencyTransferFromAccountConstants.EntityName;
        private static readonly string topic = CurrencyTransferFromAccountConstants.Command.Topic;

        private readonly IMoedeloEntityCommandKafkaTopicWriter writer;

        public CurrencyTransferFromAccountCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter writer)
        {
            this.writer = writer;
        }

        public Task WriteImportAsync(string key, string token, ImportCurrencyTransferFromAccount commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);

        }

        public Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateCurrencyTransferFromAccount commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);

        }

        public Task WriteImportWithMissingCurrencySettlementAccountAsync(string key, string token, ImportWithMissingCurrencySettlementAccountCurrencyTransferFromAccount commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);

        }
    }
}
