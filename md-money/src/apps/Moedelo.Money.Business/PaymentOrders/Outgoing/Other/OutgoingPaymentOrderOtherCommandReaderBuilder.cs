using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Other;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Other.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Other
{
    [InjectAsSingleton(typeof(IOutgoingPaymentOrderOtherCommandReaderBuilder))]
    public class OutgoingPaymentOrderOtherCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IOutgoingPaymentOrderOtherCommandReaderBuilder
    {
        public OutgoingPaymentOrderOtherCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.OtherOutgoing.Command.Topic,
                  MoneyTopics.PaymentOrders.OtherOutgoing.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IOutgoingPaymentOrderOtherCommandReaderBuilder OnImport(Func<ImportOtherOutgoing, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IOutgoingPaymentOrderOtherCommandReaderBuilder OnImportDuplicate(Func<ImportOtherOutgoingDuplicate, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IOutgoingPaymentOrderOtherCommandReaderBuilder OnImportWithMissingContractor(Func<ImportOtherOutgoingWithMissingContragent, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}
