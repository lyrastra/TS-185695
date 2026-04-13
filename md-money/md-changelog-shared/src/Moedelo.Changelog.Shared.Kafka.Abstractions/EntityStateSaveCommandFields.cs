using System;
using Moedelo.Changelog.Shared.Domain.Enums;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Enums;

namespace Moedelo.Changelog.Shared.Kafka.Abstractions
{
    /// <summary>
    /// Поля команды "Сохранить состояние сущности в журнал изменений"
    /// </summary>
    public sealed class EntityStateSaveCommandFields
    {
        /// <summary>
        /// тип события: создано, обновлено, удалено
        /// </summary>
        public ChangeLogEventType EventType { get; set; }
        /// <summary>
        /// момент времени, когда были произведены изменения
        /// если не заполнен, то будет заполнен через DateTime.Now
        /// </summary>
        public DateTime? EventDateTime { get; set; }
        /// <summary>
        /// тип сущности
        /// </summary>
        public ChangeLogEntityType EntityType { get; set; }
        /// <summary>
        /// уникальный (в рамках типа сущности) идентификатор экземпляра сущности
        /// </summary>
        public long EntityId { get; set; }
        /// <summary>
        /// идентификатор фирмы, в контексте которой происходит событие
        /// </summary>
        public int FirmId { get; set; }
        /// <summary>
        /// идентификатор пользователя, автора изменений
        /// </summary>
        public int AuthorUserId { get; set; }
        /// <summary>
        /// состояние сущности в виде Json-строки
        /// </summary>
        public string EntityStateJson { get; set; }
    }
}
