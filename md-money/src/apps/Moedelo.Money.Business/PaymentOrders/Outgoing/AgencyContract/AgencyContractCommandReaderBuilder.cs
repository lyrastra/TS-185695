using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.AgencyContract;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.AgencyContract.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.AgencyContract
{
    [InjectAsSingleton(typeof(IAgencyContractCommandReaderBuilder))]
    public class AgencyContractCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IAgencyContractCommandReaderBuilder
    {
        public AgencyContractCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.AgencyContract.Command.Topic,
                  MoneyTopics.PaymentOrders.AgencyContract.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IAgencyContractCommandReaderBuilder OnImport(Func<ImportAgencyContract, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IAgencyContractCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateAgencyContract, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IAgencyContractCommandReaderBuilder OnImportWithMissingContract(Func<ImportWithMissingContractAgencyContract, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IAgencyContractCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorAgencyContract, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}