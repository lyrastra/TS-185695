using System;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Konragents.Kafka.Abstractions;
using Moedelo.Konragents.Kafka.Abstractions.KontragentDebt;
using Moedelo.Konragents.Kafka.Abstractions.KontragentDebt.Commands;

namespace Moedelo.Konragents.Kafka.KontragentDebt
{
    [InjectAsSingleton(typeof(IKontragentDebtCommandReaderBuilder))]
    public class KontragentDebtCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IKontragentDebtCommandReaderBuilder
    {
        public KontragentDebtCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                KontragentsTopics.KontragentDebt.Command.Topic,
                KontragentsTopics.KontragentDebt.EntityName,
                reader,
                replyWriter,
                contextInitializer,
                contextAccessor,
                auditTracer)
        {
        }

        public IKontragentDebtCommandReaderBuilder OnRecalculate(Func<KontragentDebtRecalculationCommand, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}