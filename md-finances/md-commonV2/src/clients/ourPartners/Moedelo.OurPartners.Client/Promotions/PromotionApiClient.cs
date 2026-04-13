using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.OurPartners.Dto;
using Moedelo.OurPartners.Dto.Promotions;

namespace Moedelo.OurPartners.Client.Promotions
{
    [InjectAsSingleton]
#pragma warning disable CS0618
    public class PromotionApiClient : BaseCoreApiClient, IPromotionApiClient
#pragma warning restore CS0618
    {
        private readonly SettingValue apiEndpoint;

        public PromotionApiClient(
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
            apiEndpoint = settingRepository.Get("OurPartnersApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
        
        public async Task<ApiListDto<PromotionDto>> GetByCriteriaAsync(int firmId, int userId, PromotionCriterionDto criterion)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            return await PostAsync<PromotionCriterionDto, ApiListDto<PromotionDto>>(
                    $"/api/v1/Promotion/GetByCriteria?firmId={firmId}&userId={userId}", criterion, tokenHeaders).ConfigureAwait(false);
        }
        
        public async Task<ApiDto<PromotionDto>> GetAsync(int firmId, int userId, int id)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            return await GetAsync<ApiDto<PromotionDto>>(
                    $"/api/v1/Promotion/{id}", new { firmId, userId }, tokenHeaders).ConfigureAwait(false);
        }
        
        public async Task<ApiDto<int>> CreateAsync(int firmId, int userId, PromotionSaveRequestDto saveRequest)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            return await PostAsync<PromotionSaveRequestDto, ApiDto<int>>(
                    $"/api/v1/Promotion?firmId={firmId}&userId={userId}", saveRequest, tokenHeaders).ConfigureAwait(false);
        }
        
        public async Task UpdateAsync(int firmId, int userId, int id, PromotionSaveRequestDto saveRequest)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            await PutAsync($"/api/v1/Promotion/{id}?firmId={firmId}&userId={userId}", saveRequest, tokenHeaders).ConfigureAwait(false);
        }
        
        public async Task DeleteAsync(int firmId, int userId, int id)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            await DeleteAsync($"/api/v1/Promotion/{id}",new {firmId, userId}, tokenHeaders).ConfigureAwait(false);
        }

        public async Task<ApiDto<string>> SaveImageAsync(int firmId, int userId, HttpFileModel file)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            return await SendFileAsync<ApiDto<string>>($"/api/v1/Promotion/SaveImage", file, tokenHeaders).ConfigureAwait(false);
        }
    }
}