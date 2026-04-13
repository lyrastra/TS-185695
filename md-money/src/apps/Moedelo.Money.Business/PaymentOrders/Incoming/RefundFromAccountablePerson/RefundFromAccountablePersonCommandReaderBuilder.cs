using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RefundFromAccountablePerson.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.RefundFromAccountablePerson
{
    [InjectAsSingleton(typeof(IRefundFromAccountablePersonCommandReaderBuilder))]
    public class RefundFromAccountablePersonCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IRefundFromAccountablePersonCommandReaderBuilder
    {
        public RefundFromAccountablePersonCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.RefundFromAccountablePerson.Command.Topic,
                  MoneyTopics.PaymentOrders.RefundFromAccountablePerson.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IRefundFromAccountablePersonCommandReaderBuilder OnImport(Func<ImportRefundFromAccountablePerson, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IRefundFromAccountablePersonCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateRefundFromAccountablePerson, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IRefundFromAccountablePersonCommandReaderBuilder OnImportWithMissingEmployee(Func<ImportWithMissingEmployeeRefundFromAccountablePerson, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}