using Microsoft.Extensions.Logging;
using Moedelo.Authorities.ApiClient.Abstractions;
using Moedelo.Authorities.ApiClient.Abstractions.Sfr;
using Moedelo.Authorities.ApiClient.Abstractions.Sfr.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Threading.Tasks;

namespace Moedelo.Authorities.ApiClient.Sfr
{
    [InjectAsSingleton(typeof(ISfrFirmRequisitesApiClient))]
    internal class SfrFirmRequisitesApiClient : BaseApiClient, ISfrFirmRequisitesApiClient
    {
        private const string url = "/Firm/Requisites";

        public SfrFirmRequisitesApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SfrFirmRequisitesApiClient> logger)
            : base(
                  httpRequestExecuter,
                  uriCreator,
                  auditTracer,
                  authHeadersGetter,
                  auditHeadersGetter,
                  settingRepository.Get("SfrApiEndpoint"),
                  logger)
        {
        }

        public async Task<SfrFirmRequisitesResponseDto> GetAsync(int firmId, int userId)
        {
            var response = await GetAsync<ApiDataResult<SfrFirmRequisitesResponseDto>>(url);
            return response.data;
        }

        public Task SaveAsync(int firmId, int userId, SfrFirmRequisitesSaveRequestDto saveRequest)
        {
            return PutAsync(url, saveRequest);
        }

        public Task ResetRegNumbersAsync(RegNumbersDto numbers)
        {
            return PutAsync(url + "/RegNumbers", numbers);
        }

        public Task DeleteAsync(int firmId, int userId)
        {
            return DeleteAsync(url);
        }
    }
}
