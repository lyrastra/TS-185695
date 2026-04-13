using System.Collections.Generic;

namespace Moedelo.TagsV2.Client.Dtos
{
    public class GetForUnionKontragentsRequestDto
    {
        public int BaseKontragentId { get; set; }

        public IReadOnlyCollection<int> MergedKontragentIds{ get; set; }
    }
}
