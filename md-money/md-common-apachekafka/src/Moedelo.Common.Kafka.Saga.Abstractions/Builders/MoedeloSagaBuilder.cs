using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Kafka.Abstractions.Base;
using Moedelo.Common.Kafka.Abstractions.Base.MessageActionWrappers;
using Moedelo.Common.Kafka.Abstractions.Entities;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands.Replies;
using Moedelo.Common.Kafka.Abstractions.Settings;
using Moedelo.Common.Kafka.Saga.Abstractions.Exceptions;
using Moedelo.Common.Kafka.Saga.Abstractions.Internals;
using Moedelo.Infrastructure.Json;
using Moedelo.Infrastructure.Kafka.Abstractions.Models;

namespace Moedelo.Common.Kafka.Saga.Abstractions.Builders
{
    public abstract class MoedeloSagaBuilder : IMoedeloSagaBuilder, IMoedeloSaga
    {
        private readonly NotInitializedSagaStateData notInitializedSagaStateData = new NotInitializedSagaStateData();
        
        private readonly Dictionary<string, Func<string, MoedeloEntityCommandReplyKafkaMessageValue, Task>> replyHandlers = 
            new Dictionary<string, Func<string, MoedeloEntityCommandReplyKafkaMessageValue, Task>>();

        private bool isRunning;
        
        private readonly string topicName;
        private readonly string sagaType;
            
        private readonly IMoedeloSagaStateRepository sagaStateRepository;
        private readonly IMoedeloEntityCommandReplyKafkaTopicReader sagaKafkaTopicReader;
        private readonly IMoedeloEntityCommandKafkaTopicWriter commandKafkaTopicWriter;
        private readonly IMoedeloEntityCommandReplyKafkaTopicWriter commandReplyKafkaTopicWriter;
        private readonly IExecutionInfoContextInitializer contextInitializer;
        private readonly IExecutionInfoContextAccessor contextAccessor;
        private readonly IAuditTracer auditTracer;
        private readonly ILogger logger;

        private int? consumerCount;
        private OptionalReadSettings optionalReadSettings;

        private Func<UnexpectedSagaReplyDescription, Task<UnexpectedSagaReplyReaction>> unexpectedReplyHandler;

        protected MoedeloSagaBuilder(
            string topicName,
            string sagaType,
            IMoedeloSagaStateRepository moedeloSagaStateRepository, 
            IMoedeloEntityCommandReplyKafkaTopicReader sagaKafkaTopicReader,
            IMoedeloEntityCommandKafkaTopicWriter commandKafkaTopicWriter, 
            IMoedeloEntityCommandReplyKafkaTopicWriter commandReplyKafkaTopicWriter,
            IExecutionInfoContextInitializer contextInitializer, 
            IExecutionInfoContextAccessor contextAccessor,
            IAuditTracer auditTracer,
            ILogger logger = null)
        {
            if (string.IsNullOrWhiteSpace(topicName))
            {
                throw new ArgumentException(nameof(topicName));
            }
            
            if (string.IsNullOrWhiteSpace(sagaType))
            {
                throw new ArgumentException(nameof(sagaType));
            }

            this.topicName = topicName;
            this.sagaType = sagaType;
            
            this.sagaStateRepository = moedeloSagaStateRepository ?? throw new ArgumentException(nameof(moedeloSagaStateRepository));
            this.sagaKafkaTopicReader = sagaKafkaTopicReader ?? throw new ArgumentException(nameof(sagaKafkaTopicReader));
            this.commandKafkaTopicWriter = commandKafkaTopicWriter ?? throw new ArgumentException(nameof(commandKafkaTopicWriter));
            this.commandReplyKafkaTopicWriter = commandReplyKafkaTopicWriter ?? throw new ArgumentException(nameof(commandReplyKafkaTopicWriter));
            this.contextInitializer = contextInitializer ?? throw new ArgumentException(nameof(contextInitializer));
            this.contextAccessor = contextAccessor ?? throw new ArgumentException(nameof(contextAccessor));
            this.auditTracer = auditTracer ?? throw new ArgumentException(nameof(auditTracer));
            this.logger = logger;
        }

        public IMoedeloSagaBuilder OnStart<TInitStateData, TCommand>(
            Func<TInitStateData, Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>> createDescriptionHandler,
            Func<TInitStateData, Task> onBeforeInitStateSaveCallback = null) 
            where TInitStateData : ISagaStateData
            where TCommand : IEntityCommandData
        {
            EnsureIsNotRunningYet();
            
            IReadOnlyCollection<Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>> CreateDescriptionsHandlers(TInitStateData initStateData)
            {
                var descriptionHandler = createDescriptionHandler(initStateData);
                var descriptionsHandlers = new[] {descriptionHandler};

                return descriptionsHandlers;
            }

            return OnStart(CreateDescriptionsHandlers, onBeforeInitStateSaveCallback);
        }
        
        public IMoedeloSagaBuilder OnStart<TInitStateData, TCommand>(
            Func<TInitStateData, IReadOnlyCollection<Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>>> createDescriptionsHandlers,
            Func<TInitStateData, Task> onBeforeInitStateSaveCallback = null) 
            where TInitStateData : ISagaStateData
            where TCommand : IEntityCommandData
        {
            EnsureIsNotRunningYet();

            static TInitStateData Map(NotInitializedSagaStateData _, SagaInitReplyData<TInitStateData> replyData)
            {
                return replyData.Data;
            }
            
            return OnReply<NotInitializedSagaStateData, SagaInitReplyData<TInitStateData>, TInitStateData, TCommand>(Map, createDescriptionsHandlers);
        }

        public IMoedeloSagaBuilder OnSuccessReply<TStateData, TReplyData, TNewStateData, TCommand>(
            Func<TStateData, TReplyData, TNewStateData> mapState,
            Func<TNewStateData, Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>> createDescriptionHandler,
            Func<TNewStateData, Task> onBeforeNewStateSaveCallback = null)
            where TStateData : ISagaStateData 
            where TNewStateData : ISagaStateData
            where TCommand : IEntityCommandData
        {
            EnsureIsNotRunningYet();
            
            TNewStateData Map(TStateData stateData, EntityCommandSuccessReplyData<TReplyData> replyData)
            {
                return mapState(stateData, replyData.Data);
            }
            
            return OnReply<TStateData, EntityCommandSuccessReplyData<TReplyData>, TNewStateData, TCommand>(Map, createDescriptionHandler);
        }

        public IMoedeloSagaBuilder OnSuccessReply<TStateData, TReplyData, TNewStateData, TCommand>(
            Func<TStateData, TReplyData, TNewStateData> mapState,
            Func<TNewStateData, IReadOnlyCollection<Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>>> createDescriptionsHandlers,
            Func<TNewStateData, Task> onBeforeNewStateSaveCallback = null)
            where TStateData : ISagaStateData 
            where TNewStateData : ISagaStateData
            where TCommand : IEntityCommandData
        {
            EnsureIsNotRunningYet();
            
            TNewStateData Map(TStateData stateData, EntityCommandSuccessReplyData<TReplyData> replyData)
            {
                return mapState(stateData, replyData.Data);
            }
            
            return OnReply<TStateData, EntityCommandSuccessReplyData<TReplyData>, TNewStateData, TCommand>(
                Map, createDescriptionsHandlers, onBeforeNewStateSaveCallback);
        }
        
        public IMoedeloSagaBuilder OnSuccessReply<TStateData, TNewStateData, TCommand>(
            Func<TStateData, TNewStateData> mapState,
            Func<TNewStateData, Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>> createDescriptionHandler,
            Func<TNewStateData, Task> onBeforeNewStateSaveCallback = null)
            where TStateData : ISagaStateData
            where TNewStateData : ISagaStateData
            where TCommand : IEntityCommandData
        {
            EnsureIsNotRunningYet();
            
            TNewStateData Map(TStateData stateData, EntityCommandSuccessReplyData replyData)
            {
                return mapState(stateData);
            }

            return OnReply<TStateData, EntityCommandSuccessReplyData, TNewStateData, TCommand>(
                Map, createDescriptionHandler, onBeforeNewStateSaveCallback);
        }

        public IMoedeloSagaBuilder OnSuccessReply<TStateData, TNewStateData, TCommand>(
            Func<TStateData, TNewStateData> mapState,
            Func<TNewStateData, IReadOnlyCollection<Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>>> createDescriptionsHandlers,
            Func<TNewStateData, Task> onBeforeNewStateSaveCallback = null)
            where TStateData : ISagaStateData
            where TNewStateData : ISagaStateData
            where TCommand : IEntityCommandData
        {
            EnsureIsNotRunningYet();
            
            TNewStateData Map(TStateData stateData, EntityCommandSuccessReplyData replyData)
            {
                return mapState(stateData);
            }

            return OnReply<TStateData, EntityCommandSuccessReplyData, TNewStateData, TCommand>(
                Map, createDescriptionsHandlers, onBeforeNewStateSaveCallback);
        }
        
        public IMoedeloSagaBuilder OnErrorReply<TStateData, TReplyData, TNewStateData, TCommand>(
            Func<TStateData, TReplyData, TNewStateData> mapState,
            Func<TNewStateData, Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>> createDescriptionHandler,
            Func<TNewStateData, Task> onBeforeNewStateSaveCallback = null) 
            where TStateData : ISagaStateData 
            where TNewStateData : ISagaStateData
            where TCommand : IEntityCommandData
        {
            EnsureIsNotRunningYet();
            
            TNewStateData Map(TStateData stateData, EntityCommandErrorReplyData<TReplyData> replyData)
            {
                return mapState(stateData, replyData.Data);
            }
            
            return OnReply<TStateData, EntityCommandErrorReplyData<TReplyData>, TNewStateData, TCommand>(
                Map, createDescriptionHandler, onBeforeNewStateSaveCallback);
        }

        public IMoedeloSagaBuilder OnErrorReply<TStateData, TReplyData, TNewStateData, TCommand>(
            Func<TStateData, TReplyData, TNewStateData> mapState,
            Func<TNewStateData, IReadOnlyCollection<Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>>> createDescriptionsHandlers,
            Func<TNewStateData, Task> onBeforeNewStateSaveCallback = null) 
            where TStateData : ISagaStateData 
            where TNewStateData : ISagaStateData
            where TCommand : IEntityCommandData
        {
            EnsureIsNotRunningYet();
            
            TNewStateData Map(TStateData stateData, EntityCommandErrorReplyData<TReplyData> replyData)
            {
                return mapState(stateData, replyData.Data);
            }
            
            return OnReply<TStateData, EntityCommandErrorReplyData<TReplyData>, TNewStateData, TCommand>(
                Map, createDescriptionsHandlers, onBeforeNewStateSaveCallback);
        }
        
        public IMoedeloSagaBuilder OnErrorReply<TStateData, TNewStateData, TCommand>(
            Func<TStateData, TNewStateData> mapState,
            Func<TNewStateData, Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>> createDescriptionHandler,
            Func<TNewStateData, Task> onBeforeNewStateSaveCallback = null) 
            where TStateData : ISagaStateData 
            where TNewStateData : ISagaStateData
            where TCommand : IEntityCommandData
        {
            EnsureIsNotRunningYet();
            
            TNewStateData Map(TStateData stateData, EntityCommandErrorReplyData replyData)
            {
                return mapState(stateData);
            }

            return OnReply<TStateData, EntityCommandErrorReplyData, TNewStateData, TCommand>(
                Map, createDescriptionHandler, onBeforeNewStateSaveCallback);
        }

        public IMoedeloSagaBuilder OnErrorReply<TStateData, TNewStateData, TCommand>(
            Func<TStateData, TNewStateData> mapState,
            Func<TNewStateData, IReadOnlyCollection<Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>>> createDescriptionsHandlers,
            Func<TNewStateData, Task> onBeforeNewStateSaveCallback = null) 
            where TStateData : ISagaStateData 
            where TNewStateData : ISagaStateData
            where TCommand : IEntityCommandData
        {
            EnsureIsNotRunningYet();
            
            TNewStateData Map(TStateData stateData, EntityCommandErrorReplyData replyData)
            {
                return mapState(stateData);
            }

            return OnReply<TStateData, EntityCommandErrorReplyData, TNewStateData, TCommand>(
                Map, createDescriptionsHandlers, onBeforeNewStateSaveCallback);
        }

        private IMoedeloSagaBuilder OnReply<TStateData, TReplyData, TNewStateData, TCommand>(
            Func<TStateData, TReplyData, TNewStateData> mapState,
            Func<TNewStateData, Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>> createDescriptionHandler,
            Func<TNewStateData, Task> onBeforeNewStateSaveCallback = null)
            where TStateData : ISagaStateData
            where TReplyData : IEntityCommandReplyData
            where TNewStateData : ISagaStateData
            where TCommand : IEntityCommandData
        {
            EnsureIsNotRunningYet();
            
            IReadOnlyCollection<Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>> CreateDescriptionsHandlers(TNewStateData newStateData)
            {
                var descriptionHandler = createDescriptionHandler(newStateData);
                var descriptionsHandlers = new[] {descriptionHandler};

                return descriptionsHandlers;
            }

            return OnReply(mapState, CreateDescriptionsHandlers, onBeforeNewStateSaveCallback);
        }
         
        private IMoedeloSagaBuilder OnReply<TStateData, TReplyData, TNewStateData, TCommand>(
            Func<TStateData, TReplyData, TNewStateData> mapState,
            Func<TNewStateData, IReadOnlyCollection<Func<ReplyTo, IMoedeloEntityCommandKafkaMessageDefinition<TCommand>>>> createDescriptionsHandlers,
            Func<TNewStateData, Task> onBeforeNewStateSaveCallback = null)
            where TStateData : ISagaStateData
            where TReplyData : IEntityCommandReplyData
            where TNewStateData : ISagaStateData
            where TCommand : IEntityCommandData
        {
            EnsureIsNotRunningYet();
            
            async Task Reply(string stateDataJson, MoedeloEntityCommandReplyKafkaMessageValue replyMessageValue)
            {
                var stateData = stateDataJson.FromJsonString<TStateData>();
                var replyData = replyMessageValue.ReplyData.FromJsonString<TReplyData>();
                var newStateData = mapState(stateData, replyData);
                var sagaId = replyMessageValue.Sender.SenderId;
                var newStateDataType = SagaStateTypeMapper.GetStateType(newStateData);
                var newStateDataJson = newStateData.ToJsonString();
                var sagaNewStateData = new MoedeloSagaNewStateData
                {
                    SagaId = sagaId,
                    StateType = newStateDataType,
                    StateData = newStateDataJson,
                };

                if (onBeforeNewStateSaveCallback != null)
                {
                    await onBeforeNewStateSaveCallback(newStateData).ConfigureAwait(false);
                }

                await sagaStateRepository.UpdateAsync(sagaNewStateData).ConfigureAwait(false);
                var replyTo = ReplyTo.Create(sagaId, sagaType, topicName);
                var commandsDescriptionsHandlers = createDescriptionsHandlers(newStateData);
                var sendCommandsTasks = commandsDescriptionsHandlers.Select(h => 
                    commandKafkaTopicWriter.WriteCommandDataAsync(h(replyTo))).ToArray();
                await Task.WhenAll(sendCommandsTasks).ConfigureAwait(false);
            }

            var stateType = SagaStateTypeMapper.GetStateType<TStateData>();
            var replyType = EntityCommandReplyTypeMapper.GetCommandReplyType<TReplyData>();
            var replyHandlerKey = GetReplyHandlerKey(stateType, replyType);
            replyHandlers.Add(replyHandlerKey, Reply);

            return this;
        }

        public IMoedeloSagaBuilder OnUnexpectedReply(UnexpectedSagaReplyReaction reaction)
        {
            EnsureIsNotRunningYet();
            unexpectedReplyHandler = _ => Task.FromResult(reaction);

            return this;
        }


        public IMoedeloSagaBuilder OnUnexpectedReply(
            Func<UnexpectedSagaReplyDescription, Task<UnexpectedSagaReplyReaction>> handler)
        {
            EnsureIsNotRunningYet();
            unexpectedReplyHandler = handler;

            return this;
        }
        
        public async Task StartAsync<TInitStateData>(TInitStateData initStateData)
            where TInitStateData : ISagaStateData
        {
            var sagaId = Guid.NewGuid();
            var sagaNotInitializedStateData = new MoedeloSagaInitStateData
            {
                SagaId = sagaId,
                StateType = SagaStateTypeMapper.GetStateType(notInitializedSagaStateData),
                StateData = notInitializedSagaStateData.ToJsonString(),
                ExecutionContextToken = contextAccessor.ExecutionInfoContextToken,
            };
            await sagaStateRepository.SaveNewAsync(sagaNotInitializedStateData).ConfigureAwait(false);
            var replyDefinition =
                new MoedeloSagaInitReplyKafkaMessageDefinition<TInitStateData>(
                    sagaId,
                    topicName,
                    sagaType,
                    initStateData);
            await commandReplyKafkaTopicWriter.WriteCommandReplyDataAsync(replyDefinition).ConfigureAwait(false);
        }

        public IMoedeloSaga WithConsumerCount(int count)
        {
            EnsureIsNotRunningYet();

            if (count < 1)
            {
                throw new ArgumentException("Value must be positive", nameof(count));
            }

            consumerCount = count;

            return this;
        }

        public IMoedeloSaga WithAutoConsumersCount()
        {
            EnsureIsNotRunningYet();
            consumerCount = null;

            return this;
        }

        public IMoedeloSaga WithOptionalSettings(OptionalReadSettings settings)
        {
            EnsureIsNotRunningYet();
            optionalReadSettings = settings;
            
            return this;
        }

        public Task RunAsync(string groupId, CancellationToken cancellationToken)
        {
            EnsureIsNotRunningYet();
            isRunning = true;

            if (string.IsNullOrWhiteSpace(groupId))
            {
                throw new ArgumentException(nameof(groupId));
            }

            var logWrapper = new LogMessageActionWrapper<MoedeloEntityCommandReplyKafkaMessageValue>(topicName, logger);
            var auditWrapper = new AuditMessageActionWrapper<MoedeloEntityCommandReplyKafkaMessageValue>(topicName, auditTracer);

            var readTopicSettings = new ReadTopicSetting<MoedeloEntityCommandReplyKafkaMessageValue>(
                groupId,
                topicName,
                BuildOnReplyMessageFunc().WrapByIf(logWrapper, logger != null).WrapBy(auditWrapper),
                null,
                null,
                KafkaConsumerConfig.AutoOffsetResetType.Earliest,
                consumerCount);

            SetOptionalSettings(readTopicSettings);

            return sagaKafkaTopicReader.ReadCommandReplyTopicAsync(readTopicSettings, cancellationToken);
        }
        
        private void SetOptionalSettings(ReadTopicSetting<MoedeloEntityCommandReplyKafkaMessageValue> readTopicSettings)
        {
            if (optionalReadSettings == null)
            {
                return;
            }

            if (optionalReadSettings.FetchWaitMaxMs.HasValue)
            {
                readTopicSettings.FetchWaitMaxMs = optionalReadSettings.FetchWaitMaxMs.Value;
            }

            if (optionalReadSettings.FetchErrorBackoffMs.HasValue)
            {
                readTopicSettings.FetchErrorBackoffMs = optionalReadSettings.FetchErrorBackoffMs.Value;
            }
            
            if (optionalReadSettings.FetchMinBytes.HasValue)
            {
                readTopicSettings.FetchMinBytes = optionalReadSettings.FetchMinBytes.Value;
            }
            
            if (optionalReadSettings.FetchMaxBytes.HasValue)
            {
                readTopicSettings.FetchMaxBytes = optionalReadSettings.FetchMaxBytes.Value;
            }
            
            if (optionalReadSettings.QueuedMinMessages.HasValue)
            {
                readTopicSettings.QueuedMinMessages = optionalReadSettings.QueuedMinMessages.Value;
            }
            
            if (optionalReadSettings.SessionTimeoutMs.HasValue)
            {
                readTopicSettings.SessionTimeoutMs = optionalReadSettings.SessionTimeoutMs.Value;
            }
        }

        private Func<MoedeloEntityCommandReplyKafkaMessageValue, Task> BuildOnReplyMessageFunc()
        {
            async Task OnReplyMessage(MoedeloEntityCommandReplyKafkaMessageValue replyMessageValue)
            {
                if (replyMessageValue.Sender.SenderType != sagaType)
                {
                    throw new Exception("messageValue.Sender.SenderType != sagaType");
                }

                var sagaId = replyMessageValue.Sender.SenderId;
                var sagaStateData = await sagaStateRepository.GetAsync(sagaId).ConfigureAwait(false);
                var currentStateType = sagaStateData.StateType;
                var currentStateData = sagaStateData.StateData;
                var replyType = replyMessageValue.ReplyType;
                var replyHandlerKey = GetReplyHandlerKey(currentStateType, replyType);

                if (replyHandlers.ContainsKey(replyHandlerKey) == false)
                {
                    await HandleUnexpectedReplyAsync(
                        currentStateType,
                        replyType,
                        currentStateData,
                        replyMessageValue).ConfigureAwait(false);

                    return;
                }

                var executionContextToken = sagaStateData.ExecutionContextToken;
                var handler = replyHandlers[replyHandlerKey];

                if (string.IsNullOrWhiteSpace(executionContextToken))
                {
                    await handler(currentStateData, replyMessageValue).ConfigureAwait(false);
                }
                else
                {
                    var executionInfoContext = contextInitializer.Initialize(executionContextToken);

                    using (contextAccessor.SetContext(executionContextToken, executionInfoContext))
                    {
                        await handler(currentStateData, replyMessageValue).ConfigureAwait(false);
                    }
                }
            }

            return OnReplyMessage;
        }

        private async Task HandleUnexpectedReplyAsync(
            string currentStateType,
            string replyType,
            string currentStateData,
            MoedeloEntityCommandReplyKafkaMessageValue replyMessageValue)
        {
            if (unexpectedReplyHandler != null)
            {
                var description = new UnexpectedSagaReplyDescription(
                    currentStateType, replyType, currentStateData, replyMessageValue);

                var reaction = await unexpectedReplyHandler(description).ConfigureAwait(false);

                if (reaction == UnexpectedSagaReplyReaction.SilentIgnore)
                {
                    return;
                }

                if (reaction == UnexpectedSagaReplyReaction.LogErrorAndIgnore)
                {
                    logger.LogError($"Получен неожиданный ответ: {description.ToJsonString()}");

                    return;
                }
            }

            throw new UnexpectedStateReplyException(currentStateType, replyType);
        }

        private static string GetReplyHandlerKey(string stateType, string replyType)
        {
            return $"{stateType}_:_{replyType}";
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureIsNotRunningYet()
        {
            if (isRunning)
            {
                throw new InvalidOperationException($"{GetType().Name} is already running");
            }
        }
    }
}