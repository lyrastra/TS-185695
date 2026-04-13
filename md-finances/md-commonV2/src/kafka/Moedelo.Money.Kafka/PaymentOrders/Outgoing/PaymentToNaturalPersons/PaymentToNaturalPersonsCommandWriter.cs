using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.PaymentToNaturalPersons.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [InjectAsSingleton]
    public class PaymentToNaturalPersonsCommandWriter : IPaymentToNaturalPersonsCommandWriter
    {
        private const string entityName = PaymentToNaturalPersonsConstants.EntityName;
        private static readonly string topic = PaymentToNaturalPersonsConstants.Command.Topic;

        private readonly IMoedeloEntityCommandKafkaTopicWriter writer;

        public PaymentToNaturalPersonsCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter writer)
        {
            this.writer = writer;
        }

        public Task WriteImportAsync(string key, string token, ImportPaymentToNaturalPersons commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportDuplicateAsync(string key, string token, ImportDuplicatePaymentToNaturalPersons commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportWithMissingEmployeeAsync(string key, string token, ImportWithMissingEmployeePaymentToNaturalPersons commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }
    }
}
