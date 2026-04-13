using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BudgetaryPayment
{
    [InjectAsSingleton(typeof(IBudgetaryPaymentCommandReaderBuilder))]
    public class BudgetaryPaymentCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IBudgetaryPaymentCommandReaderBuilder
    {
        public BudgetaryPaymentCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.BudgetaryPayment.Command.Topic,
                  MoneyTopics.PaymentOrders.BudgetaryPayment.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
            ContextInitializer = contextInitializer;
        }

        public IExecutionInfoContextInitializer ContextInitializer { get; }

        public IBudgetaryPaymentCommandReaderBuilder OnImport(Func<ImportBudgetaryPayment, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IBudgetaryPaymentCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateBudgetaryPayment, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}