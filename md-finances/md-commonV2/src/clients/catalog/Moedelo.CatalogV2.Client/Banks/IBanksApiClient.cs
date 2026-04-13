using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.CatalogV2.Dto.Banks;

namespace Moedelo.CatalogV2.Client.Banks
{
    public interface IBanksApiClient
    {
        Task<List<BankDto>> GetByIdsAsync(IReadOnlyCollection<int> ids);

        Task<List<BankDto>> GetByIdsAsync(IReadOnlyCollection<int> ids, CancellationToken cancellationToken);

        Task<List<BankDto>> GetByBiksAsync(IReadOnlyCollection<string> biks);

        Task<List<BankDto>> GetByBiksAsync(IReadOnlyCollection<string> biks, CancellationToken cancellationToken);

        Task<List<BankDto>> GetByInnsAsync(IReadOnlyCollection<string> inns);
    }
}