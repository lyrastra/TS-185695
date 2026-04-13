using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.PaymentOrders.Outgoing.LoanRepayment.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.LoanRepayment
{
    [InjectAsSingleton]
    public class LoanRepaymentCommandWriter : ILoanRepaymentCommandWriter
    {
        private const string entityName = LoanRepaymentConstants.EntityName;
        private static readonly string topic = LoanRepaymentConstants.Command.Topic;

        private readonly IMoedeloEntityCommandKafkaTopicWriter writer;

        public LoanRepaymentCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter writer)
        {
            this.writer = writer;
        }

        public Task WriteImportAsync(string key, string token, ImportLoanRepayment commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateLoanRepayment commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportWithMissingContractAsync(string key, string token, ImportWithMissingContractLoanRepayment commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportWithMissingContractorAsync(string key, string token, ImportWithMissingContractorLoanRepayment commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }
    }
}
