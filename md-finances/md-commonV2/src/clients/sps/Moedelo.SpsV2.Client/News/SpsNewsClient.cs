using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SpsV2.Dto.News;

namespace Moedelo.SpsV2.Client.News
{
    [InjectAsSingleton(typeof(ISpsNewsClient))]
    public class SpsNewsClient : BaseApiClient, ISpsNewsClient
    {
        private readonly SettingValue apiEndpoint;

        private const string CONTROLLER_URI = "/Rest/BuroNews";

        public SpsNewsClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) 
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ProPrivateApiEndpoint");
        }

        public Task<List<DailyBuroNewsDto>> GetNewsAsync(string type, int take = 10, int skip = 0)
        {
            return GetAsync<List<DailyBuroNewsDto>>($"/news/{type}/{take}/{skip}", new {type, take, skip});
        }

        public Task<List<DailyBuroNewsDto>> GetDailyNewsAsync(GetBuroNewsRequestDto request)
        {
            return GetAsync<List<DailyBuroNewsDto>>("/GetDailyNews", request);
        }

        public Task<List<MainBuroNewsDto>> GetMainNewsAsync(GetBuroNewsRequestDto request)
        {
            return GetAsync<List<MainBuroNewsDto>>("/GetMainNews", request);
        }

        public Task<ChangeVoteResponseDto> ChangeVote(ChangeVoteRequestDto request)
        {
            return GetAsync<ChangeVoteResponseDto>("/ChangeVote", request);
        }

        protected override string GetApiEndpoint()
        {
            return $"{apiEndpoint.Value}{CONTROLLER_URI}";
        }
    }
}
