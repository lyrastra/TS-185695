using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.Deduction;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Deduction.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.Deduction
{
    [InjectAsSingleton(typeof(IOutgoingPaymentOrderDeductionCommandReaderBuilder))]
    public class OutgoingPaymentOrderDeductionCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IOutgoingPaymentOrderDeductionCommandReaderBuilder
    {
        public OutgoingPaymentOrderDeductionCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.Deduction.Command.Topic,
                  MoneyTopics.PaymentOrders.Deduction.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IOutgoingPaymentOrderDeductionCommandReaderBuilder OnImport(Func<ImportDeduction, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IOutgoingPaymentOrderDeductionCommandReaderBuilder OnImportDuplicate(Func<ImportDeductionDuplicate, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IOutgoingPaymentOrderDeductionCommandReaderBuilder OnImportWithMissingContractor(Func<ImportDeductionWithMissingContractor, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
        
        public IOutgoingPaymentOrderDeductionCommandReaderBuilder OnImportWithMissingEmployee(Func<ImportDeductionWithMissingEmployee, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}
