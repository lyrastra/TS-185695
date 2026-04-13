using Moedelo.Edm.Client.Contracts;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System;
using System.Threading.Tasks;
using Moedelo.Edm.Dto;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.Edm.Client.Implementations
{
    [InjectAsSingleton]
    public class EdmInvitationApiClient : BaseApiClient, IEdmInvitationApiClient
    {
        private readonly SettingValue apiEndpoint;

        public EdmInvitationApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) :
            base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("EdmPrivateApiEndpoint");
        }

        public Task ResetInviteStatus(int id, int firmId)
        {
            return PostAsync($"/ResetInviteStatus?id={id}&firmId={firmId}");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/Rest/EdmInvitation";
        }
        
        public Task<bool> SendInviteToMoedeloOrGlavuchetAsync(int firmId, int userId)
        {
            var setting = new HttpQuerySetting(new TimeSpan(0,0,1, 30));
            return PostAsync<bool>($"/SendInviteToMoedeloOrGlavuchet?firmId={firmId}&userId={userId}", setting: setting);
        }

        public Task<bool> SendInviteToMoedeloAsync(int firmId, int userId)
        {
            var setting = new HttpQuerySetting(new TimeSpan(0,0,1, 30));
            return PostAsync<bool>($"/SendInviteToMoedelo?firmId={firmId}&userId={userId}", setting: setting);
        }
        
        public Task<bool> SendInviteToGlavuchetAsync(int firmId, int userId, int paymentId)
        {
            var setting = new HttpQuerySetting(new TimeSpan(0,0,1, 30));
            return PostAsync<bool>($"/SendInviteToGlavuchet?firmId={firmId}&userId={userId}&paymentId={paymentId}", setting: setting);
        }

        public Task<BaseDto> InviteByGlobalIdAsync(int kontragentId, string globalId, int firmId, int userId)
        {
            return PostAsync<BaseDto>($"/InviteByGlobalId?firmId={firmId}&userId={userId}&kontragentId={kontragentId}&globalId={globalId}");
        }
        
        public Task<bool> RefuseInvitationAsync(int kontragentId, int firmId)
        {
            return PostAsync<bool>($"/RefuseInvitation?kontragentId={kontragentId}&firmId={firmId}");
        }
    }
}
