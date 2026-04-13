using System;
using System.Threading.Tasks;
using Moedelo.Docs.Dto.Common;
using Moedelo.Docs.Dto.NdsAdjustment;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Docs.Client.NdsAdjustment
{
    [InjectAsSingleton]
    public class NdsAdjustmentApiClient : BaseCoreApiClient, INdsAdjustmentApiClient
    {
        private const string prefix = "/api/v1";

        private readonly SettingValue apiEndpoint;
        private readonly HttpQuerySetting DeductionHttpQuerySetting = new HttpQuerySetting(TimeSpan.FromSeconds(60));

        public NdsAdjustmentApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("TaxWidgetApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public async Task<NdsAccrualCriteriaResponseDto> GetNdsAccrualByCriteriaAsync(int firmId, int userId, NdsAccrualCriteriaRequestDto requestDto)
        {
            var uri = $"{prefix}/NdsAccrual/GetByCriteria";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await PostAsync<NdsAccrualCriteriaRequestDto, ApiDataResult<NdsAccrualCriteriaResponseDto>>(
                uri, requestDto,
                queryHeaders: tokenHeaders).ConfigureAwait(false);

            return response.data;
        }

        public async Task<NdsDeductionCriteriaResponseDto> GetNdsDeductionByCriteriaAsync(int firmId, int userId, NdsDeductionCriteriaRequestDto requestDto)
        {
            var uri = $"{prefix}/NdsDeduction/GetByCriteria";
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await PostAsync<NdsDeductionCriteriaRequestDto, ApiDataResult<NdsDeductionCriteriaResponseDto>>(
                uri,
                requestDto,
                queryHeaders: tokenHeaders,
                setting: DeductionHttpQuerySetting).ConfigureAwait(false);

            return response.data;
        }
    }
}
