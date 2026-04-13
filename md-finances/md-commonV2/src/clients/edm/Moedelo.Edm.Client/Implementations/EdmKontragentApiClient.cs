using Moedelo.Edm.Client.Contracts;
using Moedelo.Edm.Dto.Kontragent;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Edm.Client.Implementations
{
    [InjectAsSingleton]
    public class EdmKontragentApiClient : BaseApiClient, IEdmKontragentApiClient
    {
        private readonly SettingValue apiEndpoint;

        public EdmKontragentApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) :
            base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("EdmPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/Rest/Kontragent";
        }

        public Task<bool> IsIntegratedKontragentAsync(int firmId, int userId, int kontragentId)
        {
            return GetAsync<bool>("/IsIntegratedKontragent", new { firmId, userId, kontragentId });
        }

        public Task<IEnumerable<KontragentEdmStatusDto>> GetKontragentsEdmStatusAsync(int firmId, int userId, IEnumerable<int> idList)
        {
            return PostAsync<IEnumerable<int>, IEnumerable<KontragentEdmStatusDto>>($"/GetKontragentsEdmStatus?firmId={firmId}&userId={userId}", idList);
        }

        public Task<List<FirmEdmKontragentDto>> GetEdmKontragents(int firmId)
        {
            return GetAsync<List<FirmEdmKontragentDto>>($"/GetEdmKontragents?firmId={firmId}");
        }

        public Task<List<KontragentWithInvitationDto>> GetKontragentsWithInvitesAsync(int firmId)
        {
            return GetAsync<List<KontragentWithInvitationDto>>($"/WithInvites?firmId={firmId}");
        }
    }
}
