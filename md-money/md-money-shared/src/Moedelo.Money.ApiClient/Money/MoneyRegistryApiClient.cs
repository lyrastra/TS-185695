using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.Common.Dto;
using Moedelo.Money.ApiClient.Abstractions.Money;
using Moedelo.Money.ApiClient.Abstractions.Money.Dto;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Money.ApiClient.Money
{
    [InjectAsSingleton(typeof(IMoneyRegistryApiClient))]
    internal sealed class MoneyRegistryApiClient : BaseApiClient, IMoneyRegistryApiClient
    {
        public MoneyRegistryApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<MoneyRegistryApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("MoneyApiEndpoint"),
                logger)
        {
        }

        public async Task<RegistryResponseDto> GetAsync(RegistryQueryDto query, HttpQuerySetting setting = null)
        {
            var response = await PostAsync<RegistryQueryDto, ApiPageDto<RegistryOperationDto>>(
                "/api/v1/Registry", query, setting: setting);
            return new RegistryResponseDto
            {
                Limit = response.limit,
                Offset = response.offset,
                TotalCount = response.totalCount,
                Operations = response.data
            };
        }

        public async Task<List<RegistryOperationDto>> GetOutgoingPaymentsForTaxWidgetsAsync(DateTime startDate, DateTime endDate)
        {
            var request = new 
            {
                EndDate = startDate,
                StartDate = endDate
            };
            
            var response = await GetAsync<ApiDataDto<List<RegistryOperationDto>>>(
                "/private/api/v1/RegistryTaxWidgets/GetOutgoingPaymentsForTaxWidgets", request );
            return response.data;
        }
    }
}