using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.LoanObtaining;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.LoanObtaining.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.LoanObtaining
{
    [InjectAsSingleton(typeof(ILoanObtainingCommandReaderBuilder))]
    public class LoanObtainingCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, ILoanObtainingCommandReaderBuilder
    {
        public LoanObtainingCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.LoanObtaining.Command.Topic,
                  MoneyTopics.PaymentOrders.LoanObtaining.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public ILoanObtainingCommandReaderBuilder OnImport(Func<ImportLoanObtaining, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ILoanObtainingCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateLoanObtaining, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ILoanObtainingCommandReaderBuilder OnImportWithMissingContract(Func<ImportWithMissingContractLoanObtaining, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ILoanObtainingCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorLoanObtaining, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}