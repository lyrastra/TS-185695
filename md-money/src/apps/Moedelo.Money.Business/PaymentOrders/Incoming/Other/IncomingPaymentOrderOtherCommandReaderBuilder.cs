using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.Other;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.Other.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.Other
{
    [InjectAsSingleton(typeof(IIncomingPaymentOrderOtherCommandReaderBuilder))]
    public class IncomingPaymentOrderOtherCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IIncomingPaymentOrderOtherCommandReaderBuilder
    {
        public IncomingPaymentOrderOtherCommandReaderBuilder(IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(MoneyTopics.PaymentOrders.OtherIncoming.Command.Topic,
                  MoneyTopics.PaymentOrders.OtherIncoming.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {

        }

        public IIncomingPaymentOrderOtherCommandReaderBuilder OnImport(Func<ImportOtherIncoming, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IIncomingPaymentOrderOtherCommandReaderBuilder OnImportDuplicate(Func<ImportOtherIncomingDuplicate, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IIncomingPaymentOrderOtherCommandReaderBuilder OnImportWithMissingContractor(Func<ImportOtherIncomingWithMissingContragent, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}
