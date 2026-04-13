using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson;
using Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson.Commands;
using Moedelo.Money.Kafka.Abstractions.Topics;
using System.Threading.Tasks;

namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    [InjectAsSingleton(typeof(IPaymentToAccountablePersonCommandWriter))]
    internal sealed class PaymentToAccountablePersonCommandWriter : IPaymentToAccountablePersonCommandWriter
    {
        private const string EntityName = MoneyTopics.PaymentOrders.PaymentToAccountablePerson.EntityName;
        private static readonly string Topic = MoneyTopics.PaymentOrders.PaymentToAccountablePerson.Command.Topic;

        private readonly IMoedeloEntityCommandKafkaTopicWriter commandKafkaTopicWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly MoedeloEntityCommandKafkaMessageDefinitionBuilder definitionBuilder;

        public PaymentToAccountablePersonCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter commandKafkaTopicWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor)
        {
            this.commandKafkaTopicWriter = commandKafkaTopicWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;

            definitionBuilder = MoedeloEntityCommandKafkaMessageDefinitionBuilder.For(Topic, EntityName);
        }

        public Task WriteApplyIgnoreNumberAsync(ApplyIgnoreNumberPaymentToAccountablePerson commandData)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            var commandDefinition = definitionBuilder.CreateCommandDefinition(context.FirmId.ToString(), commandData);
            return commandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }

        public Task WriteImportAsync(
            ImportPaymentToAccountablePerson commandData)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            var commandDefinition = definitionBuilder.CreateCommandDefinition(context.FirmId.ToString(), commandData);
            return commandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }

        public Task WriteImportDuplicateAsync(
            ImportDuplicatePaymentToAccountablePerson commandData)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            var commandDefinition = definitionBuilder.CreateCommandDefinition(context.FirmId.ToString(), commandData);
            return commandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }

        public Task WriteImportWithMissingEmployeeAsync(
            ImportWithMissingEmployeePaymentToAccountablePerson commandData)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            var commandDefinition = definitionBuilder.CreateCommandDefinition(context.FirmId.ToString(), commandData);
            return commandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }
    }
}