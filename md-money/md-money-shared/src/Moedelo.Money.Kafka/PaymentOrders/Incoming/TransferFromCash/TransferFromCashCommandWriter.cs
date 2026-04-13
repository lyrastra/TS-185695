using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromCash;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.TransferFromCash.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.TransferFromCash
{
    [InjectAsSingleton(typeof(ITransferFromCashCommandWriter))]
    internal sealed class TransferFromCashCommandWriter : ITransferFromCashCommandWriter
    {
        private const string EntityName = MoneyTopics.PaymentOrders.TransferFromCash.EntityName;
        private static readonly string Topic = MoneyTopics.PaymentOrders.TransferFromCash.Command.Topic;

        private readonly IMoedeloEntityCommandKafkaTopicWriter commandKafkaTopicWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly MoedeloEntityCommandKafkaMessageDefinitionBuilder definitionBuilder;

        public TransferFromCashCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter commandKafkaTopicWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor)
        {
            this.commandKafkaTopicWriter = commandKafkaTopicWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;

            definitionBuilder = MoedeloEntityCommandKafkaMessageDefinitionBuilder.For(Topic, EntityName);
        }

        public Task WriteImportAsync(ImportTransferFromCash commandData)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            var commandDefinition = definitionBuilder.CreateCommandDefinition(context.FirmId.ToString(), commandData);
            return commandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }

        public Task WriteImportDuplicateAsync(ImportDuplicateTransferFromCash commandData)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            var commandDefinition = definitionBuilder.CreateCommandDefinition(context.FirmId.ToString(), commandData);
            return commandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }
    }
}