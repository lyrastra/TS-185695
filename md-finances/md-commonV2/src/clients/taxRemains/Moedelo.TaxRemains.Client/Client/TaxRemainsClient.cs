using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.TaxRemains.Client.Dto;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.TaxRemains.Client.Client
{
    [InjectAsSingleton]
    public class TaxRemainsClient : BaseApiClient, ITaxRemainsClient
    {
        private readonly SettingValue apiEndpoint;

        public TaxRemainsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {

            apiEndpoint = settingRepository.Get("TaxRemainsPrivateApiEndpoint");
        }

        public Task<TaxRemainDto> GetAsync(int firmId)
        {
            return GetAsync<TaxRemainDto>($"/Get/{firmId}");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
    }
}
