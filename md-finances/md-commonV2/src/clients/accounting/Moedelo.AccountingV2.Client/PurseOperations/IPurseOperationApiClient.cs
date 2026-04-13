using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.PurseOperation;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.AccountingV2.Client.PurseOperations
{
    public interface IPurseOperationApiClient : IDI
    {
        Task DeleteAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);

        Task<PurseOperationClientDataDto> GetPurseOperationByBaseIdAsync(int firmId, int userId, long documentBaseId);

        Task<PurseOperationResultDto> SavePurseOperationAsync(int firmId, int userId, PurseOperationDto dto);

        Task<CreatedPurseOperationDto> SavePurseOperationWithTypeAsync(int firmId, int userId, PurseOperationForMultipleTypesDto dto);

        Task<CreatedPurseOperationDto> SavePurseOperationWithWaybill(int firmId, int userId, PurseOperationClientDataDto dto);

        Task<CreatedPurseOperationDto> SaveRefundToClientAsync(int firmId, int userId, PurseOperationForMultipleTypesDto dto);

        Task<PurseOperationsCountDto[]> GetNumberOfPurseOperationsAsync(int firmId, int userId, IReadOnlyCollection<int> kontragentIds);

        Task ProvideAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds);
    }
}