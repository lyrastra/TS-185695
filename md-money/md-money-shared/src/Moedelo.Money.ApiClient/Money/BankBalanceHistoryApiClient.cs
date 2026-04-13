using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.Money;
using Moedelo.Money.ApiClient.Abstractions.Money.Dto;

namespace Moedelo.Money.ApiClient.Money
{
    [InjectAsSingleton(typeof(IBankBalanceHistoryApiClient))]
    public class BankBalanceHistoryApiClient : BaseApiClient, IBankBalanceHistoryApiClient
    {
        public BankBalanceHistoryApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<BankBalanceHistoryApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("MoneyBankBalanceHistoryApiEndpoint"),
                logger)
        {
        }

        public async Task<BankBalanceResponseDto> GetAsync(int settlementAccountId, DateTime startDate,
            DateTime endDate)
        {
            var response = await GetAsync<ApiDataDto<BankBalanceResponseDto>>("/private/api/v1/Balances/", new
                {
                    SettlementAccountId = settlementAccountId,
                    StartDate = startDate,
                    EndDate = endDate
                });

            return response.data;
        }

        public async Task<IReadOnlyDictionary<int, LastBankBalanceResponseDto[]>> OnDateByFirms(BankBalancesOnDateByFirmsRequestDto request)
        {
            if (request.FirmIds == null || request.FirmIds.Count == 0)
            {
                return new Dictionary<int, LastBankBalanceResponseDto[]>();
            }

            var response = await PostAsync<BankBalancesOnDateByFirmsRequestDto, ApiDataDto<Dictionary<int, LastBankBalanceResponseDto[]>>>(
                "/private/api/v1/Balances/OnDateByFirms", request, setting: new HttpQuerySetting(TimeSpan.FromSeconds(60)));

            return response.data;
        }
    }
}
