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
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildBirth;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.ChildCare;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.EarlyPregnancy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.Pregnancy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Allowances.SickList;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Worker;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IAllowanceApiClient))]
    internal sealed class AllowanceApiClient : BaseLegacyApiClient, IAllowanceApiClient
    {
        public AllowanceApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<ChargeSettingsApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"), 
                logger)
        {
        }

        public Task<ChildBirthFullAllowanceDto> GetChildBirthFullAllowanceAsync(int firmId, long allowanceId)
        {
            return GetAsync<ChildBirthFullAllowanceDto>("/Allowances/GetChildBirthFullAllowance",
                new {firmId, allowanceId});
        }

        public Task<ChildBirthAllowanceDto> GetChildBirthAllowanceAsync(int firmId, long allowanceId)
        {
            return GetAsync<ChildBirthAllowanceDto>("/Allowances/GetChildBirthAllowance", new {firmId, allowanceId});
        }

        public Task UpdateAllowanceExAsync(AllowanceExDto request)
        {
            return PostAsync("/Allowances/UpdateAllowanceEx", request);
        }

        public Task<EarlyPregnancyFullAllowanceDto> GetEarlyPregnancyFullAllowanceAsync(int firmId, long allowanceId)
        {
            return GetAsync<EarlyPregnancyFullAllowanceDto>("/Allowances/GetEarlyPregnancyFullAllowance",
                new { firmId, allowanceId });
        }

        public Task<EarlyPregnancyAllowanceDto> GetEarlyPregnancyAllowanceAsync(int firmId, long allowanceId)
        {
            return GetAsync<EarlyPregnancyAllowanceDto>("/Allowances/GetEarlyPregnancyAllowance", new { firmId, allowanceId });
        }
        
        public Task<ChildCareFullAllowanceDto> GetChildCareFullAllowanceAsync(int firmId, long allowanceId)
        {
            return GetAsync<ChildCareFullAllowanceDto>("/Allowances/GetChildCareFullAllowance",
                new { firmId, allowanceId });
        }

        public Task<ChildCareAllowanceDto> GetChildCareAllowanceAsync(int firmId, long allowanceId)
        {
            return GetAsync<ChildCareAllowanceDto>("/Allowances/GetChildCareAllowance", new { firmId, allowanceId });
        }

        public Task<PregnancyFullAllowanceDto> GetPregnancyFullAllowanceAsync(int firmId, int userId, long allowanceId)
        {
            return GetAsync<PregnancyFullAllowanceDto>("/Allowances/GetPregnancyFullAllowance",
                new { firmId, userId, allowanceId });
        }

        public Task<PregnancyAllowanceDto> GetPregnancyAllowanceAsync(int firmId, int userId, long allowanceId)
        {
            return GetAsync<PregnancyAllowanceDto>("/Allowances/GetPregnancyAllowance", new { firmId, userId, allowanceId });
        }
        
        public Task<SickListFullAllowanceDto> GetSickListFullAllowanceAsync(int firmId, int userId, long allowanceId)
        {
            return GetAsync<SickListFullAllowanceDto>("/Allowances/GetSickListFullAllowance",
                new { firmId, userId, allowanceId });
        }

        public Task<SickListAllowanceDto> GetSickListAllowanceAsync(int firmId, int userId, long allowanceId)
        {
            return GetAsync<SickListAllowanceDto>("/Allowances/GetSickListAllowance", new { firmId, userId, allowanceId });
        }
        
        public Task<List<AllowanceListItemDto>> GetAllowancesListAsync(int firmId, int userId, IReadOnlyCollection<int> workerIds, bool excludeChildCareOver3Years = false)
        {
            return PostAsync<WorkerListRequest, List<AllowanceListItemDto>>($"/Allowances/GetAllowancesList?firmId={firmId}&userId={userId}",
                new WorkerListRequest(workerIds, excludeChildCareOver3Years));
        }
        
        public Task<WorkerAllowanceDto> GetSickListAllowanceIdAsync(int firmId, int userId, SickListAllowanceIdRequestDto request)
        {
            return PostAsync<SickListAllowanceIdRequestDto, WorkerAllowanceDto>($"/Allowances/GetSickListAllowanceId?firmId={firmId}&userId={userId}",
                request);
        }
        
        public Task<WorkerAllowanceDto> GetPregnancyAllowanceIdAsync(int firmId, int userId, PregnancyAllowanceIdRequestDto request)
        {
            return PostAsync<PregnancyAllowanceIdRequestDto, WorkerAllowanceDto>($"/Allowances/GetPregnancyAllowanceId?firmId={firmId}&userId={userId}",
                request);
        }
        
        public Task<WorkerAllowanceDto> GetChildCareAllowanceIdAsync(int firmId, int userId, ChildCareAllowanceIdRequestDto request)
        {
            return PostAsync<ChildCareAllowanceIdRequestDto, WorkerAllowanceDto>($"/Allowances/GetChildCareAllowanceId?firmId={firmId}&userId={userId}",
                request);
        }

        public Task<WorkerAllowanceDto> GetChildBirthAllowanceIdAsync(int firmId, int userId, ChildBirthAllowanceIdRequestDto request)
        {
            return PostAsync<ChildBirthAllowanceIdRequestDto, WorkerAllowanceDto>($"/Allowances/GetChildBirthAllowanceId?firmId={firmId}&userId={userId}",
                request);
        }

        public Task<ChildCareSaveResponseDto> CreateChildCareAllowanceAsync(FirmId firmId, UserId userId,
            ChildCareAllowanceSaveRequestDto request)
        {
            return PostAsync<ChildCareAllowanceSaveRequestDto, ChildCareSaveResponseDto>($"/ChildCare/Create?firmId={firmId}&userId={userId}",
                request);
        }

        public Task<ChildCareWorkerDataDto> GetChildCareWorkerDataAsync(FirmId firmId, UserId userId, int workerId)
        {
            return GetAsync<ChildCareWorkerDataDto>($"/ChildCare/Worker", new {firmId, userId, workerId});
        }

        public Task<ChildCareCalculationDto> ChildCareCalculateAsync(FirmId firmId, UserId userId, ChildCareCalculationRequestDto request)
        {
            return PostAsync<ChildCareCalculationRequestDto, ChildCareCalculationDto>($"/ChildCare/Calculate?firmId={firmId}&userId={userId}",
                request);
        }

        public Task<ChildBirthCalculationDto> ChildBirthCalculateAsync(FirmId firmId, UserId userId, ChildBirthCalculationRequestDto request)
        {
            return PostAsync<ChildBirthCalculationRequestDto, ChildBirthCalculationDto>($"/ChildBirth/Calculate?firmId={firmId}&userId={userId}",
                request);
        }

        public Task<ChildBirthSaveResponseDto> CreateChildBirthAllowanceAsync(FirmId firmId, UserId userId, ChildBirthAllowanceSaveRequestDto request)
        {
            return PostAsync<ChildBirthAllowanceSaveRequestDto, ChildBirthSaveResponseDto>($"/ChildBirth/Create?firmId={firmId}&userId={userId}",
                request);
        }

        public Task<ChildCareCalculationDataDto> GetChildCareCalculationDataAsync(FirmId firmId, UserId userId, long specialScheduleId)
        {
            return GetAsync<ChildCareCalculationDataDto>($"/ChildCare/GetCalculationData", new {firmId, userId, specialScheduleId});
        }
    }
}