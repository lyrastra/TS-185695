using System.Threading.Tasks;
using Moedelo.Changelog.Shared.Api.Abstractions;
using Moedelo.Changelog.Shared.Api.Abstractions.Dto;
using Moedelo.Changelog.Shared.Domain.Enums;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Changelog.Shared.Api.Framework
{
    [InjectAsSingleton(typeof(IChangeLogClient))]
    public sealed class ChangeLogClient : BaseCoreApiClient, IChangeLogClient
    {
        private readonly SettingValue apiEndpoint;

        public ChangeLogClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager) : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            this.apiEndpoint = settingRepository.Get("ChangeLogApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<ChangeLogDto> GetAsync(ChangeLogEntityType entityType, long entityId)
        {
            // todo: implement ChangeLogClient::GetAsync
            throw new System.NotImplementedException();
        }
    }
}
