using System.Collections.Generic;

namespace Moedelo.Docs.Dto.Ukd
{
    public class UkdCriteriaResponseDto
    {
        public IList<UkdCriteriaTableItemDto> Data { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int TotalCount { get; set; }
    }
}