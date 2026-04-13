using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BillingV2.Client.BillNumberGenerate
{
    [InjectAsSingleton]
    public class BillNumberGenerateClient : BaseApiClient, IBillNumberGenerateClient
    {
        private readonly SettingValue apiEndPoint;

        public BillNumberGenerateClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("InternalBillingApiEndpoint");
        }

        public Task<string> GetNextBillNumberAsync(int index)
        {
            return GetAsync<string>("/BillNumberGenerate/GetNextBillNumber", new {index});
        }

        public Task<string> TakeBillNumberAsync()
        {
            return GetAsync<string>("/BillNumberGenerate/TakeBillNumber");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}