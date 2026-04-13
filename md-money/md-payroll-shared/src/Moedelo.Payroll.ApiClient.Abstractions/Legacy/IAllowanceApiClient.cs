using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildBirth;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildCare;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.EarlyPregnancy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.Pregnancy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface IAllowanceApiClient
    {
        Task<ChildBirthFullAllowanceDto> GetChildBirthFullAllowanceAsync(int firmId, long allowanceId);

        Task<ChildBirthAllowanceDto> GetChildBirthAllowanceAsync(int firmId, long allowanceId);
        
        Task UpdateAllowanceExAsync(AllowanceExDto request);

        Task<EarlyPregnancyFullAllowanceDto> GetEarlyPregnancyFullAllowanceAsync(int firmId, long allowanceId);

        Task<EarlyPregnancyAllowanceDto> GetEarlyPregnancyAllowanceAsync(int firmId, long allowanceId);
        
        Task<ChildCareFullAllowanceDto> GetChildCareFullAllowanceAsync(int firmId, long allowanceId);

        Task<ChildCareAllowanceDto> GetChildCareAllowanceAsync(int firmId, long allowanceId);

        Task<PregnancyFullAllowanceDto> GetPregnancyFullAllowanceAsync(int firmId, int userId, long allowanceId);

        Task<PregnancyAllowanceDto> GetPregnancyAllowanceAsync(int firmId, int userId, long allowanceId);
        
        Task<SickListFullAllowanceDto> GetSickListFullAllowanceAsync(int firmId, int userId, long allowanceId);

        Task<List<AllowanceListItemDto>> GetAllowancesListAsync(int firmId, int userId, IReadOnlyCollection<int> workerIds, bool excludeChildCareOver3Years = false);

        Task<SickListAllowanceDto> GetSickListAllowanceAsync(int firmId, int userId, long allowanceId);

        Task<WorkerAllowanceDto> GetSickListAllowanceIdAsync(int firmId, int userId,
            SickListAllowanceIdRequestDto request);

        Task<WorkerAllowanceDto> GetPregnancyAllowanceIdAsync(int firmId, int userId,
            PregnancyAllowanceIdRequestDto request);

        Task<WorkerAllowanceDto> GetChildCareAllowanceIdAsync(int firmId, int userId,
            ChildCareAllowanceIdRequestDto request);

        Task<WorkerAllowanceDto> GetChildBirthAllowanceIdAsync(int firmId, int userId,
            ChildBirthAllowanceIdRequestDto request);

        Task<ChildCareSaveResponseDto> CreateChildCareAllowanceAsync(FirmId firmId, UserId userId,
            ChildCareAllowanceSaveRequestDto request);

        Task<ChildCareWorkerDataDto> GetChildCareWorkerDataAsync(FirmId firmId, UserId userId, int workerId);

        Task<ChildCareCalculationDto> ChildCareCalculateAsync(FirmId firmId, UserId userId,
            ChildCareCalculationRequestDto request);

        Task<ChildBirthCalculationDto> ChildBirthCalculateAsync(FirmId firmId, UserId userId,
            ChildBirthCalculationRequestDto request);

        Task<ChildBirthSaveResponseDto> CreateChildBirthAllowanceAsync(FirmId firmId, UserId userId,
            ChildBirthAllowanceSaveRequestDto request);

        Task<ChildCareCalculationDataDto> GetChildCareCalculationDataAsync(FirmId firmId, UserId userId, long specialScheduleId);
    }
}