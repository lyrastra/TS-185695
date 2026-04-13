using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.CurrencySale;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencySale.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.CurrencySale
{
    [InjectAsSingleton(typeof(IOutgoingCurrencySaleCommandReaderBuilder))]
    public class OutgoingCurrencySaleCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IOutgoingCurrencySaleCommandReaderBuilder
    {
        public OutgoingCurrencySaleCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.OutgoingCurrencySale.Command.Topic,
                  MoneyTopics.PaymentOrders.OutgoingCurrencySale.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IOutgoingCurrencySaleCommandReaderBuilder OnImport(Func<ImportOutgoingCurrencySale, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IOutgoingCurrencySaleCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateOutgoingCurrencySale, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IOutgoingCurrencySaleCommandReaderBuilder OnImportWithMissingExchangeRate(Func<ImportWithMissingMissingExchangeRateOutgoingCurrencySale, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}