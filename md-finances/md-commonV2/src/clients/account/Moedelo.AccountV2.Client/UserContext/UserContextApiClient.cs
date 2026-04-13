using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.UserContext;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountV2.Client.UserContext
{
    [InjectAsSingleton(typeof(IUserContextApiClient))]
    public class UserContextApiClient : BaseApiClient, IUserContextApiClient
    {
        private readonly IUserContextNetCoreApiClient netCoreApiClient;
        private readonly SettingValue apiEndPoint;
        
        public UserContextApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository,
            IUserContextNetCoreApiClient netCoreApiClient)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            this.netCoreApiClient = netCoreApiClient;
            apiEndPoint = settingRepository.Get("UserContextApiEndpoint");
        }

        public Task<UserCommonContextDto> GetAsync(int userId, int firmId)
        {
            return GetAsync<UserCommonContextDto>("/V2/GetUserCommonContext", new {userId, firmId});
        }

        public Task<UserContextBasicInfoDto> GetBasicInfoAsync(int userId, int firmId)
        {
            return netCoreApiClient.GetBasicInfoAsync(userId, firmId);
        }

        public Task<HeaderUserContextDto> GetHeaderAsync(int userId, int firmId, CancellationToken cancellationToken)
        {
            var uri = $"/V2/GetHeaderUserContext?firmId={firmId}&userId={userId}"; 

            return GetAsync<HeaderUserContextDto>(uri, cancellationToken: cancellationToken);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}