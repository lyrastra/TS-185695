using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromAccount;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromAccount.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.TransferFromAccount
{
    [InjectAsSingleton(typeof(ITransferFromAccountCommandReaderBuilder))]
    public class TransferFromAccountCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, ITransferFromAccountCommandReaderBuilder
    {
        public TransferFromAccountCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.TransferFromAccount.Command.Topic,
                  MoneyTopics.PaymentOrders.TransferFromAccount.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public ITransferFromAccountCommandReaderBuilder OnImport(Func<ImportTransferFromAccount, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ITransferFromAccountCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateTransferFromAccount, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
        
        public ITransferFromAccountCommandReaderBuilder OnImportAmbiguousOperationType(Func<ImportAmbiguousOperationTypeTransferFromAccount, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}