using System;
using System.Threading.Tasks;
using Moedelo.Changelog.Shared.Kafka.Abstractions;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Extensions;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Writers;
using Moedelo.Changelog.Shared.Kafka.NetCore.Commands;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;

namespace Moedelo.Changelog.Shared.Kafka.NetCore.Writers
{
    [InjectAsSingleton(typeof(IChangeLogCommandWriter))]
    internal sealed class ChangeLogCommandWriter : MoedeloKafkaTopicWriterBase, IChangeLogCommandWriter
    {
        private readonly MoedeloEntityCommandKafkaMessageDefinitionBuilder commandDefinitionBuilder;
        private readonly IMoedeloEntityCommandKafkaTopicWriter topicWriter;

        public ChangeLogCommandWriter(
            IMoedeloKafkaTopicWriterBaseDependencies dependencies,
            IMoedeloEntityCommandKafkaTopicWriter topicWriter)
            : base(dependencies)
        {
            this.topicWriter = topicWriter;
            this.commandDefinitionBuilder = MoedeloEntityCommandKafkaMessageDefinitionBuilder.For(
                Topics.EntityState.Command.Topic,
                Topics.EntityState.EntityName);
        }

        public Task WriteAsync(EntityStateSaveCommandFields commandFields)
        {
            var command = new EntityStateSaveCommand
            {
                CreatedAt = DateTime.Now,
                Fields = commandFields
            };

            var definition = commandDefinitionBuilder.CreateCommandDefinition(
                command.Fields.GetCommandKey(),
                command);

            return topicWriter.WriteCommandDataAsync(definition);
        }

        public Task WriteAsync(ExplicitChangesSaveCommandFields commandFields)
        {
            var command = new ExplicitChangesSaveCommand
            {
                CreatedAt = DateTime.Now,
                Fields = commandFields
            };

            var definition = commandDefinitionBuilder.CreateCommandDefinition(
                command.Fields.GetCommandKey(),
                command);

            return topicWriter.WriteCommandDataAsync(definition);
        }
    }
}
