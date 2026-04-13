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
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.PregnancyData;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.SpecialSchedule;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IPregnancyDataApiClient))]
    internal sealed class PregnancyDataApiClient : BaseLegacyApiClient, IPregnancyDataApiClient
    {
        public PregnancyDataApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,            
            ISettingRepository settingRepository,
            ILogger<Ndfl6ReportInitialDataApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"), 
                logger)
        {
        }

        public Task<IReadOnlyCollection<PregnancyInfoDto>> GetListAsync(FirmId firmId, UserId userId, int workerId)
        {
            return GetAsync<IReadOnlyCollection<PregnancyInfoDto>>("/Pregnancy/GetList", new {firmId, userId, workerId});
        }
        
        public Task<IReadOnlyDictionary<string, string>> DeleteListAsync(FirmId firmId, UserId userId, 
            IReadOnlyCollection<long> specialScheduleIds)
        {
            return PostAsync<SpecialScheduleListRequestDto, IReadOnlyDictionary<string, string>>(
                $"/Pregnancy/DeleteList?firmId={firmId}&userId={userId}",
                new SpecialScheduleListRequestDto(specialScheduleIds));
        }
        
        public Task<IReadOnlyCollection<PregnancyWorkerDto>> WorkerAutocompleteAsync(FirmId firmId, 
            UserId userId, string query = "")
        {
            return GetAsync<IReadOnlyCollection<PregnancyWorkerDto>>("/Pregnancy/Worker/WorkerAutocomplete",
                new {firmId, userId, query});
        }
        
        public Task<PregnancyWorkerDto> WorkerDataAsync(FirmId firmId, UserId userId, int workerId)
        {
            return GetAsync<PregnancyWorkerDto>("/Pregnancy/Worker", new {firmId, userId, workerId});
        }
        
        public Task<PregnancyDataDto> GetAsync(FirmId firmId, UserId userId, long specialScheduleId)
        {
            return GetAsync<PregnancyDataDto>("/Pregnancy/Get", new {firmId, userId, specialScheduleId});
        }

        public Task<PregnancyDataDto> GetPrimaryAsync(FirmId firmId, UserId userId, long specialScheduleId)
        {
            return GetAsync<PregnancyDataDto>("/Pregnancy/GetPrimary", new {firmId, userId, specialScheduleId});
        }

        public Task<PregnancyCalculationDto> CalculateAsync(FirmId firmId, UserId userId, 
            PregnancyCalculationRequestDto request)
        {
            return PostAsync<PregnancyCalculationRequestDto, 
                PregnancyCalculationDto>($"/Pregnancy/Calculate?firmId={firmId}&userId={userId}", request);
        }
        
        public Task<PregnancySaveResponseDto> CreateAsync(FirmId firmId, UserId userId, PregnancySaveRequestDto request)
        {
            return PostAsync<PregnancySaveRequestDto, 
                PregnancySaveResponseDto>($"/Pregnancy/Create?firmId={firmId}&userId={userId}", request);
        }
        
        public Task<PregnancySaveResponseDto> UpdateAsync(FirmId firmId, UserId userId, long specialScheduleId, 
            PregnancySaveRequestDto request)
        {
            return PostAsync<PregnancySaveRequestDto, PregnancySaveResponseDto>(
                $"/Pregnancy/Update?firmId={firmId}&userId={userId}&specialScheduleId={specialScheduleId}", request);
        }
        
        public Task<FileResultDto> DownloadCalculationDataAsync(FirmId firmId, UserId userId, 
            PregnancyCalculationDataRequestDto request)
        {
            return GetAsync<FileResultDto>("/Pregnancy/DownloadCalculationData", new {firmId, userId, request});
        }
        
        public Task<FileResultDto> DownloadCalculationsAsync(FirmId firmId, UserId userId, 
            SpecialScheduleListRequestDto request)
        {
            return PostAsync<SpecialScheduleListRequestDto, FileResultDto>(
                $"/Pregnancy/DownloadCalculations?firmId={firmId}&userId={userId}", request);
        }
        
        public Task<FileResultDto> DownloadStatementsAsync(FirmId firmId, UserId userId, 
            SpecialScheduleListRequestDto request)
        {
            return PostAsync<SpecialScheduleListRequestDto, FileResultDto>(
                $"/Pregnancy/DownloadStatements?firmId={firmId}&userId={userId}", request);
        }
        
        public Task<FileResultDto> DownloadOrdersAsync(FirmId firmId, UserId userId, 
            SpecialScheduleListRequestDto request)
        {
            return PostAsync<SpecialScheduleListRequestDto, FileResultDto>(
                $"/Pregnancy/DownloadOrders?firmId={firmId}&userId={userId}", request);
        }
        
        public Task<SpecialScheduleCalculationDto> GetCalculationDataAsync(FirmId firmId, UserId userId, 
            long specialScheduleId)
        {
            return GetAsync<SpecialScheduleCalculationDto>("/Pregnancy/GetCalculationData",
                new {firmId, userId, specialScheduleId});
        }
    }
}
