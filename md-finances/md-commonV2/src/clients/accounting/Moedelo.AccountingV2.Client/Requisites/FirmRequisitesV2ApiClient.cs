using System.Threading.Tasks;
using Moedelo.AccountingV2.Dto.Requsites;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountingV2.Client.Requisites
{
    [InjectAsSingleton]
    public class FirmRequisitesV2ApiClient : BaseApiClient, IFirmRequisitesV2ApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public FirmRequisitesV2ApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccountingApi");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<FirmRequisitesV2Dto> GetUserContextRequisitesAsync(int firmId, int userId)
        {
            var response = await GetAsync<DataResponseWrapper<FirmRequisitesV2Dto>>(
                "/FirmRequisitesApi/GetUserContextRequisites", 
                new { firmId, userId }).ConfigureAwait(false);

            return response.Data;
        }
    }
}