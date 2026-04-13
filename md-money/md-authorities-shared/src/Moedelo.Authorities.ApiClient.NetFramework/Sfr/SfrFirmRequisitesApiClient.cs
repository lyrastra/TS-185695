using Moedelo.Authorities.ApiClient.Abstractions;
using Moedelo.Authorities.ApiClient.Abstractions.Sfr;
using Moedelo.Authorities.ApiClient.Abstractions.Sfr.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Threading.Tasks;

namespace Moedelo.Authorities.ApiClient.NetFramework.Sfr
{
    [InjectAsSingleton(typeof(ISfrFirmRequisitesApiClient))]
    public class SfrFirmRequisitesApiClient : BaseCoreApiClient, ISfrFirmRequisitesApiClient
    {
        private readonly SettingValue apiEndPoint;

        public SfrFirmRequisitesApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("SfrApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<SfrFirmRequisitesResponseDto> GetAsync(int firmId, int userId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId)
                .ConfigureAwait(false);

            var result = await GetAsync<ApiDataResult<SfrFirmRequisitesResponseDto>>(
                    "/Firm/Requisites", queryHeaders: tokenHeaders).ConfigureAwait(false);

            return result.data;
        }

        public async Task SaveAsync(int firmId, int userId, SfrFirmRequisitesSaveRequestDto saveRequest)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId)
                .ConfigureAwait(false);

            await PutAsync("/Firm/Requisites", saveRequest, queryHeaders: tokenHeaders)
                .ConfigureAwait(false);
        }

        public async Task DeleteAsync(int firmId, int userId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId)
                .ConfigureAwait(false);

            await DeleteAsync("/Firm/Requisites", queryHeaders: tokenHeaders)
                .ConfigureAwait(false);
        }

        public Task ResetRegNumbersAsync(RegNumbersDto numbers)
        {
            //реализовано только дл¤ .Net
            return Task.CompletedTask;
        }
    }
}
