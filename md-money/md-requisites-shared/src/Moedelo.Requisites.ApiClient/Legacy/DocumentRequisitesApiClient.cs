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

namespace Moedelo.Requisites.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IDocumentRequisitesApiClient))]
    internal sealed class DocumentRequisitesApiClient : BaseLegacyApiClient, IDocumentRequisitesApiClient
    {
        public DocumentRequisitesApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<DocumentRequisitesApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FirmRequisitesApiEndpoint"),
                logger)
        {
        }

        public async Task<DocumentRequisitesDto> GetAsync(FirmId firmId, UserId userId)
        {
            var uri = $"/DocumentRequisites/Get?firmId={firmId}&userId={userId}";

            /* Unmerged change from project 'Moedelo.Requisites.ApiClient (net6.0)'
            Before:
                        var response = await GetAsync<Wrappers.DataResponseWrapper<DocumentRequisitesDto>>(uri);
            After:
                        var response = await GetAsync<DataResponseWrapper<DocumentRequisitesDto>>(uri);
            */
            var response = await GetAsync<Wrappers.DataResponseWrapper<DocumentRequisitesDto>>(uri);
            return response.Data;
        }
    }
}