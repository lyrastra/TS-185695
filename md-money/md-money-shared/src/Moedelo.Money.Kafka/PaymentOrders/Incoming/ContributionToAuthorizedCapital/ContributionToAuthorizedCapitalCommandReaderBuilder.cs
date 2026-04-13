using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionToAuthorizedCapital.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.ContributionToAuthorizedCapital
{
    [InjectAsSingleton(typeof(IContributionToAuthorizedCapitalCommandReaderBuilder))]
    public class ContributionToAuthorizedCapitalCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IContributionToAuthorizedCapitalCommandReaderBuilder
    {
        public ContributionToAuthorizedCapitalCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.ContributionToAuthorizedCapital.Command.Topic,
                  MoneyTopics.PaymentOrders.ContributionToAuthorizedCapital.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IContributionToAuthorizedCapitalCommandReaderBuilder OnImport(Func<ImportContributionToAuthorizedCapital, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IContributionToAuthorizedCapitalCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateContributionToAuthorizedCapital, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IContributionToAuthorizedCapitalCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorContributionToAuthorizedCapital, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}