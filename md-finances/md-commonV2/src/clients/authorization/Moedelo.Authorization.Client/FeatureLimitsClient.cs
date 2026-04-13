using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Authorization.Client.Abstractions;
using Moedelo.Authorization.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Authorization.Client
{
    [InjectAsSingleton(typeof(IFeatureLimitsClient))]
    public class FeatureLimitsClient : BaseCoreApiClient, IFeatureLimitsClient
    {
        private readonly SettingValue endpoint;
        
        public FeatureLimitsClient(
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
            this.endpoint = settingRepository.GetRequired("AuthorizationPrivateApiEndpoint");
        }
        
        public Task<FirmFeatureLimitDto> GetActualAsync(int firmId, FeatureLimitId limitId)
        {
            return GetAsync<FirmFeatureLimitDto>("/v1/FeatureLimit", new {firmId, limitId});
        }

        public Task<List<FirmFeatureLimitDto>> GetActualAsync(int firmId, IReadOnlyCollection<FeatureLimitId> limitIds)
        {
            if (!limitIds.Any())
            {
                return Task.FromResult(new List<FirmFeatureLimitDto>());
            }

            var request = new FirmFeatureLimitRequestDto
            {
                FirmId = firmId,
                FeatureLimitIds = limitIds
            };
            return PostAsync<FirmFeatureLimitRequestDto, List<FirmFeatureLimitDto>>("/v1/FeatureLimit/GetActualList", request);
        }

        public Task<List<FirmFeatureLimitDto>> GetAsync(int firmId, IReadOnlyCollection<string> limitCodes)
        {
            if (!limitCodes.Any())
            {
                return Task.FromResult(new List<FirmFeatureLimitDto>());
            }

            var request = new FirmFeatureLimitByCodesRequestDto
            {
                FirmId = firmId,
                FeatureLimitCodes = limitCodes
            };

            return PostAsync<FirmFeatureLimitByCodesRequestDto, List<FirmFeatureLimitDto>>("/v1/FeatureLimit/GetFeedListByCodes", request);
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }
    }
}