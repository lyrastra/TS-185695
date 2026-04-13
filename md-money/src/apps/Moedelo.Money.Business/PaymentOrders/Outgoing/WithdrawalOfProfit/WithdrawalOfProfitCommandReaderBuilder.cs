using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Business.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System;
using System.Threading.Tasks;

namespace Moedelo.Money.Business.PaymentOrders.Outgoing.WithdrawalOfProfit
{
    [InjectAsSingleton(typeof(IWithdrawalOfProfitCommandReaderBuilder))]
    public class WithdrawalOfProfitCommandReaderBuilder : MoedeloEntityCommandKafkaTopicReaderBuilder, IWithdrawalOfProfitCommandReaderBuilder
    {
        public WithdrawalOfProfitCommandReaderBuilder(
            IMoedeloEntityCommandKafkaTopicReader reader,
            IMoedeloEntityCommandReplyKafkaTopicWriter replyWriter,
            IExecutionInfoContextInitializer contextInitializer,
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer)
            : base(
                  MoneyTopics.PaymentOrders.WithdrawalOfProfit.Command.Topic,
                  MoneyTopics.PaymentOrders.WithdrawalOfProfit.EntityName,
                  reader,
                  replyWriter,
                  contextInitializer,
                  contextAccessor,
                  auditTracer)
        {
        }

        public IWithdrawalOfProfitCommandReaderBuilder OnImport(Func<ImportWithdrawalOfProfit, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IWithdrawalOfProfitCommandReaderBuilder OnImportDuplicate(Func<ImportDuplicateWithdrawalOfProfit, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IWithdrawalOfProfitCommandReaderBuilder OnImportWithMissingContractor(Func<ImportWithMissingContractorWithdrawalOfProfit, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }

        public IWithdrawalOfProfitCommandReaderBuilder OnApplyIgnoreNumber(Func<ApplyIgnoreNumberWithdrawalOfProfit, Task> onCommand)
        {
            OnCommand(onCommand);
            return this;
        }
    }
}
