using System;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.Balances
{
    public class KontragentsReplaceInResultsDto
    {
        public IReadOnlyCollection<int> SourceKontragentIds { get; set; }
        public int TargetKontragentId { get; set; }
        public DateTime SinceDate { get; set; }
        public string TargetKontragentName { get; set; }
    }
}
