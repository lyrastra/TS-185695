using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.UnifiedBudgetaryPayment
{
    [InjectAsSingleton(typeof(IUnifiedBudgetaryPaymentCommandWriter))]
    internal sealed class UnifiedBudgetaryPaymentCommandWriter : IUnifiedBudgetaryPaymentCommandWriter
    {
        private const string EntityName = MoneyTopics.PaymentOrders.UnifiedBudgetaryPayment.EntityName;
        private static readonly string Topic = MoneyTopics.PaymentOrders.UnifiedBudgetaryPayment.Command.Topic;

        private readonly IMoedeloEntityCommandKafkaTopicWriter commandKafkaTopicWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly MoedeloEntityCommandKafkaMessageDefinitionBuilder definitionBuilder;

        public UnifiedBudgetaryPaymentCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter commandKafkaTopicWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor)
        {
            this.commandKafkaTopicWriter = commandKafkaTopicWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;

            definitionBuilder = MoedeloEntityCommandKafkaMessageDefinitionBuilder.For(Topic, EntityName);
        }

        public Task WriteImportAsync(ImportUnifiedBudgetaryPayment commandData)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            var commandDefinition = definitionBuilder.CreateCommandDefinition(context.FirmId.ToString(), commandData);
            return commandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }

        public Task WriteImportDuplicateAsync(ImportDuplicateUnifiedBudgetaryPayment commandData)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            var commandDefinition = definitionBuilder.CreateCommandDefinition(context.FirmId.ToString(), commandData);
            return commandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }
    }
}