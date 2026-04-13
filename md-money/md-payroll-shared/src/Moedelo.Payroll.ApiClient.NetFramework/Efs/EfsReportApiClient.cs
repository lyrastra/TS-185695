using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Common.Types;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Common;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.EfsExperience;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.Employment;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Efs.Injured;

namespace Moedelo.Payroll.ApiClient.NetFramework.Efs
{
    [InjectAsSingleton(typeof(IEfsReportApiClient))]
    public class EfsReportApiClient : BaseApiClient, IEfsReportApiClient
    {
        private readonly SettingValue apiEndPoint;

        public EfsReportApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager) : base(
            httpRequestExecutor,
            uriCreator,
            responseParser,
            auditTracer,
            auditScopeManager
        )
        {
            apiEndPoint = settingRepository.Get("PayrollPrivateApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<EfsEmploymentDto> GetEfsEmploymentDataAsync(FirmId firmId, UserId userId, DateTime reportDate)
        {
            return GetAsync<EfsEmploymentDto>("/EfsReport/GetEfsEmploymentData",
                new {firmId, userId, reportDate});
        }

        public Task<List<EfsPositionChangesDto>> GetEfsPositionChangesAsync(FirmId firmId, UserId userId, int year, int month)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<EfsPositionChangesDto>> GetEfsPositionDataAsync(FirmId firmId, UserId userId, EfsPositionDataRequestDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<int>> GetEfsPositionChangesWorkerIdsAsync(FirmId firmId, UserId userId, int year, int month)
        {
            throw new NotImplementedException();
        }

        public Task<List<EfsEmploymentWorkerDto>> GetEfsWorkersDataAsync(FirmId firmId, UserId userId, EfsWorkersDataRequest dto)
        {
            throw new NotImplementedException();
        }

        public Task<EfsInjuredDto> GetEfsInjuredInitialDataAsync(FirmId firmId, UserId userId, int year, int periodNumber)
        {
            throw new NotImplementedException();
        }

        public Task<EfsExperienceInitialDataDto> GetEfsExperienceInitialDataAsync(FirmId firmId, UserId userId, int year, int? workerId = null)
        {
            throw new NotImplementedException();
        }

        public Task<EfsExperienceInitialDataDto> GetEfsChildCareInitialDataAsync(FirmId firmId, UserId userId, DateTime reportDate)
        {
            throw new NotImplementedException();
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