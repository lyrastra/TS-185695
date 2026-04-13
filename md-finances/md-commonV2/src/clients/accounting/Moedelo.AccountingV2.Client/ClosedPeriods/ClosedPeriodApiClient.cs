using System;
using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.ClosedPeriods;
using Moedelo.Common.Enums.Enums.ClosingPeriod;
using Moedelo.CommonV2.Extensions.System;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.ClosedPeriods
{
    [InjectAsSingleton]
    public class ClosedPeriodApiClient : BaseApiClient, IClosedPeriodApiClient
    {
        private readonly SettingValue apiEndPoint;

        public ClosedPeriodApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                auditTracer,
                auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<DateTime> GetLastClosedDateAsync(int firmId, int userId)
        {
            var response = await GetAsync<ClosedPeriodDto>("/ClosedPeriodApi/GetLastClosedDate", new { firmId, userId })
                .ConfigureAwait(false);

            return response.Data.ParseOrDefaultDate(DateTime.MinValue);
        }

        public Task CloseMonthAsync(int firmId, int userId, DateTime upToDate, CloseType? closeType = CloseType.Default)
        {
            return PostAsync($"/ClosedPeriodApi/CloseMonth?firmId={firmId}&userId={userId}&date={upToDate:yyyy-MM-dd}&closeType={closeType}", setting: new HttpQuerySetting { Timeout = TimeSpan.FromSeconds(300) });
        }

        public async Task<int?> GetLastNotClosedPeriodEventIdAsync(int firmId, int userId, DateTime endDate)
        {
            var response = await PostAsync<DataResponseWrapper<int?>>($"/ClosedPeriodApi/GetLastNotClosedPeriodEventId?firmId={firmId}&userId={userId}&endDate={endDate:yyyy-MM-dd}")
               .ConfigureAwait(false);

            return response.Data;
        }

        public Task CloseYearAsync(int firmId, int userId, int year)
        {
            return PostAsync($"/ClosedPeriodApi/CloseYear?firmId={firmId}&userId={userId}&year={year}");
        }

        public async Task<CheckClosingMonthDto> CheckClosingMonthAsync(int firmId, int userId, int month, int year)
        {
            var response = await GetAsync<DataResponseWrapper<CheckClosingMonthDto>>($"/ClosedPeriodApi/CheckClosingMonth?firmId={firmId}&userId={userId}&month={month}&year={year}")
                .ConfigureAwait(false);
            return response.Data;
        }

        public Task OpenPeriodAsync(int firmId, int userId, DateTime date)
        {
            return PostAsync($"/ClosedPeriodApi/OpenPeriod?date={date:yyyy-MM-dd}&userId={userId}&firmId={firmId}");
        }
    }
}
