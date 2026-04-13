using System.Collections.Generic;

namespace Moedelo.Tariffs.Dto.Tariffs
{
    public class TariffsByPriceListIdsResponseDto
    {
        public Dictionary<int, int> TariffByPriceList { get; set; }
        
        public List<TariffDto> Tariffs { get; set; }
    }
}