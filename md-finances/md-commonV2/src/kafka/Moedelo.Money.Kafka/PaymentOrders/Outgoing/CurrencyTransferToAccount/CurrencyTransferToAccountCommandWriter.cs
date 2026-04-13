using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyTransferToAccount.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyTransferToAccount
{
    [InjectAsSingleton]
    public class CurrencyTransferToAccountCommandWriter : ICurrencyTransferToAccountCommandWriter
    {
        private const string entityName = CurrencyTransferToAccountConstants.EntityName;
        private static readonly string topic = CurrencyTransferToAccountConstants.Command.Topic;

        private readonly IMoedeloEntityCommandKafkaTopicWriter writer;

        public CurrencyTransferToAccountCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter writer)
        {
            this.writer = writer;
        }

        public Task WriteImportAsync(string key, string token, ImportCurrencyTransferToAccount commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateCurrencyTransferToAccount commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportWithMissingCurrencySettlementAccountAsync(string key, string token, ImportWithMissingCurrencySettlementAccountCurrencyTransferToAccount commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }
    }
}
