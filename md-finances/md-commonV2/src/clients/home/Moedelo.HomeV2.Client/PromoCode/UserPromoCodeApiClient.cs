using System.Threading.Tasks;
using Moedelo.HomeV2.Dto.PromoCode;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HomeV2.Client.PromoCode
{
    [InjectAsSingleton]
    public class UserPromoCodeApiClient : BaseApiClient, IUserPromoCodeApiClient
    {
        private readonly SettingValue apiEndPoint;

        public UserPromoCodeApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("HomePrivateApiEndpoint");
        }

        public Task<string> GetActivePromoCodeAsync(int firmId, int userId)
        {
            return GetAsync<string>("/GetActivePromoCode", new { firmId, userId });
        }

        public Task<InviteFriendsWidgetDto> GetParticipantStateAsync(int firmId, int userId)
        {
            return GetAsync<InviteFriendsWidgetDto>("/GetParticipantStateAsync", new { firmId, userId });
        }

        public Task<int> GetInvitedUsersCountAsync(int userId)
        {
            return GetAsync<int>("/GetInvitedUsersCount", new {userId});
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/Rest/UserPromoCode/V2";
        }
    }
}