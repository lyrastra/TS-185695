using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Requisites.Kafka.Abstractions.RequisitesFilling;
using Moedelo.Requisites.Kafka.Abstractions.RequisitesFilling.Commands;
using Moedelo.Requisites.Kafka.Abstractions.RequisitesFilling.Writers;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Requisites.Kafka.RequisitesFilling.Writers
{
    [InjectAsSingleton(typeof(IRequisitesFillingCommandWriter))]
    internal sealed class RequisitesFillingCommandWriter : MoedeloKafkaTopicWriterBase, IRequisitesFillingCommandWriter
    {
        private readonly MoedeloEntityCommandKafkaMessageDefinitionBuilder commandDefinitionBuilder;
        private readonly IMoedeloEntityCommandKafkaTopicWriter topicWriter;

        public RequisitesFillingCommandWriter(
            IMoedeloKafkaTopicWriterBaseDependencies dependencies,
            IMoedeloEntityCommandKafkaTopicWriter topicWriter)
            : base(dependencies)
        {
            this.topicWriter = topicWriter;
            this.commandDefinitionBuilder = MoedeloEntityCommandKafkaMessageDefinitionBuilder.For(
                Topics.Inn.Command.Topic,
                Topics.Inn.EntityName);
        }

        /// <summary>
        /// Записывает команду заполнения реквизитов в Kafka.
        /// </summary>
        /// <param name="commandData">Данные команды заполнения реквизитов.</param>
        /// <returns>Задача, представляющая асинхронную операцию.</returns>
        public Task WriteAsync(RequisitesFillingCommand commandData, CancellationToken cancellationToken)
        {
            var definition = commandDefinitionBuilder.CreateCommandDefinition(commandData.Inn, commandData);
            return topicWriter.WriteCommandDataAsync(definition, cancellationToken);
        }
    }
}