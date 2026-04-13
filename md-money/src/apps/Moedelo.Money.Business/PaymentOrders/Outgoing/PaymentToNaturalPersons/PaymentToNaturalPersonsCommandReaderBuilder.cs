using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToNaturalPersons.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    [InjectAsSingleton(typeof(IPaymentToNaturalPersonsCommandReaderBuilder))]
    public class PaymentToNaturalPersonsCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IPaymentToNaturalPersonsCommandReaderBuilder
    {
        public PaymentToNaturalPersonsCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.PaymentToNaturalPersons.Command.Topic,
                  MoneyTopics.PaymentOrders.PaymentToNaturalPersons.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IPaymentToNaturalPersonsCommandReaderBuilder OnImport(Func<ImportPaymentToNaturalPersons, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IPaymentToNaturalPersonsCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicatePaymentToNaturalPersons, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IPaymentToNaturalPersonsCommandReaderBuilder OnImportWithMissingEmployee(Func<ImportWithMissingEmployeePaymentToNaturalPersons, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}