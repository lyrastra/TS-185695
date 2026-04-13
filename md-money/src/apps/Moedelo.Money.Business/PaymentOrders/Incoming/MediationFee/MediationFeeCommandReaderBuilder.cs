using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.MediationFee;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.MediationFee.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.MediationFee
{
    [InjectAsSingleton(typeof(IMediationFeeCommandReaderBuilder))]
    public class MediationFeeCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IMediationFeeCommandReaderBuilder
    {
        public MediationFeeCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.MediationFee.Command.Topic,
                  MoneyTopics.PaymentOrders.MediationFee.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IMediationFeeCommandReaderBuilder OnImport(Func<ImportMediationFee, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IMediationFeeCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateMediationFee, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IMediationFeeCommandReaderBuilder OnImportWithMissingContract(Func<ImportWithMissingContractMediationFee, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IMediationFeeCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorMediationFee, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}