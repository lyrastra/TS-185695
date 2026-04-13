using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.File;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SickListData;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SpecialSchedule;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(ISickListDataApiClient))]
    internal sealed class SickListDataApiClient : BaseLegacyApiClient, ISickListDataApiClient
    {
        public SickListDataApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,            
            ISettingRepository settingRepository,
            ILogger<SickListDataApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"), 
                logger)
        {
        }

        public Task<IReadOnlyCollection<SickListInfoDto>> GetListAsync(FirmId firmId, UserId userId, int workerId)
        {
            return GetAsync<IReadOnlyCollection<SickListInfoDto>>("/SickList/GetList", new {firmId, userId, workerId});
        }

        public Task<IReadOnlyDictionary<string, string>> DeleteListAsync(FirmId firmId, UserId userId, 
            IReadOnlyCollection<long> specialScheduleIds)
        {
            return PostAsync<SpecialScheduleListRequestDto, IReadOnlyDictionary<string, string>>(
                $"/SickList/DeleteList?firmId={firmId}&userId={userId}",
                new SpecialScheduleListRequestDto(specialScheduleIds));
        }
        
        public Task<IReadOnlyCollection<SickListWorkerDto>> WorkerAutocompleteAsync(FirmId firmId, 
            UserId userId, string query = "")
        {
            return GetAsync<IReadOnlyCollection<SickListWorkerDto>>("/SickList/Worker/WorkerAutocomplete",
                new {firmId, userId, query});
        }

        public Task<SickListWorkerDto> WorkerDataAsync(FirmId firmId, UserId userId, int workerId)
        {
            return GetAsync<SickListWorkerDto>("/SickList/Worker", new {firmId, userId, workerId});
        }
        
        public Task<SickListDataDto> GetAsync(FirmId firmId, UserId userId, long specialScheduleId)
        {
            return GetAsync<SickListDataDto>("/SickList/Get", new {firmId, userId, specialScheduleId});
        }

        public Task<SickListDataDto> GetPrimaryAsync(FirmId firmId, UserId userId, long specialScheduleId)
        {
            return GetAsync<SickListDataDto>("/SickList/GetPrimary", new {firmId, userId, specialScheduleId});
        }

        public Task<SickListCalculationDto> CalculateAsync(FirmId firmId, UserId userId, 
            SickListCalculationRequestDto request)
        {
            return PostAsync<SickListCalculationRequestDto, 
                SickListCalculationDto>($"/SickList/Calculate?firmId={firmId}&userId={userId}", request);
        }

        public Task<SickListSaveResponseDto> CreateAsync(FirmId firmId, UserId userId, SickListSaveRequestDto request)
        {
            return PostAsync<SickListSaveRequestDto, 
                SickListSaveResponseDto>($"/SickList/Create?firmId={firmId}&userId={userId}", request);
        }
        
        public Task<SickListSaveResponseDto> UpdateAsync(FirmId firmId, UserId userId, long specialScheduleId, 
            SickListSaveRequestDto request)
        {
            return PostAsync<SickListSaveRequestDto, SickListSaveResponseDto>(
                $"/SickList/Update?firmId={firmId}&userId={userId}&specialScheduleId={specialScheduleId}", request);
        }

        public Task<FileResultDto> DownloadCalculationDataAsync(FirmId firmId, UserId userId, 
            SickListCalculationDataRequestDto request)
        {
            return GetAsync<FileResultDto>("/SickList/DownloadCalculationData", new {firmId, userId, request});
        }
        
        public Task<FileResultDto> DownloadCalculationsAsync(FirmId firmId, UserId userId, 
            SpecialScheduleListRequestDto request)
        {
            return PostAsync<SpecialScheduleListRequestDto, FileResultDto>(
                $"/SickList/DownloadCalculations?firmId={firmId}&userId={userId}", request);
        }
        
        public Task<SpecialScheduleCalculationDto> GetCalculationDataAsync(FirmId firmId, UserId userId, 
            long specialScheduleId)
        {
            return GetAsync<SpecialScheduleCalculationDto>("/SickList/GetCalculationData",
                new {firmId, userId, specialScheduleId});
        }
    }
}
