using Moedelo.Docs.ApiClient.Abstractions.legacy.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Waybills.Dto;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Sales.Waybills
{
    /// <summary>
    /// source: https://github.com/moedelo/md-commonV2/blob/33823daa29ab897bfff4ee6c90ff56c4fb07d4b7/src/clients/accounting/Moedelo.AccountingV2.Client/Waybill/ISalesWaybillApiClient.cs
    /// </summary>
    public interface ISalesWaybillApiClient
    {
        /// <summary>
        /// Возвращает суммы, покрытые платежными документами
        /// </summary>
        Task<List<PaidSumDto>> GetPaidSumByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);

        Task<SalesWaybillDto> GetByBaseIdAsync(FirmId firmId, UserId userId, long baseId);

        /// <summary>
        /// Создаёт накладную на продажу через вызов external api
        /// </summary>
        Task<SalesWaybillDto> SaveAsync(FirmId firmId, UserId userId, SalesWaybillSaveRequestDto dto);
    }
}
