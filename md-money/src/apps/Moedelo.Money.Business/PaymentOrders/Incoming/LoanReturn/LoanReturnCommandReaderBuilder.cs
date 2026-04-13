using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.LoanReturn;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.LoanReturn.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.LoanReturn
{
    [InjectAsSingleton(typeof(ILoanReturnCommandReaderBuilder))]
    public class LoanReturnCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, ILoanReturnCommandReaderBuilder
    {
        public LoanReturnCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.LoanReturn.Command.Topic,
                  MoneyTopics.PaymentOrders.LoanReturn.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public ILoanReturnCommandReaderBuilder OnImport(Func<ImportLoanReturn, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ILoanReturnCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateLoanReturn, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ILoanReturnCommandReaderBuilder OnImportWithMissingContract(Func<ImportWithMissingContractLoanReturn, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ILoanReturnCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorLoanReturn, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}