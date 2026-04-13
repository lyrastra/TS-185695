using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.SalesUpds.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesUpds
{
    public interface ISalesUpdsCoreApiClient
    {
        Task<DataPageResponse<DocsSalesUpdByCriteriaResponseDto>> GetByCriteriaAsync(
            DocsSalesUpdsByCriteriaRequestDto criteria, 
            int? companyId = null);
    }
}