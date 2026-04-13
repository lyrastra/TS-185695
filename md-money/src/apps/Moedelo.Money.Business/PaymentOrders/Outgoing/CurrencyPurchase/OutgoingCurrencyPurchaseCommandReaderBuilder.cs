using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPurchase.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencyPurchase
{
    [InjectAsSingleton(typeof(IOutgoingCurrencyPurchaseCommandReaderBuilder))]
    public class OutgoingCurrencyPurchaseCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IOutgoingCurrencyPurchaseCommandReaderBuilder
    {
        public OutgoingCurrencyPurchaseCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.OutgoingCurrencyPurchase.Command.Topic,
                  MoneyTopics.PaymentOrders.OutgoingCurrencyPurchase.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IOutgoingCurrencyPurchaseCommandReaderBuilder OnImport(Func<ImportOutgoingCurrencyPurchase, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IOutgoingCurrencyPurchaseCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateOutgoingCurrencyPurchase, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IOutgoingCurrencyPurchaseCommandReaderBuilder OnImportWithMissingExchangeRate(Func<ImportWithMissingMissingExchangeRateOutgoingCurrencyPurchase, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IOutgoingCurrencyPurchaseCommandReaderBuilder OnImportWithMissingCurrencySettlementAccount(Func<ImportWithMissingCurrencySettlementAccountOutgoingCurrencyPurchase, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}