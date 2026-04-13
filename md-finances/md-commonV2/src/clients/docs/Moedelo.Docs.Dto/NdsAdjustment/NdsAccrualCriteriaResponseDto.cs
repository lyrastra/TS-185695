using System;
using System.Collections.Generic;

namespace Moedelo.Docs.Dto.NdsAdjustment
{
    public class NdsAccrualCriteriaResponseDto
    {
        public List<NdsAccrualItemDto> Data { get; set; }
        public int? Quarter { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int Year { get; set; }
        public int Offset { get; set; }
        public int Limit { get; set; }
        public int TotalCount { get; set; }
    }
}