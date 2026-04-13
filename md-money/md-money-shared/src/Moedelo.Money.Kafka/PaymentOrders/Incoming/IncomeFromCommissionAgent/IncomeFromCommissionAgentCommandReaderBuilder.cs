using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.IncomeFromCommissionAgent.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.IncomeFromCommissionAgent
{
    [InjectAsSingleton(typeof(IIncomeFromCommissionAgentCommandReaderBuilder))]
    public class IncomeFromCommissionAgentCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IIncomeFromCommissionAgentCommandReaderBuilder
    {
        public IncomeFromCommissionAgentCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.IncomeFromCommissionAgent.Command.Topic,
                  MoneyTopics.PaymentOrders.IncomeFromCommissionAgent.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IIncomeFromCommissionAgentCommandReaderBuilder OnImport(Func<ImportIncomeFromCommissionAgent, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IIncomeFromCommissionAgentCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateIncomeFromCommissionAgent, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IIncomeFromCommissionAgentCommandReaderBuilder OnImportWithMissingContract(Func<ImportWithMissingContractIncomeFromCommissionAgent, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IIncomeFromCommissionAgentCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorIncomeFromCommissionAgent, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}