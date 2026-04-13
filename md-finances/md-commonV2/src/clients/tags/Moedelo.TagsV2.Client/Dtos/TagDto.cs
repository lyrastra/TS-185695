using Moedelo.Common.Enums.Enums.Tags;

namespace Moedelo.TagsV2.Client.Dtos
{
    public class TagDto
    {
        /// <summary>
        /// Числовой идентификатор тега
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Числовой идентификатор сущности
        /// </summary>
        public long? EntityId { get; set; }

        /// <summary>
        /// Тип Сущности
        /// 1 - Контрагент
        /// </summary>
        public TagEntityType? EntityType { get; set; }

        /// <summary>
        /// Название тега
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Цвет тега
        /// </summary>
        public TagColor Color { get; set; }

        /// <summary>
        /// Является ли тег системным
        /// </summary>
        public bool IsSystem { get; set; }

        /// <summary>
        /// Количество использований тега
        /// </summary>
        public int UsageCount { get; set; }
    }
}
