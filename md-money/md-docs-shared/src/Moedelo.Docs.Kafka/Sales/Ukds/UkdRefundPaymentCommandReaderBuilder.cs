using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Docs.Kafka.Abstractions;
using Moedelo.Docs.Kafka.Abstractions.Sales.Ukds.Commands;
using Moedelo.Docs.Kafka.Abstractions.Topics;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Docs.Kafka.Sales.Ukds
{
    [InjectAsSingleton(typeof(IUkdRefundPaymentCommandReaderBuilder))]
    public class UkdRefundPaymentCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IUkdRefundPaymentCommandReaderBuilder
    {
        public UkdRefundPaymentCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                AccountingPrimaryDocumentsTopics.Sales.Ukds.UpdateRefundPaymentCommand,
                nameof(AccountingPrimaryDocumentsTopics.Sales.Ukds.UpdateRefundPaymentCommand),
                reader,
                replyWriter,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IUkdRefundPaymentCommandReaderBuilder OnUpdate(Func<UkdUpdateRefundPayment, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}