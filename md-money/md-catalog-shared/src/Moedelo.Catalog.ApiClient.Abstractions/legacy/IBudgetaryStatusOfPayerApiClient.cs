using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using System.Threading.Tasks;

namespace Moedelo.Catalog.ApiClient.Abstractions.legacy
{
    public interface IBudgetaryStatusOfPayerApiClient
    {
        Task<BudgetaryStatusOfPayerDto[]> GetListAsync();
    }
}