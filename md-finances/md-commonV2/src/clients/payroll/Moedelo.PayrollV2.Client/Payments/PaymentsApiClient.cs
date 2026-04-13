using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.PayrollV2.Dto;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using System;
using Moedelo.PayrollV2.Dto.SalaryPayments;
using System.Collections.Generic;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.PayrollV2.Client.Payments
{
    [InjectAsSingleton]
    public class PaymentsApiClient : BaseApiClient, IPaymentsApiClient
    {
        private readonly SettingValue apiEndPoint;
        private readonly HttpQuerySetting defaultSetting = new HttpQuerySetting(TimeSpan.FromMinutes(5));

        public PaymentsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("PayrollApi");
        }

        public async Task<bool> HasAnyCalendarPaymentEventsAsync(int firmId, int userId)
        {
            var response = await GetAsync<DataResponse<bool>>("/HasAnyCalendarPaymentEvents", new { firmId, userId }, null,
                    defaultSetting)
                        .ConfigureAwait(false);
            return response.Data;
        }

        public async Task<bool> IsCustomChargeExistAsync(int firmId, int userId, string kontragentName, decimal sum)
        {
            var result = await GetAsync<DataResponse<bool>>("/IsCustomChargeExist",
                new {firmId, userId, kontragentName, sum}, null,
                defaultSetting).ConfigureAwait(false);

            return result.Data;
        }

        public async Task<bool> IsDeductionExistAsync(int firmId, int userId, string kontragentName)
        {
            var result = await GetAsync<DataResponse<bool>>("/IsDeductionExist", new { firmId, userId, kontragentName }, null,
                defaultSetting).ConfigureAwait(false);

            return result.Data;
        }

        public Task  SetSalaryChargingStartDate(int firmId, int userId, DateTime date)
        {
            return PostAsync<object>($"/SetSalaryChargingStartDate?userId={userId}&firmId={firmId}", new { date }, null,
                defaultSetting);
        }

        public async Task<decimal> GetTripPaymentSumAsync(int firmId, int userId, long tripId)
        {
            var result = await GetAsync<DataResponse<decimal>>($"/GetTripPaymentSum", new { firmId, userId, tripId }, null,
                defaultSetting).ConfigureAwait(false);

            return result.Data;
        }
      
        public async Task<List<SickKudirDto>> GetKudirSicklistPaymentsAsync(int firmId, int userId, int year)
        {
            return (
                await PostAsync<object, ListResponse<SickKudirDto>>($"/GetKudirSicklistPayments?firmId={firmId}&userId={userId}", 
                new { year }, 
                null,
                    defaultSetting).ConfigureAwait(false))
            .Items;
        }

        protected override HttpQuerySetting DefaultHttpQuerySetting()
        {
            return defaultSetting;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/PaymentsRestApi";
        }
    }
}