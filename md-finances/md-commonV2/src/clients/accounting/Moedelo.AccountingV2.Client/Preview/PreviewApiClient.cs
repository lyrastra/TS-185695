using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.Preview
{
    [InjectAsSingleton]
    public class PreviewApiClient : BaseApiClient, IPreviewApiClient
    {
        private readonly SettingValue apiEndPoint;

        public PreviewApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(
                httpRequestExecutor, 
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        public async Task<byte[]> GetBillOnlineAsync(int id, string guid, bool useWatermark)
        {
            return (await GetAsync<DataResponseWrapper<byte[]>>("/GetBillOnline", new { id, guid, useWatermark }).ConfigureAwait(false)).Data;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value + "/PreviewApi";
        }
    }
}
