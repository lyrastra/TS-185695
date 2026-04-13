using System;
using System.Threading.Tasks;
using Moedelo.Changelog.Shared.Domain;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Kafka.Abstractions;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Enums;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Extensions;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Writers;
using Moedelo.Common.ExecutionContext.Abstractions.Models;

namespace Moedelo.Money.ChangeLog.HostedServices
{
    public static class ChangeLogCommandWriterExtensions
    {
        public static Task WriteAsync<TDefinition, TState>(
            this IChangeLogCommandWriter commandWriter,
            ChangeLogEventType eventType,
            AutoEntityStateDefinition<TDefinition, TState> stateDefinition,
            ExecutionInfoContext userContext,
            TState entityState,
            DateTime changesDateTime) where TDefinition : AutoEntityStateDefinition<TDefinition, TState>, new()
        {
            var firmId = (int)userContext.FirmId;
            var authorUserId = (int)userContext.UserId;

            var commandFields = stateDefinition
                .CreateEntityStateSaveCommandFields(
                    eventType,
                    firmId,
                    authorUserId,
                    changesDateTime,
                    entityState);

            return commandWriter.WriteAsync(commandFields);
        }
        
        public static Task WriteAsync(this IChangeLogCommandWriter commandWriter,
            ExecutionInfoContext userContext,
            EntityStateDefinition stateDef,
            DateTime changesDateTime,
            long documentBaseId,
            params (string name, object value)[] fieldStates)
        {
            var firmId = (int)userContext.FirmId;
            var authorUserId = (int)userContext.UserId;

            var changedFields = new ExplicitChangesSaveCommandFields
            {
                EventType = ChangeLogEventType.Updated,
                EntityId = documentBaseId,
                EntityType = stateDef.EntityType,
                FirmId = firmId,
                AuthorUserId = authorUserId,
                EventDateTime = changesDateTime,
                FieldChanges = fieldStates.ToExplicitFieldChanges(stateDef)
            };

            return commandWriter.WriteAsync(changedFields);
        }
    }
}
