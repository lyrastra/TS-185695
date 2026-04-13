using System.Collections.Generic;

namespace Moedelo.AgentsV2.Dto.WebStatistics
{
    public class WebStatisticsResponseDto
    {
        public int Count { get; set; }

        public WebStatisticsItemDto Summary { get; set; }

        public List<WebStatisticsItemDto> List { get; set; }
    }
}