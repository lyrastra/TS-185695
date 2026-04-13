using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.TaxRemains.Client.Dto;

namespace Moedelo.TaxRemains.Client.Client
{
    [InjectAsSingleton]
    public class ProfitDeclarationRemainsClient : BaseApiClient, IProfitDeclarationRemainsClient
    {
        private readonly SettingValue apiEndpoint;

        public ProfitDeclarationRemainsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("TaxRemainsPrivateApiEndpoint");
        }
        public Task<ProfitDeclarationRemainsDto> GetAsync(int firmId)
        {
            return GetAsync<ProfitDeclarationRemainsDto>($"/ProfitDeclaration/Get", new { firmId });
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
    }
}
