using System.Collections.Generic;
using Moedelo.Changelog.Shared.Domain.Extensions;
using Moedelo.Infrastructure.System.Extensions.Enums;
using Newtonsoft.Json;

namespace Moedelo.Changelog.Shared.Domain.Definitions
{
    public abstract class AutoEntityStateDefinition<TDefinition, TState> : EntityStateDefinition
        where TDefinition : AutoEntityStateDefinition<TDefinition, TState>, new()
    {
        protected AutoEntityStateDefinition()
        {
            Fields = typeof(TState).ToFieldDefinitions();
        }

        public static readonly TDefinition Instance = new TDefinition();

        public override string GetEntityName(string entityStateJson)
        {
            var state = JsonConvert.DeserializeObject<TState>(entityStateJson, JsonSerializationSettingsFactory.SkipErrorSettings);

            return GetEntityName(state);
        }

        public override string EntityTypeName => EntityType.GetDescription();

        public abstract long GetEntityId(TState state);
        protected abstract string GetEntityName(TState entityState);
        public sealed override IReadOnlyDictionary<string, EntityFieldDefinition> Fields { get; }

        public sealed override ISet<string> Tags { get; } = new HashSet<string>();

        protected void AddTag(string tag)
        {
            Tags.Add(tag);
        }
    }
}
