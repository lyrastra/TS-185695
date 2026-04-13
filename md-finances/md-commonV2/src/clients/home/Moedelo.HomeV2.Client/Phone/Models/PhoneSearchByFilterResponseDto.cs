using System.Collections.Generic;
using Moedelo.HomeV2.Dto.Phone;

namespace Moedelo.HomeV2.Client.Phone.Models
{
    public class PhoneSearchByFilterResponseDto
    {
        public IReadOnlyCollection<PhoneDto> Data { get; set; }

        public int? Offset { get; set; }

        public int Limit { get; set; }

        public int TotalCount { get; set; }
    }
}