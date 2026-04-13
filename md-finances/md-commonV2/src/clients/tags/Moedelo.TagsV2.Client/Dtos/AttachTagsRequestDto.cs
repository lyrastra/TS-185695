using Moedelo.Common.Enums.Enums.Tags;
using System.Collections.Generic;

namespace Moedelo.TagsV2.Client.Dtos
{
    public class AttachTagsRequestDto
    {
        /// <summary>
        /// Числовые идентификаторы тегов
        /// </summary>
        public IReadOnlyCollection<long> TagIds { get; set; }

        /// <summary>
        /// Числовые идентификаторы сущностей
        /// </summary>
        public IReadOnlyCollection<long> EntityIds { get; set; }

        /// <summary>
        /// Тип сущностей
        /// </summary>
        public TagEntityType EntityType { get; set; }
    }
}
