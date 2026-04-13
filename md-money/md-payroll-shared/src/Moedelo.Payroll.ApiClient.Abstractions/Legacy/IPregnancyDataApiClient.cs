using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.File;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PregnancyData;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SpecialSchedule;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface IPregnancyDataApiClient
    {
        Task<IReadOnlyCollection<PregnancyInfoDto>> GetListAsync(FirmId firmId, UserId userId, int workerId);

        Task<IReadOnlyDictionary<string, string>> DeleteListAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<long> specialScheduleIds);
        
        Task<IReadOnlyCollection<PregnancyWorkerDto>> WorkerAutocompleteAsync(FirmId firmId,
            UserId userId, string query = "");
        
        Task<PregnancyWorkerDto> WorkerDataAsync(FirmId firmId, UserId userId, int workerId);

        Task<PregnancyDataDto> GetAsync(FirmId contextFirmId, UserId contextUserId, long specialScheduleId);

        Task<PregnancyDataDto> GetPrimaryAsync(FirmId firmId, UserId userId, long specialScheduleId);

        Task<PregnancyCalculationDto> CalculateAsync(FirmId contextFirmId, UserId contextUserId,
            PregnancyCalculationRequestDto request);
        
        Task<PregnancySaveResponseDto> CreateAsync(FirmId firmId, UserId userId, PregnancySaveRequestDto request);
        
        Task<PregnancySaveResponseDto> UpdateAsync(FirmId firmId, UserId userId, long specialScheduleId, 
            PregnancySaveRequestDto request);

        Task<FileResultDto> DownloadCalculationDataAsync(FirmId firmId, UserId userId, 
            PregnancyCalculationDataRequestDto request);
        
        Task<FileResultDto>  DownloadCalculationsAsync(FirmId firmId, UserId userId, 
            SpecialScheduleListRequestDto request);
        
        Task<FileResultDto>  DownloadStatementsAsync(FirmId firmId, UserId userId, 
            SpecialScheduleListRequestDto request);
        
        Task<FileResultDto>  DownloadOrdersAsync(FirmId firmId, UserId userId, 
            SpecialScheduleListRequestDto request);
        
        Task<SpecialScheduleCalculationDto> GetCalculationDataAsync(FirmId firmId, UserId userId, 
            long specialScheduleId);
    }
}