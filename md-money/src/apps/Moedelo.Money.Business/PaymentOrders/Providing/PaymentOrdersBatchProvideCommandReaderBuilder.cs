using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Providing;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Providing.Commands;

namespace Moedelo.Money.Business.PaymentOrders.Providing
{
    [InjectAsSingleton(typeof(IPaymentOrdersBatchProvideCommandReaderBuilder))]
    internal sealed class PaymentOrdersBatchProvideCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IPaymentOrdersBatchProvideCommandReaderBuilder
    {
        public PaymentOrdersBatchProvideCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer,
            ILogger<PaymentOrdersBatchProvideCommandReaderBuilder> logger)
            : base(
                  Kafka.Abstractions.Topics.MoneyTopics.PaymentOrders.BatchProvide.Command.Topic,
                  Kafka.Abstractions.Topics.MoneyTopics.PaymentOrders.BatchProvide.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer,
                  logger)
        {
        }

        public IPaymentOrdersBatchProvideCommandReaderBuilder OnProvide(Func<PaymentOrdersBatchProvideCommand, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}