using System;
using Moedelo.Changelog.Shared.Kafka.Abstractions;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApacheKafka;

namespace Moedelo.Changelog.Shared.Kafka.Framework.Commands
{
    /// <summary>
    /// Команда на сохранение явно описанных изменений по сущности
    /// </summary>
    public sealed class ExplicitChangesSaveCommand : IEntityCommandData
    {
        /// <summary>
        /// время создания записи
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// содержимое (поля) записи
        /// </summary>
        public ExplicitChangesSaveCommandFields Fields { get; set; }
    }
}
