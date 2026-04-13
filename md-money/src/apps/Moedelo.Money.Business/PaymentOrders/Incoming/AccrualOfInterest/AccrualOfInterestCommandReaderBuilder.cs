using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Incoming.AccrualOfInterest;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.AccrualOfInterest.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Incoming.AccrualOfInterest
{
    [InjectAsSingleton(typeof(IAccrualOfInterestCommandReaderBuilder))]
    public class AccrualOfInterestCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IAccrualOfInterestCommandReaderBuilder
    {
        public AccrualOfInterestCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.AccrualOfInterest.Command.Topic,
                  MoneyTopics.PaymentOrders.AccrualOfInterest.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IAccrualOfInterestCommandReaderBuilder OnImport(Func<ImportAccrualOfInterest, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IAccrualOfInterestCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateAccrualOfInterest, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}