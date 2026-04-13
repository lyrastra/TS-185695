using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PaymentImport.Dto;

namespace Moedelo.PaymentImport.Client
{
    [InjectAsSingleton]
    internal sealed class OutsourceImportRulesClient : BaseCoreApiClient, IOutsourceImportRulesClient
    {
        private readonly SettingValue endpoint;

        public OutsourceImportRulesClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            endpoint = settingRepository.Get("OutsourcePaymentImportApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }

        public async Task<IReadOnlyCollection<OutsourceAppliedImportRuleDto>> GetAppliedRulesAsync(int firmId, int userId, AppliedRulesRequestDto request)
        {
            if (request?.DocumentBaseIds?.Any() != true)
            {
                return Array.Empty<OutsourceAppliedImportRuleDto>();
            }

            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var result = await PostAsync<AppliedRulesRequestDto, ApiDataResult<IReadOnlyCollection<OutsourceAppliedImportRuleDto>>>(
                "/api/v1/ImportRules/GetApplied",
                request,
                queryHeaders: tokenHeaders).ConfigureAwait(false);

            return result?.data ?? Array.Empty<OutsourceAppliedImportRuleDto>();
        }
    }
}