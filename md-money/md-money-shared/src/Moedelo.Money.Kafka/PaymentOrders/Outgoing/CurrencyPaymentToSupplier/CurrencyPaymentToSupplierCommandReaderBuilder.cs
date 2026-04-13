using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyPaymentToSupplier.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    [InjectAsSingleton(typeof(ICurrencyPaymentToSupplierCommandReaderBuilder))]
    public class CurrencyPaymentToSupplierCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, ICurrencyPaymentToSupplierCommandReaderBuilder
    {
        public CurrencyPaymentToSupplierCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.CurrencyPaymentToSupplier.Command.Topic,
                  MoneyTopics.PaymentOrders.CurrencyPaymentToSupplier.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public ICurrencyPaymentToSupplierCommandReaderBuilder OnImport(Func<ImportCurrencyPaymentToSupplier, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ICurrencyPaymentToSupplierCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateCurrencyPaymentToSupplier, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ICurrencyPaymentToSupplierCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingMissingContractorCurrencyPaymentToSupplier, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ICurrencyPaymentToSupplierCommandReaderBuilder OnImportWithMissingExchangeRate(Func<ImportWithMissingMissingExchangeRateCurrencyPaymentToSupplier, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}