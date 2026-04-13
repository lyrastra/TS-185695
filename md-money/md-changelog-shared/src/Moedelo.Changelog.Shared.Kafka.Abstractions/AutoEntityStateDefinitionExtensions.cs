using System;
using Moedelo.Changelog.Shared.Domain.Definitions;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Enums;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Changelog.Shared.Kafka.Abstractions
{
    public static class AutoEntityStateDefinitionExtensions
    {
        public static EntityStateSaveCommandFields CreateEntityStateSaveCommandFields<TDefinition, TState>(
            this AutoEntityStateDefinition<TDefinition, TState> stateDefinition,
            ChangeLogEventType eventType,
            int firmId,
            int authorUserId,
            DateTime? changesTimestamp,
            TState state) where TDefinition : AutoEntityStateDefinition<TDefinition, TState>, new()
        {
            return new EntityStateSaveCommandFields
            {
                EntityId = stateDefinition.GetEntityId(state),
                EntityType = stateDefinition.EntityType,
                EventType = eventType,
                FirmId = firmId,
                AuthorUserId = authorUserId,
                EventDateTime = changesTimestamp,
                EntityStateJson = state.ToJsonString()
            };
        }
    }
}
