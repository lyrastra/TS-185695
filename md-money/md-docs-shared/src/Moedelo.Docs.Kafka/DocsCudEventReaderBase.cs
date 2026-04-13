using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Docs.Kafka.Abstractions;
using Moedelo.Docs.Kafka.Abstractions.Sales.SignStatusChanged.Events;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Docs.Kafka
{
    /// <summary>
    /// Базовый класс для чтения CUD-событий от документов
    /// </summary>
    /// <typeparam name="TCreatedMsg">Сообщение о создании документа</typeparam>
    /// <typeparam name="TUpdatedMsg">Сообщение об изменении документа</typeparam>
    /// <typeparam name="TDeletedMsg">Сообщение об удалении</typeparam>
    public abstract class DocsCudEventReaderBase<TCreatedMsg, TUpdatedMsg, TDeletedMsg> : MoedeloKafkaTopicReaderBase
    {
        private readonly ILogger _logger;
        protected abstract string TopicName { get; }
        
        protected DocsCudEventReaderBase(
            IMoedeloKafkaTopicReaderBaseDependencies dependencies, 
            ILogger logger)
            : base(
                dependencies, 
                logger)
        {
            _logger = logger;
        }

        public Task ReadAsync(
            string groupId,
            Func<TCreatedMsg, KafkaMessageValueMetadata, Task> onCreate,
            Func<TUpdatedMsg, KafkaMessageValueMetadata, Task> onUpdate,
            Func<TDeletedMsg, KafkaMessageValueMetadata, Task> onDelete,
            Func<SignStatusChangedMessage, KafkaMessageValueMetadata, Task> onSignStatusUpdate,
            Func<DocsCudEventMessageValue, Exception, Task> onException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null)
        {
            return CommonReadAsync(groupId,onCreate,onUpdate,onDelete, onSignStatusUpdate, onException, resetType, consumerCount,cancellationToken);
        }

        public Task ReadAsync(
            string groupId,
            Func<TCreatedMsg, KafkaMessageValueMetadata, Task> onCreate,
            Func<TUpdatedMsg, KafkaMessageValueMetadata, Task> onUpdate,
            Func<TDeletedMsg, KafkaMessageValueMetadata, Task> onDelete,
            Func<DocsCudEventMessageValue, Exception, Task> onException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest,
            int consumerCount = 1,
            CancellationToken? cancellationToken = null)
        {
            return CommonReadAsync(groupId,onCreate,onUpdate,onDelete, null, onException, resetType, consumerCount,cancellationToken);
        }

        private async Task CommonReadAsync(
            string groupId, 
            Func<TCreatedMsg, KafkaMessageValueMetadata, Task> onCreate, 
            Func<TUpdatedMsg, KafkaMessageValueMetadata, Task> onUpdate, 
            Func<TDeletedMsg, KafkaMessageValueMetadata, Task> onDelete,
            Func<SignStatusChangedMessage, KafkaMessageValueMetadata, Task> onSignStatusUpdate = null,
            Func<DocsCudEventMessageValue, Exception, Task> onException = null,
            KafkaConsumerConfig.AutoOffsetResetType resetType = KafkaConsumerConfig.AutoOffsetResetType.Latest, 
            int consumerCount = 1,
            CancellationToken? cancellationToken = null)
        {
            ValidateHandlerFunc(onCreate, nameof(onCreate));
            ValidateHandlerFunc(onUpdate, nameof(onUpdate));
            ValidateHandlerFunc(onDelete, nameof(onDelete));

            Task OnMessageAction(DocsCudEventMessageValue messageValue)
            {
                var metadata = messageValue.Metadata;
                switch (messageValue.EventType)
                {
                    case DocsCudEventType.Created:
                        var createdMessage = messageValue.EventDataJson.FromJsonString<TCreatedMsg>();
                        return onCreate(createdMessage, metadata);
                    case DocsCudEventType.Updated:
                        var updatedMessage = messageValue.EventDataJson.FromJsonString<TUpdatedMsg>();
                        return onUpdate(updatedMessage, metadata);
                    case DocsCudEventType.Deleted:
                        var deletedMessage = messageValue.EventDataJson.FromJsonString<TDeletedMsg>();
                        return onDelete(deletedMessage, metadata);
                    case DocsCudEventType.SignStatusUpdated:
                    {
                        if (onSignStatusUpdate == null)
                        {
                            return Task.CompletedTask;
                        }

                        var signStatusUpdatedMessage = messageValue.EventDataJson.FromJsonString<SignStatusChangedMessage>();
                        return onSignStatusUpdate(signStatusUpdatedMessage, metadata);
                    }
                    default:
                        throw new ArgumentOutOfRangeException($"Not supported eventType: {messageValue.EventType}.");
                }
            }

            var settings = new ReadTopicSetting<DocsCudEventMessageValue>
            (groupId, 
                TopicName, 
                OnMessageAction, 
                onException,
                onFatalException:null,
                resetType: resetType, 
                consumersCount: consumerCount);
            try
            {
                await ReadTopicWithRetryAsync(settings, cancellationToken ?? CancellationToken.None);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error DocsCud read message from kafka {e.Message}");
            }
        }
    }
}