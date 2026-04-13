using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Http.Abstractions.Dto;
using Moedelo.Common.Types;
using Moedelo.Contracts.ApiClient.Abstractions.legacy.Dtos;

namespace Moedelo.Contracts.ApiClient.Abstractions.legacy
{
    /// <summary>
    /// source: https://github.com/moedelo/md-commonV2/blob/69666c1/src/clients/contracts/Moedelo.ContractsV2.Client/IContractClient.cs
    /// </summary>
    public interface IContractsApiClient
    {
        Task<List<ContractDto>> GetByBaseIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds);
        
        Task<ContractDto> GetOrCreateMainContractAsync(FirmId firmId, UserId userId, int kontragentId);

        Task<List<ContractDto>> GetByIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> baseIds);

        Task<RentalContractDto> GetRentalByIdAsync(FirmId firmId, UserId userId, int id);

        Task<List<RentalPaymentItemDto>> GetRentalPaymentItemsByIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> ids);

        Task<List<ContractSearchResponseDto>> SearchAsync(FirmId firmId, UserId userId,
            ContractSearchCriteriaDto request, bool useReadOnly = true);

        Task<ApiDataDto<int>> SaveAsync(FirmId firmId, UserId userId, ContractDto contract);

        Task<ContractSavedDto> SaveV2Async(FirmId firmId, UserId userId, ContractV1Dto contractDto);
    }
}