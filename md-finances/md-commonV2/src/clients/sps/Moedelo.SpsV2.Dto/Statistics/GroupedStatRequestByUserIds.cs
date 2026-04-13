using System;
using System.Collections.Generic;

namespace Moedelo.SpsV2.Dto.Statistics
{
    public class GroupedStatRequestByUserIds
    {
        public List<int> UserIds { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
