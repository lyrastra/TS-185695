using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.File;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SickListData;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SpecialSchedule;

namespace Moedelo.Payroll.ApiClient.Abstractions.Legacy
{
    public interface ISickListDataApiClient
    {
        Task<IReadOnlyCollection<SickListInfoDto>> GetListAsync(FirmId firmId, UserId userId, int workerId);

        Task<IReadOnlyDictionary<string, string>> DeleteListAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<long> specialScheduleIds);

        Task<IReadOnlyCollection<SickListWorkerDto>> WorkerAutocompleteAsync(FirmId firmId,
            UserId userId, string query = "");
        
        Task<SickListWorkerDto> WorkerDataAsync(FirmId firmId, UserId userId, int workerId);
        
        Task<SickListDataDto> GetAsync(FirmId firmId, UserId userId, long specialScheduleId);

        Task<SickListDataDto> GetPrimaryAsync(FirmId firmId, UserId userId, long specialScheduleId);

        Task<SickListCalculationDto>
            CalculateAsync(FirmId firmId, UserId userId, SickListCalculationRequestDto request);

        Task<SickListSaveResponseDto> CreateAsync(FirmId firmId, UserId userId, SickListSaveRequestDto request);
        
        Task<SickListSaveResponseDto> UpdateAsync(FirmId firmId, UserId userId, long specialScheduleId, 
            SickListSaveRequestDto request);

        Task<FileResultDto> DownloadCalculationDataAsync(FirmId firmId, UserId userId, 
            SickListCalculationDataRequestDto request);

        Task<FileResultDto>  DownloadCalculationsAsync(FirmId firmId, UserId userId, 
            SpecialScheduleListRequestDto request);
        
        Task<SpecialScheduleCalculationDto> GetCalculationDataAsync(FirmId firmId, UserId userId, 
            long specialScheduleId);
    }
}