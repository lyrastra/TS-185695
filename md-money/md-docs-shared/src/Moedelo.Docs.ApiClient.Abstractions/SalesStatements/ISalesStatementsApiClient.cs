using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Docs.ApiClient.Abstractions.SalesStatements.Models;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesStatements
{
    public interface ISalesStatementsApiClient
    {
        Task<DataPageResponse<DocsSalesStatementByCriteriaResponseDto>> GetByCriteriaAsync(
            DocsSalesStatementsByCriteriaRequestDto dto, 
            int? companyId = null);
        
        /// <summary>
        /// Возвращает сумму НДС для указанных идентификаторов
        /// </summary>
        Task<IReadOnlyCollection<NdsSumDto>> GetNdsSumByBaseIdsAsync(IReadOnlyCollection<long> baseIds);
    }
}