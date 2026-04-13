using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentCommandReaderBuilder))]
    public class UnifiedBudgetaryPaymentCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IUnifiedBudgetaryPaymentCommandReaderBuilder
    {
        public UnifiedBudgetaryPaymentCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.UnifiedBudgetaryPayment.Command.Topic,
                  MoneyTopics.PaymentOrders.UnifiedBudgetaryPayment.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
            ContextInitializer = contextInitializer;
        }

        public IExecutionInfoContextInitializer ContextInitializer { get; }

        public IUnifiedBudgetaryPaymentCommandReaderBuilder OnImport(Func<ImportUnifiedBudgetaryPayment, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IUnifiedBudgetaryPaymentCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateUnifiedBudgetaryPayment, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}