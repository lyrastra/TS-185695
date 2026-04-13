using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Upds.Dto;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Upds
{
    /// <summary>
    /// source: https://github.com/moedelo/md-commonV2/blob/master/src/clients/docs/Moedelo.Docs.Client/SalesUpd/ISalesUpdApiClient.cs
    /// </summary>
    public interface ISalesUpdApiClient
    {
        Task<List<SalesUpdWithItemsDto>> GetWithItemsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Возвращает суммы, покрытые платежными документами
        /// </summary>
        Task<List<PaidSumDto>> GetPaidSumByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);

        Task<List<SalesUpdWithItemsDto>> GetWithItemsAsync(int firmId, int userId, List<long> baseIds);
        
        /// <summary>
        /// Создаёт УПД на покупку через вызов external api
        /// source: https://github.com/moedelo/md-commonV2/blob/master/src/clients/docs/Moedelo.Docs.Client/SalesUpd/ISalesUpdRestApiClient.cs#L20
        /// </summary>
        Task<SalesUpdExternalDto> SaveAsync(FirmId firmId, UserId userId, SalesUpdSaveRequestDto dto);
    }
}