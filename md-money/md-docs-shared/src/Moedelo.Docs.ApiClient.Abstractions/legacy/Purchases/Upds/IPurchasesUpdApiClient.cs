using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Upds.Dto;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Upds
{
    /// <summary>
    /// source: https://github.com/moedelo/md-commonV2/blob/master/src/clients/docs/Moedelo.Docs.Client/Upd/IUpdApiClient.cs
    /// </summary>
    public interface IPurchasesUpdApiClient
    {
        Task<List<PurchasesUpdWithItemsDto>> GetWithItemsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Возвращает суммы, покрытые платежными документами
        /// </summary>
        Task<List<PaidSumDto>> GetPaidSumByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Перепроведение проводок 
        /// </summary>
        Task ReprovideByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);
        
        /// <summary>
        /// Создаёт УПД на покупку через вызов external api
        /// </summary>
        Task<PurchasesUpdExternalDto> SaveAsync(FirmId firmId, UserId userId, PurchasesUpdSaveRequestDto dto);
    }
}
