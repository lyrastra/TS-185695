using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.RefundToCustomer.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.RefundToCustomer
{
    [InjectAsSingleton]
    public class RefundToCustomerCommandWriter : IRefundToCustomerCommandWriter
    {
        private const string entityName = RefundToCustomerConstants.EntityName;
        private static readonly string topic = RefundToCustomerConstants.Command.Topic;

        private readonly IMoedeloEntityCommandKafkaTopicWriter writer;

        public RefundToCustomerCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter writer)
        {
            this.writer = writer;
        }

        public Task WriteImportAsync(string key, string token, ImportRefundToCustomer commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateRefundToCustomer commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportWithMissingContractorAsync(string key, string token, ImportWithMissingContractorRefundToCustomer commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }
    }
}
