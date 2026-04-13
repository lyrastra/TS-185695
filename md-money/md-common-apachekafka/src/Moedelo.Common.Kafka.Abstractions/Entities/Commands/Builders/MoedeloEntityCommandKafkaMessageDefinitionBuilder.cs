using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;

namespace Moedelo.Common.Kafka.Abstractions.Entities.Commands.Builders
{
    public sealed class MoedeloEntityCommandKafkaMessageDefinitionBuilder
    {
        private readonly string topicName;
        private readonly string entityType;

        public static MoedeloEntityCommandKafkaMessageDefinitionBuilder For(string topicName, string entityType)
        {
            return new MoedeloEntityCommandKafkaMessageDefinitionBuilder(topicName, entityType);
        }

        private MoedeloEntityCommandKafkaMessageDefinitionBuilder(string topicName, string entityType)
        {
            if (string.IsNullOrWhiteSpace(topicName))
            {
                throw new ArgumentException(nameof(topicName));
            }

            if (string.IsNullOrWhiteSpace(entityType))
            {
                throw new ArgumentException(nameof(entityType));
            }

            this.topicName = topicName;
            this.entityType = entityType;
        }

        public IMoedeloEntityCommandKafkaMessageDefinition<T> CreateCommandDefinition<T>(
            string commandKey,
            T commandData)
            where T : IEntityCommandData
        {
            return new MoedeloEntityCommandKafkaMessageDefinition<T>(topicName, entityType, commandKey, commandData);
        }

        public IMoedeloEntityCommandKafkaMessageDefinition<CommandWithReplyData<T>> CreateCommandWithReplyDefinition<T>(
            string commandKey,
            T commandData,
            ReplyTo replyTo)
            where T : IEntityCommandData
        {
            var commandWithReplyData = new CommandWithReplyData<T>(commandData, replyTo);

            return new MoedeloEntityCommandKafkaMessageDefinition<CommandWithReplyData<T>>(
                topicName,
                entityType,
                commandKey,
                commandWithReplyData);
        }
        
        public Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<CommandWithReplyData<T>>> CreateCommandWithReplyDefinitionHandler<T>(
            string commandKey,
            T commandData)
            where T : IEntityCommandData
        {
            if (string.IsNullOrWhiteSpace(commandKey))
            {
                throw new ArgumentNullException(nameof(commandKey));
            }

            if (commandData == null)
            {
                throw new ArgumentNullException(nameof(commandData));
            }

            return replyTo =>
            {
                if (replyTo == null)
                {
                    throw new ArgumentNullException(nameof(replyTo));
                }

                var commandWithReplyData = new CommandWithReplyData<T>(commandData, replyTo);

                return new MoedeloEntityCommandKafkaMessageDefinition<CommandWithReplyData<T>>(
                    topicName,
                    entityType,
                    commandKey,
                    commandWithReplyData);
            };
        }
    }
}