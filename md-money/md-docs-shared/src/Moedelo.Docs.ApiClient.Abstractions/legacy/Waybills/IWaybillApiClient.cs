using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Docs.ApiClient.Abstractions.legacy.Waybills.Dto;

namespace Moedelo.Docs.ApiClient.Abstractions.legacy.Waybills
{
    /// <summary>
    /// source: https://github.com/moedelo/md-commonV2/blob/master/src/clients/accounting/Moedelo.AccountingV2.Client/Waybill/IWaybillApiClient.cs
    /// </summary>
    public interface IWaybillApiClient
    {
        Task<List<WaybillDto>> GetByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);
    }
}