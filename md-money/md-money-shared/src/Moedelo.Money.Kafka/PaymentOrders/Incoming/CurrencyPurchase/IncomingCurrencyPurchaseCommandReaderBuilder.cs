using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPurchase;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPurchase.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencyPurchase
{
    [InjectAsSingleton(typeof(IIncomingCurrencyPurchaseCommandReaderBuilder))]
    public class IncomingCurrencyPurchaseCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IIncomingCurrencyPurchaseCommandReaderBuilder
    {
        public IncomingCurrencyPurchaseCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.IncomingCurrencyPurchase.Command.Topic,
                  MoneyTopics.PaymentOrders.IncomingCurrencyPurchase.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IIncomingCurrencyPurchaseCommandReaderBuilder OnImport(Func<ImportIncomingCurrencyPurchase, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IIncomingCurrencyPurchaseCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateIncomingCurrencyPurchase, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}