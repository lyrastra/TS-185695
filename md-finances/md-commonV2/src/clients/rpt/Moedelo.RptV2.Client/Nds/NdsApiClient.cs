using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RptV2.Client.Nds.Request;

namespace Moedelo.RptV2.Client.Nds
{
    [InjectAsSingleton]
    public class NdsApiClient : BaseApiClient, INdsApiClient
    {
        private readonly SettingValue apiEndpoint;

        public NdsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("RptApiEndpoint");
        }

        public async Task<FileResponse> GetAsync(InventoryJournalOfInvoicesXmlRequest request)
        {
            var response = await PostAsync<InventoryJournalOfInvoicesXmlRequest, FileResponseBase64>(
                $"/Nds/GetInventoryJournalOfInvoicesXml?userId={request.UserId}&firmId={request.FirmId}", request
            ).ConfigureAwait(false);
            return FileResponse.FromBase64(response);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
    }
}