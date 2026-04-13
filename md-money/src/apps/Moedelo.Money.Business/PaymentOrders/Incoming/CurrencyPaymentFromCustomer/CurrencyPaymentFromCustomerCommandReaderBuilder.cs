using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyPaymentFromCustomer.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    [InjectAsSingleton(typeof(ICurrencyPaymentFromCustomerCommandReaderBuilder))]
    public class CurrencyPaymentFromCustomerCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, ICurrencyPaymentFromCustomerCommandReaderBuilder
    {
        public CurrencyPaymentFromCustomerCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.CurrencyPaymentFromCustomer.Command.Topic,
                  MoneyTopics.PaymentOrders.CurrencyPaymentFromCustomer.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public ICurrencyPaymentFromCustomerCommandReaderBuilder OnImport(Func<ImportCurrencyPaymentFromCustomer, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ICurrencyPaymentFromCustomerCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateCurrencyPaymentFromCustomer, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ICurrencyPaymentFromCustomerCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorCurrencyPaymentFromCustomer, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ICurrencyPaymentFromCustomerCommandReaderBuilder OnImportWithMissingExchangeRate(Func<ImportWithMissingExchangeRateCurrencyPaymentFromCustomer, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}