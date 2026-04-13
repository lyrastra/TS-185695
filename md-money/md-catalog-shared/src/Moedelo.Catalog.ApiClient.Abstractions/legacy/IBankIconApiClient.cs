using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Catalog.ApiClient.Abstractions.legacy
{
    public interface IBankIconApiClient
    {
        Task<Dictionary<int, string>> GetByBankIdsAsync(IReadOnlyCollection<int> bankIds);
    }
}
