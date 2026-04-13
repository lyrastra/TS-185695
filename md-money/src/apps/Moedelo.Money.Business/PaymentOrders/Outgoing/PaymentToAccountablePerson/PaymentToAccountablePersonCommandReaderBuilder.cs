using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    [InjectAsSingleton(typeof(IPaymentToAccountablePersonCommandReaderBuilder))]
    public class PaymentToAccountablePersonCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IPaymentToAccountablePersonCommandReaderBuilder
    {
        public PaymentToAccountablePersonCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.PaymentToAccountablePerson.Command.Topic,
                  MoneyTopics.PaymentOrders.PaymentToAccountablePerson.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IPaymentToAccountablePersonCommandReaderBuilder OnImport(Func<ImportPaymentToAccountablePerson, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IPaymentToAccountablePersonCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicatePaymentToAccountablePerson, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IPaymentToAccountablePersonCommandReaderBuilder OnImportWithMissingEmployee(Func<ImportWithMissingEmployeePaymentToAccountablePerson, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IPaymentToAccountablePersonCommandReaderBuilder OnApplyIgnoreNumber(Func<ApplyIgnoreNumberPaymentToAccountablePerson, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}