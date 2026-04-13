using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.AccountingStatements.ApiClient.Abstractions;
using Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos.SelfCostTax;
using Moedelo.AccountingStatements.ApiClient.Abstractions.Dtos.TaxSelfCost;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.AccountingStatements.ApiClient
{
    [InjectAsSingleton(typeof(ISelfCostTaxClient))]
    public class SelfCostTaxClient : BaseApiClient, ISelfCostTaxClient
    {
        public SelfCostTaxClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SelfCostTaxClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("AccountingStatementsApiEndpoint"),
                logger)
        {
        }

        public async Task<SelfCostTaxCreateResponseDto[]> CreateAsync(IReadOnlyCollection<SelfCostTaxCreateRequestDto> requests)
        {
            var response = await PostAsync<IReadOnlyCollection<SelfCostTaxCreateRequestDto>, DataResponseWrapper<SelfCostTaxCreateResponseDto[]>>(
                "/private/api/v1/SelfCostTax/CreateMultiple", requests, setting: new HttpQuerySetting(new TimeSpan(0, 2, 0)));
            return response.Data;
        }

        public Task DeleteByPeriodAsync(SelfCostTaxDeleteByPeriodDto request)
        {
            return PostAsync(
                "/private/api/v1/SelfCostTax/DeleteByPeriod", 
                request);
        }

        public Task<SelfCostTaxGetResponseDto> GetByPeriodAsync(SelfCostTaxGetByPeriodDto request)
        {
            return PostAsync<SelfCostTaxGetByPeriodDto, SelfCostTaxGetResponseDto>(
                "/private/api/v1/SelfCostTax/GetByPeriod", request);
        }
    }
}
