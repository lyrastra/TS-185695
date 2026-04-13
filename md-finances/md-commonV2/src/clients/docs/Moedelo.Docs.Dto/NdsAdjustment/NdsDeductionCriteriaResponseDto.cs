using System.Collections.Generic;

namespace Moedelo.Docs.Dto.NdsAdjustment
{
    public class NdsDeductionCriteriaResponseDto
    {
        public List<NdsDeductionItemDto> Data { get; set; }
        public decimal? TotalNdsDeductionAvailable { get; set; }
        public bool IsPayable { get; set; }
        public int Quarter { get; set; }
        public int Year { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int TotalCount { get; set; }
    }
}