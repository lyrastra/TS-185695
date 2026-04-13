using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.ClosedPeriods;
using Moedelo.Accounting.Enums.ClosedPeriods;
using Moedelo.Common.Types;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Accounting.ApiClient.NetFramework.legacy
{
    [InjectAsSingleton(typeof(IClosedPeriodApiClient))]
    public class ClosedPeriodApiClient : BaseApiClient, IClosedPeriodApiClient
    {
        private readonly SettingValue apiEndPoint;

        public ClosedPeriodApiClient(
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
            apiEndPoint = settingRepository.Get("AccountingApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<DateTime> GetLastClosedDateAsync(FirmId firmId, UserId userId)
        {
            throw new NotImplementedException();
        }

        public Task<DateTime> GetFirstOpenedDateAsync(FirmId firmId, UserId userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ClosedPeriodDto> GetLastClosedPeriodAsync(FirmId firmId, UserId userId)
        {
            var uri = $"/ClosedPeriods/Last?firmId={firmId}&userId={userId}";
            var response = await GetAsync<DataResponseWrapper<ClosedPeriodDto>>(uri).ConfigureAwait(false);

            return response.Data;
        }

        public Task<IReadOnlyDictionary<int, DateTime>> GetLastClosedDateByFirmIdsAsync(IReadOnlyCollection<int> firmIds)
        {
            throw new NotImplementedException();
        }

        public Task ClosePeriodByCalendarIdAsync(FirmId firmId, UserId userId, int calendarId, CloseType closeType = CloseType.Default,
            int timeoutMs = 30000)
        {
            throw new NotImplementedException();
        }

        public Task CloseMonthAsync(FirmId firmId, UserId userId, DateTime upToDate, CloseType closeType = CloseType.Default,
            int timeoutMs = 30000)
        {
            throw new NotImplementedException();
        }

        public Task<CheckPerionValidationDto> CheckPeriodAsync(FirmId firmId, UserId userId, DateTime startDate, DateTime endDate, int timeoutMs = 30000)
        {
            throw new NotImplementedException();
        }
    }
}