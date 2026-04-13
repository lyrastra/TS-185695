using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.SuiteCrm.Dto;
using Moedelo.SuiteCrm.Dto.Bpm;

namespace Moedelo.SuiteCrm.Client
{
    [InjectAsSingleton]
    public class ClientApiClient : BaseApiClient, IClientApiClient
    {
        private const string Uri = "/v1";
        private readonly SettingValue apiEndPoint;

        public ClientApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) :
            base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("OutsourceClientApiEndpoint");
        }

        public async Task<ClientDto[]> GetByFirmIdsAsync(IReadOnlyCollection<int> firmIds)
        {
            var response = await PostAsync<IReadOnlyCollection<int>, DataResponse<ClientDto[]>>($"{Uri}/GetByFirmIds", firmIds)
                .ConfigureAwait(false);
            return response?.Data;
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}
