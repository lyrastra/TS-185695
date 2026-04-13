using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.TransferToAccount.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.TransferToAccount
{
    [InjectAsSingleton]
    public class TransferToAccountCommandWriter : ITransferToAccountCommandWriter
    {
        private const string entityName = TransferToAccountConstants.EntityName;
        private static readonly string topic = TransferToAccountConstants.Command.Topic;

        private readonly IMoedeloEntityCommandKafkaTopicWriter writer;

        public TransferToAccountCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter writer)
        {
            this.writer = writer;
        }

        public Task WriteImportAsync(string key, string token, ImportTransferToAccount commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateTransferToAccount commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }
    }
}
