using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.RefundToCustomer;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RefundToCustomer.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.RefundToCustomer
{
    [InjectAsSingleton(typeof(IRefundToCustomerCommandReaderBuilder))]
    public class RefundToCustomerCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IRefundToCustomerCommandReaderBuilder
    {
        public RefundToCustomerCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.RefundToCustomer.Command.Topic,
                  MoneyTopics.PaymentOrders.RefundToCustomer.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IRefundToCustomerCommandReaderBuilder OnImport(Func<ImportRefundToCustomer, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IRefundToCustomerCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateRefundToCustomer, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IRefundToCustomerCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorRefundToCustomer, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}