using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Price;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CatalogV2.Client.Price
{
    public interface IPriceExtendedApiClient : IDI
    {
        Task<PriceExtendedDto> GetByPriceIdAsync(int priceId);

        Task<List<PriceExtendedDto>> GetListByCriterionAsync(GetPriceExtendedCriterionDto criterionDto);
    }
}