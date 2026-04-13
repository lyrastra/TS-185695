using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.FinancialAssistance;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.FinancialAssistance.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.FinancialAssistance
{
    [InjectAsSingleton(typeof(IFinancialAssistanceCommandReaderBuilder))]
    public class FinancialAssistanceCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IFinancialAssistanceCommandReaderBuilder
    {
        public FinancialAssistanceCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.FinancialAssistance.Command.Topic,
                  MoneyTopics.PaymentOrders.FinancialAssistance.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IFinancialAssistanceCommandReaderBuilder OnImport(Func<ImportFinancialAssistance, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IFinancialAssistanceCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateFinancialAssistance, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IFinancialAssistanceCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorFinancialAssistance, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}