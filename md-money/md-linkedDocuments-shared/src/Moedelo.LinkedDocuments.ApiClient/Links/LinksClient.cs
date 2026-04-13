using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.ExecutionContext.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.Links;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.Links.Dtos;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.LinkedDocuments.Enums;

namespace Moedelo.LinkedDocuments.ApiClient.Links
{
    [InjectAsSingleton(typeof(ILinksClient))]
    internal sealed class LinksClient : BaseApiClient, ILinksClient
    {
        public LinksClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<LinksClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("LinkedDocumentsApiEndpoint"),
                logger)
        {
        }

        public Task<LinkWithDocumentDto[]> GetLinksWithDocumentsAsync(
            long documentBaseId,
            bool useReadOnly = false,
            HttpQuerySetting setting = null,
            CancellationToken ct = default)
        {
            var url = $"/private/api/v1/Links/{documentBaseId}/WithDocuments";
            return GetAsync<LinkWithDocumentDto[]>(
                url,
                new{ useReadOnly },
                setting: setting,
                cancellationToken: ct);
        }
        
        public Task<Dictionary<long, LinkWithDocumentDto[]>> GetLinksWithDocumentsAsync(
            IReadOnlyCollection<long> documentBaseIds,
            bool useReadOnly = false,
            HttpQuerySetting setting = null,
            CancellationToken ct = default)
        {
            return PostAsync<IReadOnlyCollection<long>, Dictionary<long, LinkWithDocumentDto[]>>(
                $"/private/api/v1/Links/WithDocuments?useReadOnly={useReadOnly}",
                documentBaseIds,
                setting: setting,
                cancellationToken: ct);
        }

        public Task<List<LinkDto>> GetLinksByLinkedDocumentTypesAsync(LinksFromBaseIdsByToDocumentTypesRequestDto request)
        {
            if (request?.FromBaseIds?.Any() != true || request.ToDocumentTypes?.Any() != true)
            {
                return Task.FromResult(new List<LinkDto>());
            }

            return PostAsync<LinksFromBaseIdsByToDocumentTypesRequestDto, List<LinkDto>>(
                "/private/api/v1/Links/GetLinksFromBaseIdsByToDocumentTypes",
                request);
        }

        public Task<Dictionary<long, int>> GetLinksCountAsync(IReadOnlyCollection<long> documentBaseIds)
        {
            return PostAsync<IReadOnlyCollection<long>, Dictionary<long, int>>(
                "/private/api/v1/Links/Count", 
                documentBaseIds);
        }
    }
}