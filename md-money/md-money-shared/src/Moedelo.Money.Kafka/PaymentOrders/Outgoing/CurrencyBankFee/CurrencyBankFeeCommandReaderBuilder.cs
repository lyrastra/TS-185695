using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyBankFee.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyBankFee
{
    [InjectAsSingleton(typeof(ICurrencyBankFeeCommandReaderBuilder))]
    public class CurrencyBankFeeCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, ICurrencyBankFeeCommandReaderBuilder
    {
        public CurrencyBankFeeCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.CurrencyBankFee.Command.Topic,
                  MoneyTopics.PaymentOrders.CurrencyBankFee.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public ICurrencyBankFeeCommandReaderBuilder OnImport(Func<ImportCurrencyBankFee, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ICurrencyBankFeeCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateCurrencyBankFee, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ICurrencyBankFeeCommandReaderBuilder OnImportWithMissingExchangeRate(Func<ImportWithMissingExchangeRateCurrencyBankFee, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}