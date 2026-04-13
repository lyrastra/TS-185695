using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;
using Moedelo.Money.Kafka.PaymentOrders.Incoming.ContributionToAuthorizedCapital.Commands;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.ContributionToAuthorizedCapital
{
    [InjectAsSingleton]
    public class ContributionToAuthorizedCapitalCommandWriter : IContributionToAuthorizedCapitalCommandWriter
    {
        private const string entityName = ContributionToAuthorizedCapitalConstants.EntityName;
        private static readonly string topic = ContributionToAuthorizedCapitalConstants.Command.Topic;

        private readonly IMoedeloEntityCommandKafkaTopicWriter writer;

        public ContributionToAuthorizedCapitalCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter writer)
        {
            this.writer = writer;
        }

        public Task WriteImportAsync(string key, string token, ImportContributionToAuthorizedCapital commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportDuplicateAsync(string key, string token, ImportDuplicateContributionToAuthorizedCapital commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }

        public Task WriteImportWithMissingContractorAsync(string key, string token, ImportWithMissingContractorContributionToAuthorizedCapital commandData)
        {
            return writer.WriteCommandDataAsync(topic, key, entityName, commandData, token);
        }
    }
}
