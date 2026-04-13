using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.WorkContracts;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy;

public interface IWorkContractApiClient
{
    Task<IReadOnlyCollection<WorkContractDto>> GetListAsync(WorkContractsCriteriaDto criteriaDto);
    Task<WorkerWorkContractDto> GetActualContractAsync(long id);
    Task UpdateNumbersAsync(int firmId, int userId, List<WorkerContractNumberUpdatingDto> dto);
    Task<bool> HasContractsWithSfrChargesAsync(int firmId, int userId, int workerId);

    Task<IReadOnlyCollection<WorkerContractSettingDto>> GetActualContractsByWorkerIdAsync(int firmId, int userId,
        int workerId);
}