using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.ContributionOfOwnFunds.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.ContributionOfOwnFunds
{
    [InjectAsSingleton(typeof(IContributionOfOwnFundsCommandReaderBuilder))]
    public class ContributionOfOwnFundsCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IContributionOfOwnFundsCommandReaderBuilder
    {
        public ContributionOfOwnFundsCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.ContributionOfOwnFunds.Command.Topic,
                  MoneyTopics.PaymentOrders.ContributionOfOwnFunds.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IContributionOfOwnFundsCommandReaderBuilder OnImport(Func<ImportContributionOfOwnFunds, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IContributionOfOwnFundsCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateContributionOfOwnFunds, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IContributionOfOwnFundsCommandReaderBuilder OnImportAmbiguousOperationType(Func<ImportAmbiguousOperationTypeContributionOfOwnFunds, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}