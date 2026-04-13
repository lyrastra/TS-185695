using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencyTransferToAccount.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyTransferToAccount
{
    [InjectAsSingleton(typeof(ICurrencyTransferToAccountCommandReaderBuilder))]
    public class CurrencyTransferToAccountCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, ICurrencyTransferToAccountCommandReaderBuilder
    {
        public CurrencyTransferToAccountCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.CurrencyTransferToAccount.Command.Topic,
                  MoneyTopics.PaymentOrders.CurrencyTransferToAccount.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public ICurrencyTransferToAccountCommandReaderBuilder OnImport(Func<ImportCurrencyTransferToAccount, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ICurrencyTransferToAccountCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateCurrencyTransferToAccount, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ICurrencyTransferToAccountCommandReaderBuilder OnImportWithMissingCurrencySettlementAccount(Func<ImportWithMissingCurrencySettlementAccountCurrencyTransferToAccount, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}