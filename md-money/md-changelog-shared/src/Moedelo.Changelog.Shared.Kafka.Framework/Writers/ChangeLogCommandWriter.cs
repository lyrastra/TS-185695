using System;
using System.Threading.Tasks;
using Moedelo.Changelog.Shared.Kafka.Abstractions;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Extensions;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Writers;
using Moedelo.Changelog.Shared.Kafka.Framework.Commands;
using Moedelo.ExecutionContext.Client;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;

namespace Moedelo.Changelog.Shared.Kafka.Framework.Writers
{
    [InjectAsSingleton(typeof(IChangeLogCommandWriter))]
    public sealed class ChangeLogCommandWriter : IChangeLogCommandWriter
    {
        private readonly IMoedeloEntityCommandKafkaTopicWriter topicWriter;
        private readonly ITokenApiClient tokenClient;

        public ChangeLogCommandWriter(
            IMoedeloEntityCommandKafkaTopicWriter topicWriter,
            ITokenApiClient tokenClient)
        {
            this.topicWriter = topicWriter;
            this.tokenClient = tokenClient;
        }

        public async Task WriteAsync(EntityStateSaveCommandFields commandFields)
        {
            var command = new EntityStateSaveCommand
            {
                CreatedAt = DateTime.Now,
                Fields = commandFields
            };
            
            var token = await tokenClient
                .GetFromUserContextAsync(commandFields.FirmId, commandFields.AuthorUserId)
                .ConfigureAwait(false);

            await topicWriter.WriteCommandDataAsync(
                Topics.EntityState.Command.Topic,
                commandFields.GetCommandKey(),
                Topics.EntityState.EntityName,
                command,
                token).ConfigureAwait(false); 
        }

        public async Task WriteAsync(ExplicitChangesSaveCommandFields commandFields)
        {
            var command = new ExplicitChangesSaveCommand
            {
                CreatedAt = DateTime.Now,
                Fields = commandFields
            };
            
            var token = await tokenClient
                .GetFromUserContextAsync(commandFields.FirmId, commandFields.AuthorUserId)
                .ConfigureAwait(false);

            await topicWriter.WriteCommandDataAsync(
                Topics.EntityState.Command.Topic,
                commandFields.GetCommandKey(),
                Topics.EntityState.EntityName,
                command,
                token).ConfigureAwait(false);
        }
    }
}
