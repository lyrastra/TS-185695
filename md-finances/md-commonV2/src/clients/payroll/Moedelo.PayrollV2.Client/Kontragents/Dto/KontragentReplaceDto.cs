using System;
using System.Collections.Generic;

namespace Moedelo.PayrollV2.Client.Kontragents.Dto
{
    public class KontragentReplaceDto
    {
        public List<int> SourceKontragentIds { get; set; }
        public int TargetKontragentId { get; set; }
        public DateTime StartDate { get; set; }
    }
}
