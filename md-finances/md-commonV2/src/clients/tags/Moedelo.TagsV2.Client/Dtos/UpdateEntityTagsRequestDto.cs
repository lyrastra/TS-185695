using Moedelo.Common.Enums.Enums.Tags;
using System.Collections.Generic;

namespace Moedelo.TagsV2.Client.Dtos
{
    public class UpdateEntityTagsRequestDto
    {
        public IReadOnlyCollection<long> TagIds { get; set; }
        public long EntityId { get; set; }
        public TagEntityType EntityType { get; set; }
    }
}
