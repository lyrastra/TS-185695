using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Common;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Dto;
using Moedelo.PaymentOrderImport.ApiClient.Abstractions.Rule;

namespace Moedelo.PaymentOrderImport.ApiClient.Rule
{
    [InjectAsSingleton(typeof(IImportRuleApiClient))]
    internal sealed class ImportRuleApiClient : BaseApiClient, IImportRuleApiClient
    {
        public ImportRuleApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<ImportRuleApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("PaymentImportRulesApiEndpoint"),
                logger)
        {
        }

        public async Task<ImportRuleDto> GetAsync(int ruleId, CancellationToken ct)
        {
            var result = await GetAsync<ApiDataDto<ImportRuleDto>>($"/api/v1/Rule/{ruleId}", cancellationToken: ct);

            return result.data;
        }

        public async Task<IReadOnlyCollection<ImportRuleDto>> GetByIdsAsync(IReadOnlyCollection<int> ids, CancellationToken ct)
        {
            if (ids?.Any() != true)
            {
                return Array.Empty<ImportRuleDto>();
            }

            var result =
                await PostAsync<IReadOnlyCollection<int>, ApiDataDto<IReadOnlyCollection<ImportRuleDto>>>($"/api/v1/Rule/GetByIds", ids, cancellationToken: ct);

            return result?.data;
        }
        
        public async Task<ImportRuleSaveResponseDto> CreateRuleAsync(ImportRuleSaveDto dto, CancellationToken ct)
        {
            var result = 
                await PostAsync<ImportRuleSaveDto, ApiDataDto<ImportRuleSaveResponseDto>>($"/api/v1/Rule", dto, cancellationToken: ct);

            return result.data;
        }

        public async Task<IReadOnlyCollection<ImportRuleListResponseDto>> GetRulesAsync(CancellationToken ct)
        {
            var result = await GetAsync<ApiDataDto<IReadOnlyCollection<ImportRuleListResponseDto>>>($"/api/v1/Rule", cancellationToken: ct);
            
            return result.data;
        }
    }
}
