using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.PaymentToSupplier
{
    [InjectAsSingleton(typeof(IPaymentToSupplierCommandReaderBuilder))]
    public class PaymentToSupplierCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IPaymentToSupplierCommandReaderBuilder
    {
        public PaymentToSupplierCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.PaymentToSupplier.Command.Topic,
                  MoneyTopics.PaymentOrders.PaymentToSupplier.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IPaymentToSupplierCommandReaderBuilder OnImport(Func<ImportPaymentToSupplier, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IPaymentToSupplierCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicatePaymentToSupplier, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IPaymentToSupplierCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorPaymentToSupplier, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IPaymentToSupplierCommandReaderBuilder OnApplyIgnoreNumber(Func<ApplyIgnoreNumberPaymentToSupplier, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}