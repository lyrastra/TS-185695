using System;
using System.Collections.Generic;
using Moedelo.Changelog.Shared.Domain.Enums;
using Moedelo.Changelog.Shared.Kafka.Abstractions.Enums;

namespace Moedelo.Changelog.Shared.Kafka.Abstractions
{
    /// <summary>
    /// Поля команды "Сохранить явно заданные изменения для сущности в журнал изменений"
    /// </summary>
    public sealed class ExplicitChangesSaveCommandFields
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
        /// данные об изменённых полях
        /// </summary>
        public IReadOnlyCollection<FieldChange> FieldChanges{ get; set; }

        public struct FieldChange
        {
            public string FieldName { get; set; }
            public string NewValue { get; set; }
        }
    }
}
