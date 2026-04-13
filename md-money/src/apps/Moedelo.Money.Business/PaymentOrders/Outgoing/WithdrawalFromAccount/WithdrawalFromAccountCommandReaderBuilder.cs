using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    [InjectAsSingleton(typeof(IWithdrawalFromAccountCommandReaderBuilder))]
    public class WithdrawalFromAccountCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IWithdrawalFromAccountCommandReaderBuilder
    {
        public WithdrawalFromAccountCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.WithdrawalFromAccount.Command.Topic,
                  MoneyTopics.PaymentOrders.WithdrawalFromAccount.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IWithdrawalFromAccountCommandReaderBuilder OnImport(Func<ImportWithdrawalFromAccount, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IWithdrawalFromAccountCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateWithdrawalFromAccount, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IWithdrawalFromAccountCommandReaderBuilder OnApplyIgnoreNumber(Func<ApplyIgnoreNumberWithdrawalFromAccount, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}
