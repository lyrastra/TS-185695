using Moedelo.Common.Enums.Enums.Tags;
using System.Collections.Generic;

namespace Moedelo.TagsV2.Client.Dtos
{
    public class EntityListRequestDto
    {
        /// <summary>
        /// Список числовых идентификаторов сущностей
        /// </summary>
        public IReadOnlyCollection<long> EntityIds { get; set; }

        /// <summary>
        /// Тип сущности
        /// </summary>
        public TagEntityType EntityType { get; set; }
    }
}
