using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Extensions;

namespace Moedelo.Common.Kafka.Abstractions.Base.MessageActionWrappers
{
    public sealed class LogMessageActionWrapper<TMessage> : IMessageActionWrapper<TMessage>
        where TMessage : MoedeloKafkaMessageValueBase
    {
        private readonly string topicName;
        private readonly ILogger logger;

        public LogMessageActionWrapper(string topicName, ILogger logger)
        {
            this.logger = logger.EnsureIsNotNull(nameof(logger));
            this.topicName = topicName.EnsureIsNotNullOrWhiteSpace(topicName);
        }

        public Func<TMessage, Task> Wrap(Func<TMessage, Task> onMessage)
        {
            return async message =>
            {
                LogBeforeOnMessage(message);
                await onMessage(message).ConfigureAwait(false);
                LogAfterOnMessage(message);
            };
        }

        private void LogBeforeOnMessage(TMessage message)
        {
            try
            {
                logger.LogMessageProcessingIsStarting(topicName, message.Metadata);
                logger.LogDumpProcessingMessage(topicName, message.Metadata, message);
            }
            catch
            {
                // ignore
            }
        }

        private void LogAfterOnMessage(TMessage message)
        {
            try
            {
                logger.LogMessageProcessingIsDone(topicName, message.Metadata);
            }
            catch
            {
                //ignore
            }
        }
    }
}