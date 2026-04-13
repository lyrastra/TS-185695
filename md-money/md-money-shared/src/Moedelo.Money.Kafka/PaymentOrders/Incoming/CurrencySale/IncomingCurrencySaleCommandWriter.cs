using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencySale;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.CurrencySale.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencySale
{
    [InjectAsSingleton(typeof(IIncomingCurrencySaleCommandWriter))]
    internal sealed class IncomingCurrencySaleCommandWriter : IIncomingCurrencySaleCommandWriter
    {
        private const string EntityName = MoneyTopics.PaymentOrders.IncomingCurrencySale.EntityName;
        private static readonly string Topic = MoneyTopics.PaymentOrders.IncomingCurrencySale.Command.Topic;

        private readonly IMoedeloEntityCommandKafkaTopicWriter commandKafkaTopicWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly MoedeloEntityCommandKafkaMessageDefinitionBuilder definitionBuilder;

        public IncomingCurrencySaleCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter commandKafkaTopicWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor)
        {
            this.commandKafkaTopicWriter = commandKafkaTopicWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;

            definitionBuilder = MoedeloEntityCommandKafkaMessageDefinitionBuilder.For(Topic, EntityName);
        }

        public Task WriteImportAsync(ImportIncomingCurrencySale commandData)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            var commandDefinition = definitionBuilder.CreateCommandDefinition(context.FirmId.ToString(), commandData);
            return commandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }

        public Task WriteImportDuplicateAsync(ImportDuplicateIncomingCurrencySale commandData)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            var commandDefinition = definitionBuilder.CreateCommandDefinition(context.FirmId.ToString(), commandData);
            return commandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }

        public Task WriteImportWithMissingCurrencySettlementAccountAsync(
            ImportWithMissingCurrencySettlementAccountIncomingCurrencySale commandData)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            var commandDefinition = definitionBuilder.CreateCommandDefinition(context.FirmId.ToString(), commandData);
            return commandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }
    }
}