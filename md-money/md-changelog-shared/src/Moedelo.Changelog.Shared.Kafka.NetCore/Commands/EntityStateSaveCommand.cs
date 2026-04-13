using System;
using Moedelo.Changelog.Shared.Kafka.Abstractions;
using Moedelo.Common.Kafka.Abstractions.Entities.Commands;

namespace Moedelo.Changelog.Shared.Kafka.NetCore.Commands
{
    public sealed class EntityStateSaveCommand : IEntityCommandData
    {
        /// <summary>
        /// время создания записи
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// содержимое (поля) записи
        /// </summary>
        public EntityStateSaveCommandFields Fields { get; set; }
    }
}
