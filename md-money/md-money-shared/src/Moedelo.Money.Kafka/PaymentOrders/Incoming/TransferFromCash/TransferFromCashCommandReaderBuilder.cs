using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromCash.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.TransferFromCash
{
    [InjectAsSingleton(typeof(ITransferFromCashCommandReaderBuilder))]
    public class TransferFromCashCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, ITransferFromCashCommandReaderBuilder
    {
        public TransferFromCashCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.TransferFromCash.Command.Topic,
                  MoneyTopics.PaymentOrders.TransferFromCash.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public ITransferFromCashCommandReaderBuilder OnImport(Func<ImportTransferFromCash, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ITransferFromCashCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateTransferFromCash, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}