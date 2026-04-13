using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Waybills.Dto;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Purchases.Waybills
{
    public interface IPurchasesWaybillApiClient
    {
        /// <summary>
        /// Возвращает суммы, покрытые платежными документами
        /// </summary>
        Task<List<PaidSumDto>> GetPaidSumByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);

        /// <summary>
        /// Создаёт накладную на покупку через вызов external api
        /// </summary>
        Task<PurchasesWaybillDto> SaveAsync(FirmId firmId, UserId userId, PurchasesWaybillSaveRequestDto dto);
    }
}