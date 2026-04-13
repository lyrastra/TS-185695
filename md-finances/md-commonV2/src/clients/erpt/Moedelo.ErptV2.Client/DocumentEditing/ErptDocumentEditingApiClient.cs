using System.Threading.Tasks;
using Moedelo.ErptV2.Dto.DocumentEditing;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client.DocumentEditing
{
    [InjectAsSingleton]
    public class ErptDocumentEditingApiClient : BaseApiClient, IErptDocumentEditingApiClient
    {
        private readonly SettingValue apiEndpoint;

        public ErptDocumentEditingApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(httpRequestExecutor, uriCreator,
            responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ErptApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }


        public Task AddAsync(SaveEditingDataDto data) =>
            PostAsync($"/reports/saveEditingData", data);
    }
}