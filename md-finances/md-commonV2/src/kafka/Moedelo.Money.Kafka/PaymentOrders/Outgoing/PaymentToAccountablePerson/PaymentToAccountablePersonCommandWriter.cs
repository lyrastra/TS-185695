using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.PaymentToAccountablePerson.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    [InjectAsSingleton]
    public class PaymentToAccountablePersonCommandWriter : IPaymentToAccountablePersonCommandWriter
    {
        private const string entityName = PaymentToAccountablePersonConstants.EntityName;
        private static readonly string topic = PaymentToAccountablePersonConstants.Command.Topic;

        private readonly IMoedeloEntityCommandKafkaTopicWriter writer;

        public PaymentToAccountablePersonCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter writer)
        {
            this.writer = writer;
        }

        public Task WriteImportAsync(string key, string token, ImportPaymentToAccountablePerson commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportDuplicateAsync(string key, string token, ImportDuplicatePaymentToAccountablePerson commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportWithMissingEmployeeAsync(string key, string token, ImportWithMissingEmployeePaymentToAccountablePerson commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }
    }
}
