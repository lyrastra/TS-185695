using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.Funds;
using Moedelo.Payroll.Enums.Funds;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IFundPaymentApiClient))]
    public class FundPaymentApiClient : BaseLegacyApiClient, IFundPaymentApiClient
    {
        private static readonly HttpQuerySetting DefaultHttpSetting = new HttpQuerySetting(TimeSpan.FromSeconds(90));
        
        public FundPaymentApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<FundPaymentApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"),
                logger)
        {
        }

        public Task<decimal> GetSfrPaymentAsync(int firmId, int userId, int year, int month)
        {
            return GetAsync<decimal>("/FundPayments/GetSfrPayment", new {firmId, userId, year, month});
        }

        public Task<bool> HasNonPayedFundPaymentsAsync(int firmId, int userId, FundsRegistryRequestDto requestDto)
        {
            return GetAsync<bool>("/FundPayments/HasNonPayedFundPayments",
                new
                {
                    firmId, userId, requestDto.StartDate, requestDto.EndDate, requestDto.IsFfoms, requestDto.IsPfr,
                    requestDto.IsFssDisability, requestDto.IsFssInjured
                });
        }
        
        public Task<decimal> GetSfrInjuredPaymentAsync(int firmId, int userId, int year, int month)
        {
            return GetAsync<decimal>("/FundPayments/GetSfrInjuredPayment", new {firmId, userId, year, month});
        }

        public Task<IReadOnlyCollection<(FundChargeType fundType, decimal sum)>> GetPaymentsAsync(int firmId, 
            int userId, int year, int month)
        {
            return GetAsync<IReadOnlyCollection<(FundChargeType fundType, decimal sum)>>("/FundPayments/GetPayments",
                new { firmId, userId, year, month });
        }
        
        public Task<SfrPaymentDataDto> GetSfrDataAsync(FirmId firmId, UserId userId, int year, int month)
        {
            return GetAsync<SfrPaymentDataDto>("/FundPayments/GetSfrData",
                new {firmId, userId, year, month}, setting: DefaultHttpSetting);
        }
    }
}