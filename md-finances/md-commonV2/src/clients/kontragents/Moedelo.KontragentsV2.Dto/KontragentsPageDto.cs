using System.Collections.Generic;

namespace Moedelo.KontragentsV2.Dto
{
    public class KontragentsPageDto
    {
        public int TotalCount { get; set; } 
        public uint PageNumber { get; set; }
        public uint PageSize { get; set; }
        public List<KontragentDto> Items;
    }
}