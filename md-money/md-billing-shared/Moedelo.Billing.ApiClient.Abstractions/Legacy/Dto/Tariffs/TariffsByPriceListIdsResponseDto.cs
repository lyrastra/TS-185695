using System.Collections.Generic;

namespace Moedelo.Billing.Abstractions.Legacy.Dto.Tariffs;

public class TariffsByPriceListIdsResponseDto
{
    public Dictionary<int, int> TariffByPriceList { get; set; }

    public List<TariffDto> Tariffs { get; set; }
}