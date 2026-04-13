using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy;
using Moedelo.Requisites.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Requisites.ApiClient.Legacy.Wrappers;

namespace Moedelo.Requisites.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IPurseApiClient))]
    internal sealed class PurseApiClient : BaseLegacyApiClient, IPurseApiClient
    {
        public PurseApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PurseApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FirmRequisitesApiEndpoint"),
                logger)
        {
        }

        public async Task<PurseDto[]> GetAsync(FirmId firmId, UserId userId)
        {
            var result = await GetAsync<ListResponseWrapper<PurseDto>>("/Purse/GetAll", new { firmId, userId })
                .ConfigureAwait(false);
            return result.Items;
        }

        public async Task<PurseDto> GetByNameAsync(FirmId firmId, UserId userId, string name)
        {
            var result = await GetAsync<DataResponseWrapper<PurseDto>>("/Purse/GetByName", new { firmId, userId, name })
                .ConfigureAwait(false);
            return result.Data;
        }

        public async Task<int> SaveVirtualPurseAsync(FirmId firmId, UserId userId, PurseDto purse)
        {
            var result = await PostAsync<PurseDto, DataResponseWrapper<int>>(
                $"/Purse/SaveVirtualPurse?firmId={firmId}&userId={userId}", purse).ConfigureAwait(false);
            return result.Data;
        }
    }
}