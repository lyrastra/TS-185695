using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions;
using Moedelo.Docs.ApiClient.Abstractions.Registry;
using Moedelo.Docs.ApiClient.Abstractions.Registry.Models;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Threading.Tasks;

namespace Moedelo.Docs.ApiClient.PurchasesWaybills
{
    [InjectAsSingleton(typeof(IDocsRegistryApiClient))]
    public class DocsRegistryApiClient : BaseApiClient, IDocsRegistryApiClient
    {
        public DocsRegistryApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<DocsRegistryApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("DocsRegistryApiEndpoint"),
                logger)
        {
        }

        public async Task<DocumentsResponseDto> GetAsync(DocumentsRequestDto requestDto)
        {
            var url = "/api/v1/Documents/GetByCriteria";
            var response = await PostAsync<DocumentsRequestDto, DataPageResponse<DocumentResponseDto>>(url, requestDto);
            return new DocumentsResponseDto
            {
                Offset = response.Offset,
                Limit = response.Limit,
                TotalCount = response.TotalCount,
                Documents = response.Data
            };
        }
    }
}