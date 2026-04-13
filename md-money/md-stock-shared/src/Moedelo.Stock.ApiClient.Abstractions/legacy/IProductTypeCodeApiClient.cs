using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy
{
    public interface IProductTypeCodeApiClient
    {
        Task<ProductTypeCodeDto[]> GetByIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> ids);
    }
}