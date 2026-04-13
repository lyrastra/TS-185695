using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Commands;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Commands.ActualizeFromImport;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Commands.ChangeIsPaidFromIntegration;
using System.Threading.Tasks;
using System;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Commands
{
    [InjectAsSingleton(typeof(IPaymentOrderOutgoingCommandReaderBuilder))]
    internal sealed class PaymentOrderOutgoingCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IPaymentOrderOutgoingCommandReaderBuilder
    {
        public PaymentOrderOutgoingCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  Kafka.Abstractions.Topics.MoneyTopics.PaymentOrders.Outgoing.Commands.Topic,
                  Kafka.Abstractions.Topics.MoneyTopics.PaymentOrders.Outgoing.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IPaymentOrderOutgoingCommandReaderBuilder OnActualize(Func<ActualizeFromImport, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
        
        public IPaymentOrderOutgoingCommandReaderBuilder OnChangeIsPaid(Func<ChangeIsPaidFromIntegrationItem, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}