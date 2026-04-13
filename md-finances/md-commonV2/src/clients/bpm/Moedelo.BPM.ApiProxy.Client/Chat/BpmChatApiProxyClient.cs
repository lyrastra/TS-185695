using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BPM.ApiProxy.Client.Chat
{
    [InjectAsSingleton]
    public class BpmChatApiProxyClient : BaseApiClient, IBpmChatApiProxyClient
    {
        private readonly SettingValue apiEndpoint;

        public BpmChatApiProxyClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("BPMApiUrl");
        }

        public Task<List<ChatUserDto>> GetUsersAsync() => GetAsync<List<ChatUserDto>>("/Rest/ChatApi/Users");

        public Task<ChatAccountDto> GetAccountInfoAsync(int firmId) => GetAsync<ChatAccountDto>("/Rest/ChatApi/AccountInfo", new {firmId});
        
        public Task<ChatAccountDto> GetAccountInfoAsync(string login) => GetAsync<ChatAccountDto>("/Rest/ChatApi/AccountInfo", new {login});

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/apiproxy";
        }
    }
}