using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PurseOperations.Common;
using Moedelo.Money.Kafka.Abstractions.PurseOperations.Common.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PurseOperations.Common
{
    [InjectAsSingleton(typeof(IPurseOperationCommandReaderBuilder))]
    public class PurseOperationCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IPurseOperationCommandReaderBuilder
    {
        public PurseOperationCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PurseOperations.PurseOperation.Command.Topic,
                  MoneyTopics.PurseOperations.PurseOperation.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IPurseOperationCommandReaderBuilder OnCreate(Func<CreatePurseOperation, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}