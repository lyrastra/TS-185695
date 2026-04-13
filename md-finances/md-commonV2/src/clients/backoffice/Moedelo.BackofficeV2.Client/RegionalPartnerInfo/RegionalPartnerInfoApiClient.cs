using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.BackofficeV2.Dto.RegionalPartnerInfo;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BackofficeV2.Client.RegionalPartnerInfo
{
    [InjectAsSingleton]
    public class RegionalPartnerInfoApiClient : BaseApiClient, IRegionalPartnerInfoApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public RegionalPartnerInfoApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("BackOfficePrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<RegionalPartnerInfoDto> GetAsync(int id)
        {
            return GetAsync<RegionalPartnerInfoDto>($"/Rest/RegionalPartnerInfo/{id}");
        }

        public Task<IReadOnlyCollection<RegionalPartnerInfoDto>> GetByIdsAsync(IReadOnlyCollection<int> ids)
        {
            return PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<RegionalPartnerInfoDto>>($"/Rest/RegionalPartnerInfo/GetByIds", ids);
        }

        public Task<RegionalPartnerInfoDto> GetByUserIdAsync(int id)
        {
            return GetAsync<RegionalPartnerInfoDto>($"/Rest/RegionalPartnerInfo/GetByUserId/{id}");
        }

        public Task<List<RegionalPartnerInfoDto>> GetByUserIdsAsync(IReadOnlyCollection<int> ids)
        {
            return PostAsync<IReadOnlyCollection<int>, List<RegionalPartnerInfoDto>>(
                "/Rest/RegionalPartnerInfo/GetByUserIds", ids);
        }

        public Task<RegionalPartnerInfoDto> GetByProfOutsourceIdAsync(int id)
        {
            return GetAsync<RegionalPartnerInfoDto>($"/Rest/RegionalPartnerInfo/GetByProfOutsourceId/{id}");
        }

        public Task<int> GetRegionalPartnerIdByUtmSourceAsync(string utmSource)
        {
            return GetAsync<int>($"/Rest/RegionalPartnerInfo/GetRegionalPartnerIdByUtmSource", new { utmSource });
        }

        public Task<bool> IsDeletedAsync(int id, CancellationToken cancellationToken = default)
        {
            return GetAsync<bool>($"/Rest/RegionalPartnerInfo/{id}/IsDeleted");
        }

        public Task<IReadOnlyCollection<RegionalPartnerInfoDto>> GetByExternalPartnerCredentialIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return GetAsync<IReadOnlyCollection<RegionalPartnerInfoDto>>(
                $"/Rest/RegionalPartnerInfo/GetByExternalPartnerCredentialId/{id}",
                cancellationToken);
        }
    }
}
