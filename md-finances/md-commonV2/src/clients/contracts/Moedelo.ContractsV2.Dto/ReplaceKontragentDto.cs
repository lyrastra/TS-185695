using System;
using System.Collections.Generic;

namespace Moedelo.ContractsV2.Dto
{
    public class ReplaceKontragentDto
    {
        public int NewKontragentId { get; set; }
        public List<int> OldKontragentIds { get; set; }
        public DateTime StartDate { get; set; }
    }
}
