using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.PaymentFromCustomer
{
    [InjectAsSingleton(typeof(IPaymentFromCustomerCommandReaderBuilder))]
    class PaymentFromCustomerCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IPaymentFromCustomerCommandReaderBuilder
    {
        public PaymentFromCustomerCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.PaymentFromCustomer.Command.Topic,
                  MoneyTopics.PaymentOrders.PaymentFromCustomer.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IPaymentFromCustomerCommandReaderBuilder OnImport(Func<ImportPaymentFromCustomer, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IPaymentFromCustomerCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicatePaymentFromCustomer, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IPaymentFromCustomerCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorPaymentFromCustomer, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}
