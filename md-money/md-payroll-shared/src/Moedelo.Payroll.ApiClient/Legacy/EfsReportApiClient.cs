using System;
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
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Common;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.EfsExperience;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.Employment;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.Injured;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IEfsReportApiClient))]
    internal sealed class EfsReportApiClient : BaseLegacyApiClient, IEfsReportApiClient
    {
        public EfsReportApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<EfsReportApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"),
                logger)
        {
        }

        public Task<EfsEmploymentDto> GetEfsEmploymentDataAsync(FirmId firmId, UserId userId, DateTime reportDate)
        {
            return GetAsync<EfsEmploymentDto>("/EfsReport/GetEfsEmploymentData",
                new {firmId, userId, reportDate});
        }

        public Task<List<EfsPositionChangesDto>> GetEfsPositionChangesAsync(FirmId firmId, UserId userId, int year, int month)
        {
            return GetAsync<List<EfsPositionChangesDto>>("/EfsReport/GetEfsPositionChanges",
                new { firmId, userId, year, month });
        }

        public Task<IReadOnlyCollection<EfsPositionChangesDto>> GetEfsPositionDataAsync(FirmId firmId, UserId userId,
            EfsPositionDataRequestDto dto)
        {
            return PostAsync<EfsPositionDataRequestDto, IReadOnlyCollection<EfsPositionChangesDto>>(
                $"/EfsReport/GetEfsPositionData?firmId={firmId}&userId={userId}", dto);
        }

        public Task<IReadOnlyCollection<int>> GetEfsPositionChangesWorkerIdsAsync(FirmId firmId, UserId userId, int year, int month)
        {
            return GetAsync<IReadOnlyCollection<int>>("/EfsReport/GetEfsPositionChangesWorkerIds",
                new { firmId, userId, year, month });
        }

        public Task<List<EfsEmploymentWorkerDto>> GetEfsWorkersDataAsync(FirmId firmId, UserId userId, EfsWorkersDataRequest dto)
        {
            return PostAsync<EfsWorkersDataRequest, List<EfsEmploymentWorkerDto>>($"/EfsReport/GetEfsWorkersData?firmId={firmId}&userId={userId}",
                dto);
        }
        
        public Task<EfsInjuredDto> GetEfsInjuredInitialDataAsync(FirmId firmId, UserId userId, int year, int periodNumber)
        {
            return GetAsync<EfsInjuredDto>("/EfsReport/GetEfsInjuredInitialData",
                new { firmId, userId, year, periodNumber });
        }
        
        public Task<EfsExperienceInitialDataDto> GetEfsExperienceInitialDataAsync(FirmId firmId, UserId userId, int year, int? workerId = null)
        {
            return GetAsync<EfsExperienceInitialDataDto>("/EfsReport/GetEfsExperienceInitialData",
                new { firmId, userId, year, workerId });
        }

        public Task<EfsExperienceInitialDataDto> GetEfsChildCareInitialDataAsync(FirmId firmId, UserId userId, DateTime reportDate)
        {
            return GetAsync<EfsExperienceInitialDataDto>("/EfsReport/GetEfsChildCareInitialData",
                new { firmId, userId, reportDate });
        }

        public Task<bool> HasEfsChildCareWorkersAsync(FirmId firmId, UserId userId, DateTime reportDate)
        {
            return GetAsync<bool>("/EfsReport/HasEfsChildCareWorkers",
                new { firmId, userId, reportDate });
        }

        public Task<IReadOnlyCollection<DateTime>> GetEfsInjuredWorkPeriodsAsync(FirmId firmId, UserId userId, 
            PeriodDto request)
        {
            return PostAsync<PeriodDto, IReadOnlyCollection<DateTime>>(
                $"/EfsReport/GetEfsInjuredWorkPeriods?firmId={firmId}&userId={userId}", request);
        }
    }
}