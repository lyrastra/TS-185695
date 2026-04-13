using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.RequisitesV2.Dto.DetailsOfState;

namespace Moedelo.RequisitesV2.Client.DetailsOfState
{
    [InjectAsSingleton]
    public class DetailsOfStateApiClient : BaseApiClient, IDetailsOfStateApiClient
    {
        private readonly SettingValue apiEndPoint;

        public DetailsOfStateApiClient(
            IHttpRequestExecutor requestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(requestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FirmRequisitesApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<DetailsOfStateDto> GetFssDetailsByIdAsync(int firmId, int userId, int id)
        {
            var response = await GetAsync<GetFssDetailsByIdResponse>("/DetailsOfState/GetFssDetailsById", new {firmId, userId, id}).ConfigureAwait(false);
            return response.Data;
        }

        public async Task<DetailsOfStateDto> GetPfrDetailsByIdAsync(int firmId, int userId, int id)
        {
            var response = await GetAsync<GetFssDetailsByIdResponse>("/DetailsOfState/GetPfrDetailsById", new { firmId, userId, id }).ConfigureAwait(false);
            return response.Data;
        }
    }
}
