using System.Collections.Generic;
using System.Threading;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using System.Threading.Tasks;

namespace Moedelo.Catalog.ApiClient.Abstractions.legacy
{
    public interface IBankApiClient
    {
        Task<BankDto[]> GetByIdsAsync(IReadOnlyCollection<int> ids, CancellationToken cancellationToken = default);
        Task<BankDto> GetByBikAsync(string bik);
        
        Task<BankDto[]> GetByBiksAsync(IReadOnlyCollection<string> biks);
        
        Task<BankDto[]> GetByInnsAsync(IReadOnlyCollection<string> inns);
    }
}