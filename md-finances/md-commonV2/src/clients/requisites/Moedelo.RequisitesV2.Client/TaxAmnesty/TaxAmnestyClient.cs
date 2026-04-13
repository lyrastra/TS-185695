using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.RequisitesV2.Client.TaxAmnesty
{
    [InjectAsSingleton]
    public class TaxAmnestyClient : BaseApiClient, ITaxAmnestyClient
    {
        private readonly SettingValue apiEndPoint;

        public TaxAmnestyClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) :
            base(
                httpRequestExecutor,
                uriCreator,
                responseParser, auditTracer, auditScopeManager
                )
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task ActivateIfAvailableAsync(int firmId, int userId)
        {
            return PostAsync($"/TaxAmnesty/ActivateIfAvailable?firmId={firmId}&userId={userId}");
        }

        public Task<bool> GetAsync(int firmId, int userId)
        {
            return GetAsync<bool>($"/TaxAmnesty/Get?firmId={firmId}&userId={userId}");
        }
    }
}
