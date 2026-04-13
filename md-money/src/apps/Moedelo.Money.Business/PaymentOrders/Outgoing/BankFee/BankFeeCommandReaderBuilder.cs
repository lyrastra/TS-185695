using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.BankFee;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BankFee.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.BankFee
{
    [InjectAsSingleton(typeof(IBankFeeCommandReaderBuilder))]
    public class BankFeeCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IBankFeeCommandReaderBuilder
    {
        public BankFeeCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.BankFee.Command.Topic,
                  MoneyTopics.PaymentOrders.BankFee.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IBankFeeCommandReaderBuilder OnImport(Func<ImportBankFee, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IBankFeeCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateBankFee, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}