using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromPurse;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromPurse.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.TransferFromPurse
{
    [InjectAsSingleton(typeof(ITransferFromPurseCommandReaderBuilder))]
    public class TransferFromPurseCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, ITransferFromPurseCommandReaderBuilder
    {
        public TransferFromPurseCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.TransferFromPurse.Command.Topic,
                  MoneyTopics.PaymentOrders.TransferFromPurse.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public ITransferFromPurseCommandReaderBuilder OnImport(Func<ImportTransferFromPurse, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public ITransferFromPurseCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateTransferFromPurse, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}