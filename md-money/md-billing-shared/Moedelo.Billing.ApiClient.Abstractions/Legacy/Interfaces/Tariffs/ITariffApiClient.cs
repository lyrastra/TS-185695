using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Legacy.Dto.Tariffs;

namespace Moedelo.Billing.Abstractions.Legacy.Interfaces.Tariffs;

public interface ITariffApiClient
{
    Task<List<TariffDto>> GetAllTariffsAsync();

    Task<TariffDto> GetAsync(int id);

    Task<IReadOnlyCollection<TariffDto>> GetByAsync(TariffFilterDto filter, CancellationToken cancellationToken);

    Task<List<TariffDto>> GetListAsync(IReadOnlyCollection<int> ids);

    Task<IReadOnlyDictionary<int, TariffDto>> GetByPriceListIdsAsync(IReadOnlyCollection<int> priceListIds);
}