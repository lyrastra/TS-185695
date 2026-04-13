using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.LoanRepayment;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.LoanRepayment.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.LoanRepayment
{
    [InjectAsSingleton(typeof(ILoanRepaymentCommandReaderBuilder))]
    public class LoanRepaymentCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, ILoanRepaymentCommandReaderBuilder
    {
        public LoanRepaymentCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.LoanRepayment.Command.Topic,
                  MoneyTopics.PaymentOrders.LoanRepayment.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public ILoanRepaymentCommandReaderBuilder OnImport(Func<ImportLoanRepayment, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ILoanRepaymentCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateLoanRepayment, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ILoanRepaymentCommandReaderBuilder OnImportWithMissingContract(Func<ImportWithMissingContractLoanRepayment, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ILoanRepaymentCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorLoanRepayment, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}