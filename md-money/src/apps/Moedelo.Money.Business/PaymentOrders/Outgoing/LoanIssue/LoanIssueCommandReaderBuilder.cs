using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanIssue;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanIssue.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanIssue
{
    [InjectAsSingleton(typeof(ILoanIssueCommandReaderBuilder))]
    public class LoanIssueCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, ILoanIssueCommandReaderBuilder
    {
        public LoanIssueCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.LoanIssue.Command.Topic,
                  MoneyTopics.PaymentOrders.LoanIssue.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public ILoanIssueCommandReaderBuilder OnImport(Func<ImportLoanIssue, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ILoanIssueCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateLoanIssue, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ILoanIssueCommandReaderBuilder OnImportWithMissingContract(Func<ImportWithMissingContractLoanIssue, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ILoanIssueCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorLoanIssue, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}