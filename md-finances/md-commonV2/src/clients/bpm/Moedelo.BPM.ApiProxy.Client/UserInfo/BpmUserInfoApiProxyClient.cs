using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BPM.ApiProxy.Client.UserInfo
{
    [InjectAsSingleton]
    public class BpmUserInfoApiProxyClient : BaseApiClient, IBpmUserInfoApiProxyClient
    {
        private readonly SettingValue apiEndpoint;

        public BpmUserInfoApiProxyClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("BPMApiUrl");
        }

        public Task<List<MessageAuthorDto>> GetInfoAsync(List<string> userIds)
            => PostAsync<IEnumerable<string>, List<MessageAuthorDto>>("/Rest/UserInfo", userIds);

        public Task<MessageAuthorDto> GetInfoAsync(string userId)
            => GetAsync<MessageAuthorDto>("/Rest/UserInfo", new {userId});

        public Task<MessageAuthorDto> GetInfoByEmailAsync(string email)
            => GetAsync<MessageAuthorDto>("/Rest/UserInfo/ByEmail", new { email});

        public Task<HttpFileModel> GetAvatarAsync(string avatarId)
            => GetFileAsync("/Rest/UserInfo/Avatar", new {avatarId});

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/apiproxy";
        }
    }
}