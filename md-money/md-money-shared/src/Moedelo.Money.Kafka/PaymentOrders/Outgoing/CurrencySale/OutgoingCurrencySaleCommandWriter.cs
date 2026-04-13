using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencySale;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.CurrencySale.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencySale
{
    [InjectAsSingleton(typeof(IOutgoingCurrencySaleCommandWriter))]
    internal sealed class OutgoingCurrencySaleCommandWriter : IOutgoingCurrencySaleCommandWriter
    {
        private const string EntityName = MoneyTopics.PaymentOrders.OutgoingCurrencySale.EntityName;
        private static readonly string Topic = MoneyTopics.PaymentOrders.OutgoingCurrencySale.Command.Topic;

        private readonly IMoedeloEntityCommandKafkaTopicWriter commandKafkaTopicWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly MoedeloEntityCommandKafkaMessageDefinitionBuilder definitionBuilder;

        public OutgoingCurrencySaleCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter commandKafkaTopicWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor)
        {
            this.commandKafkaTopicWriter = commandKafkaTopicWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;

            definitionBuilder = MoedeloEntityCommandKafkaMessageDefinitionBuilder.For(Topic, EntityName);
        }

        public Task WriteImportAsync(
            ImportOutgoingCurrencySale commandData)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            var commandDefinition = definitionBuilder.CreateCommandDefinition(context.FirmId.ToString(), commandData);
            return commandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }

        public Task WriteImportDuplicateAsync(
            ImportDuplicateOutgoingCurrencySale commandData)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            var commandDefinition = definitionBuilder.CreateCommandDefinition(context.FirmId.ToString(), commandData);
            return commandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }

        public Task WriteImportWithMissingExchangeRateAsync(
            ImportWithMissingMissingExchangeRateOutgoingCurrencySale commandData)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            var commandDefinition = definitionBuilder.CreateCommandDefinition(context.FirmId.ToString(), commandData);
            return commandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }
    }
}