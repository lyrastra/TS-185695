using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.PurseOperation;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy
{
    public interface IPurseOperationApiClient
    {
        Task DeleteAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);

        Task SavePurseOperationWithTypeAsync(FirmId firmId, UserId userId, PurseOperationForMultipleTypesDto dto);

        Task ChangeTaxationSystemAsync(FirmId firmId, UserId userId, ChangeTaxationSystemRequestDto dto);
    }
}
