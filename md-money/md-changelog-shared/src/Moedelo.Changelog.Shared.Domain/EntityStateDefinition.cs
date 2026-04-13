using System.Collections.Generic;
using Moedelo.Changelog.Shared.Domain.Enums;
using Moedelo.Common.AccessRules.Abstractions;

namespace Moedelo.Changelog.Shared.Domain
{
    /// <summary>
    /// Определение схемы состояния сущности
    /// </summary>
    public abstract class EntityStateDefinition
    {
        /// <summary>
        /// Права, требуемые для чтения журнала изменений сущностей такого типа
        /// </summary>
        public abstract IReadOnlyCollection<AccessRule> RequiredReadPermissions { get; }
        /// <summary>
        /// тип сущности <see cref="ChangeLogEntityType"/>
        /// </summary>
        public abstract ChangeLogEntityType EntityType { get; }
        /// <summary>
        /// список тэгов. Тэги позволяют объединять типы сущности в группы для целей поиска
        /// </summary>
        public abstract ISet<string> Tags { get; }
        /// <summary>
        /// определения полей (лучше не заполнять руками, наследуюйся от <see cref="Moedelo.Changelog.Shared.Domain.Definitions.AutoEntityStateDefinition"/> для автоматического заполнения)
        /// </summary>
        public abstract IReadOnlyDictionary<string, EntityFieldDefinition> Fields { get; }
        /// <summary>
        /// Получить название сущности из его состояния (в виде json строки)
        /// </summary>
        /// <param name="entityStateJson"></param>
        /// <returns></returns>
        public abstract string GetEntityName(string entityStateJson);
        /// <summary>
        /// Название типа сущности
        /// </summary>
        public abstract string EntityTypeName { get; }
    }
}
