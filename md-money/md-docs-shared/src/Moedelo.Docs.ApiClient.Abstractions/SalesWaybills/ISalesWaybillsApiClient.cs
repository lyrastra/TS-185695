using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.SalesWaybills.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesWaybills
{
    public interface ISalesWaybillsApiClient
    {
        Task<DataPageResponse<DocsSalesWaybillByCriteriaResponseDto>> GetByCriteriaAsync(
            DocsSalesWaybillsByCriteriaRequestDto dto, 
            int? companyId = null);
        
        /// <summary>
        /// Возвращает сумму НДС для указанных идентификаторов
        /// </summary>
        Task<IReadOnlyCollection<NdsSumDto>> GetNdsSumByBaseIdsAsync(IReadOnlyCollection<long> baseIds);
    }
}