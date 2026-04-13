using System.Collections.Generic;

namespace Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos
{
    public class KontragentsPageDto
    {
        public int TotalCount { get; set; } 
        public uint PageNumber { get; set; }
        public uint PageSize { get; set; }
        public List<KontragentDto> Items;
    }
}