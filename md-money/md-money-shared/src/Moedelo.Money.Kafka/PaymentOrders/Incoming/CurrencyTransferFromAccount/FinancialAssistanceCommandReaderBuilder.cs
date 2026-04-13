using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencyTransferFromAccount.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    [InjectAsSingleton(typeof(ICurrencyTransferFromAccountCommandReaderBuilder))]
    public class CurrencyTransferFromAccountCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, ICurrencyTransferFromAccountCommandReaderBuilder
    {
        public CurrencyTransferFromAccountCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.CurrencyTransferFromAccount.Command.Topic,
                  MoneyTopics.PaymentOrders.CurrencyTransferFromAccount.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public ICurrencyTransferFromAccountCommandReaderBuilder OnImport(Func<ImportCurrencyTransferFromAccount, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ICurrencyTransferFromAccountCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateCurrencyTransferFromAccount, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ICurrencyTransferFromAccountCommandReaderBuilder OnImportWithMissingCurrencySettlementAccount(Func<ImportWithMissingCurrencySettlementAccountCurrencyTransferFromAccount, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}