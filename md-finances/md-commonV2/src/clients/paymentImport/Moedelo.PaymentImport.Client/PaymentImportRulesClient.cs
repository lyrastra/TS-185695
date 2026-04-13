using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.PaymentImport.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.PaymentImport.Client
{
    [InjectAsSingleton]
    public class PaymentImportRulesClient : BaseCoreApiClient, IPaymentImportRulesClient
    {
        private readonly SettingValue endpoint;

        public PaymentImportRulesClient(
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
            endpoint = settingRepository.Get("PaymentImportRulesApiEndpoint");
        }

        public async Task<IdentifyOperationIgnoreNumberResponseDto[]> IdentifyIgnoreNumberAsync(int firmId, int userId, IReadOnlyCollection<IdentifyOperationIgnoreNumberRequestDto> request)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var result = await PostAsync<IReadOnlyCollection<IdentifyOperationIgnoreNumberRequestDto>, ApiDataResult<IdentifyOperationIgnoreNumberResponseDto[]>>(
                "/private/api/v1/IdentifyOperation/GetIgnoreNumber", request, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return result.data;
        }

        public async Task<IdentifyOperationResponseDto[]> IdentifyOperationsAsync(int firmId, int userId, IReadOnlyCollection<IdentifyOperationRequestDto> request)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var result = await PostAsync<IReadOnlyCollection<IdentifyOperationRequestDto>,ApiDataResult<IdentifyOperationResponseDto[]>>(
                "/private/api/v1/IdentifyOperation/GetType", request, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return result.data;
        }

        public async Task<IdentifyOperationTaxationSystemResponseDto[]> IdentifyTaxationSystemAsync(int firmId, int userId, IReadOnlyCollection<IdentifyOperationTaxationSystemRequestDto> request)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var result = await PostAsync<IReadOnlyCollection<IdentifyOperationTaxationSystemRequestDto>, ApiDataResult<IdentifyOperationTaxationSystemResponseDto[]>>(
                "/private/api/v1/IdentifyOperation/GetTaxationSystem", request, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return result.data;
        }

        public async Task<IReadOnlyCollection<AppliedImportRuleDto>> GetAppliedRulesAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var request = new AppliedImportRulesRequestDto { DocumentBaseIds = documentBaseIds };
            var result = await PostAsync<AppliedImportRulesRequestDto, ApiDataResult<IReadOnlyCollection<AppliedImportRuleDto>>>(
                "/api/v1/Operations/GetAppliedRules", request, queryHeaders: tokenHeaders).ConfigureAwait(false);
            return result.data;
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }
    }
}
