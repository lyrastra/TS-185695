using Moedelo.CatalogV2.Dto.BudgetaryStatusOfPayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.CatalogV2.Client.BudgetaryStatusOfPayer
{
    public interface IBudgetaryStatusOfPayerApiClient
    {
        Task<List<BudgetaryStatusOfPayerDto>> GetListAsync();
    }
}