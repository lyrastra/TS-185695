using System.Collections.Generic;

namespace Moedelo.OfficeV2.Dto.Egr.Search
{
    public class SearchEgrResponseDto
    {
        public List<SearchEgrDataDto> Result { get; set; }

        public int Count { get; set; }

        public List<RegionDto> Regions { get; set; }

        public List<ActivityGroupDto> Sections { get; set; }
    }
}
