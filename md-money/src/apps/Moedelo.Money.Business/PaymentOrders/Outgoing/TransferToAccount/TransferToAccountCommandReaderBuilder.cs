using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.TransferToAccount;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.TransferToAccount.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.TransferToAccount
{
    [InjectAsSingleton(typeof(ITransferToAccountCommandReaderBuilder))]
    public class TransferToAccountCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, ITransferToAccountCommandReaderBuilder
    {
        public TransferToAccountCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.TransferToAccount.Command.Topic,
                  MoneyTopics.PaymentOrders.TransferToAccount.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public ITransferToAccountCommandReaderBuilder OnImport(Func<ImportTransferToAccount, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ITransferToAccountCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateTransferToAccount, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ITransferToAccountCommandReaderBuilder OnApplyIgnoreNumber(Func<ApplyIgnoreNumberTransferToAccount, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}
