using System.Threading.Tasks;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Konragents.Kafka.Abstractions;
using Moedelo.Konragents.Kafka.Abstractions.KontragentDebt;
using Moedelo.Konragents.Kafka.Abstractions.KontragentDebt.Commands;

namespace Moedelo.Konragents.Kafka.KontragentDebt
{
    [InjectAsSingleton(typeof(IKontragentDebtCommandWriter))]
    public class KontragentDebtCommandWriter : IKontragentDebtCommandWriter
    {
        private const string EntityName = KontragentsTopics.KontragentDebt.EntityName;
        private static readonly string Topic = KontragentsTopics.KontragentDebt.Command.Topic;

        private readonly IMoedeloEntityCommandKafkaTopicWriter commandKafkaTopicWriter;
        private readonly IExecutionInfoContextAccessor executionInfoContextAccessor;
        private readonly MoedeloEntityCommandKafkaMessageDefinitionBuilder definitionBuilder;

        public KontragentDebtCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter commandKafkaTopicWriter,
            IExecutionInfoContextAccessor executionInfoContextAccessor)
        {
            this.commandKafkaTopicWriter = commandKafkaTopicWriter;
            this.executionInfoContextAccessor = executionInfoContextAccessor;

            definitionBuilder = MoedeloEntityCommandKafkaMessageDefinitionBuilder.For(Topic, EntityName);
        }

        public Task WriteRecalculationCommandAsync(KontragentDebtRecalculationCommand commandData)
        {
            var context = executionInfoContextAccessor.ExecutionInfoContext;
            var commandDefinition = definitionBuilder.CreateCommandDefinition(context.FirmId.ToString(), commandData);
            return commandKafkaTopicWriter.WriteCommandDataAsync(commandDefinition);
        }
    }
}