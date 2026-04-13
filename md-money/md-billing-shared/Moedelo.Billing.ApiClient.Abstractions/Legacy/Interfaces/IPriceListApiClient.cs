using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Billing.Abstractions.Legacy.Dto;

namespace Moedelo.Billing.Abstractions.Legacy.Interfaces;

public interface IPriceListApiClient
{
    Task<List<PriceListDto>> GetByTariffIdAsync(int tariffId);
    Task<List<PriceListDto>> GetByIdsAsync(IReadOnlyCollection<int> priceListIds);
    Task<List<PriceListDto>> GetByTariffIdsAsync(List<int> tariffIds);
}