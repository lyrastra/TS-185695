using Moedelo.Common.Types;
using Moedelo.Money.ApiClient.Abstractions.legacy.PurseOperations.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Money.ApiClient.Abstractions.legacy.PurseOperations
{
    /// <summary>
    /// Клиент операций по электронным деньгам
    /// source: https://github.com/moedelo/md-commonV2/blob/master/src/clients/accounting/Moedelo.AccountingV2.Client/PurseOperations/IPurseOperationApiClient.cs
    /// </summary>
    public interface IPurseOperationApiClient
    {
        Task<ReadPurseOperationDto[]> GetByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);

        Task<CreatedPurseOperationDto> SavePurseOperationWithTypeAsync(int firmId, int userId, PurseOperationDto dto);

        Task<CreatedPurseOperationDto> SaveRefundToClientAsync(int firmId, int userId, PurseOperationDto dto);

    }
}
