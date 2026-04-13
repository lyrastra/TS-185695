using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Docs.ApiClient.Abstractions.SalesUpds
{
    public interface ISalesUpdsNdsApiClient
    {
        /// <summary>
        /// Возвращает сумму НДС для указанных идентификаторов
        /// </summary>
        Task<IReadOnlyCollection<NdsSumDto>> GetNdsSumByBaseIdsAsync(IReadOnlyCollection<long> baseIds);
    }
}