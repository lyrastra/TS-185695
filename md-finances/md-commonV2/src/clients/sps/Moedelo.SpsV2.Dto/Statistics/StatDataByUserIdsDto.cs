using System;
using System.Collections.Generic;

namespace Moedelo.SpsV2.Dto.Statistics
{
    public class StatDataByUserIdsDto
    {
        public int UserId { get; set; }

        public DateTime LastOperationDate { get; set; }

        public List<StatDataGroupDto> Data { get; set; }
    }
}
