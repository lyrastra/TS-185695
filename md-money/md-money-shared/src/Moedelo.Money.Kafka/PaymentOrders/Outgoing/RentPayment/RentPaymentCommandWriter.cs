using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RentPayment;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.RentPayment.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.RentPayment
{
    [InjectAsSingleton]
    internal sealed class RentPaymentCommandWriter : IRentPaymentCommandWriter
    {
        private const string EntityName = MoneyTopics.PaymentOrders.RentPayment.EntityName;
        private static readonly string Topic = MoneyTopics.PaymentOrders.RentPayment.Command.Topic;

        private readonly IMoedeloEntityCommandKafkaTopicWriter commandKafkaTopicWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly MoedeloEntityCommandKafkaMessageDefinitionBuilder definitionBuilder;

        public RentPaymentCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter commandKafkaTopicWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor)
        {
            this.commandKafkaTopicWriter = commandKafkaTopicWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;

            definitionBuilder = MoedeloEntityCommandKafkaMessageDefinitionBuilder.For(Topic, EntityName);
        }

        public Task WriteImportAsync(ImportRentPayment command)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            var commandDefinition = definitionBuilder.CreateCommandDefinition(context.FirmId.ToString(), command);
            return commandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }

        public Task WriteImportWithMissingContractAsync(ImportWithMissingContractRentPayment command)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            var commandDefinition = definitionBuilder.CreateCommandDefinition(context.FirmId.ToString(), command);
            return commandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }

        public Task WriteImportDuplicateAsync(ImportDuplicateRentPayment command)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            var commandDefinition = definitionBuilder.CreateCommandDefinition(context.FirmId.ToString(), command);
            return commandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }

        public Task WriteImportWithMissingContractorAsync(ImportWithMissingContractorRentPayment command)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            var commandDefinition = definitionBuilder.CreateCommandDefinition(context.FirmId.ToString(), command);
            return commandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }
    }
}