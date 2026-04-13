using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.legacy;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.LinkedDocuments.ApiClient.legacy.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.LinkedDocuments.Enums;

namespace Moedelo.LinkedDocuments.ApiClient.legacy
{
    [InjectAsSingleton(typeof(ILinkOfDocumentsClient))]
    public class LinkOfDocumentsClient : BaseApiClient, ILinkOfDocumentsClient
    {
        public LinkOfDocumentsClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<LinkOfDocumentsClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("PostingPrivateApiEndpoint"),
                logger)
        {
        }

        public Task CreateLinksAsync(FirmId firmId, UserId userId, IReadOnlyCollection<LinkOfDocumentsDto> links, HttpQuerySetting querySetting = null)
        {
            if (links?.Any() != true)
            {
                return Task.CompletedTask;
            }
            var request = new OneWayLinksUpdateRequestDto
            {
                ToSave = links
            };
            return PostAsync($"/Rest/LinkOfDocuments/UpdateLinks?firmId={firmId}&userId={userId}", request, setting: querySetting);

        }

        public Task UpdateLinksAsync(FirmId firmId, UserId userId, IReadOnlyCollection<TwoWayLinkOfDocumentsDto> links,
            HttpQuerySetting querySetting = null)
        {
            if (links?.Any() != true)
            {
                return Task.CompletedTask;
            }
            return PostAsync($"/Rest/LinkOfDocuments/UpdateDocumentsLinks?firmId={firmId}&userId={userId}", links, setting: querySetting);
        }

        public Task ReplaceAllForDocumentAsync(FirmId firmId, UserId userId, ReplaceAllForDocumentRequestDto saveRequest)
        {
            return PostAsync($"/Rest/LinkOfDocuments/ReplaceAllForDocument?firmId={firmId}&userId={userId}", saveRequest);
        }

        public Task DeleteAllDocumentLinksAsync(FirmId firmId, UserId userId, long documentBaseId, HttpQuerySetting querySetting = null)
        {
            return DeleteAllDocumentLinksAsync(firmId, userId, new[] { documentBaseId }, querySetting);
        }

        public Task DeleteAllDocumentLinksAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> baseIds, HttpQuerySetting querySetting = null)
        {
            if (baseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }

            var uri = $"/Rest/LinkOfDocuments/DeleteAllDocumentsLinks?firmId={firmId}&userId={userId}";
            return PostAsync(uri, baseIds, setting: querySetting);
        }

        public Task<List<LinkOfDocumentsDto>> GetLinksFromAsync(FirmId firmId, UserId userId, long documentBaseId)
        {
            return GetAsync<List<LinkOfDocumentsDto>>(
                "/Rest/LinkOfDocuments/GetLinksFrom",
                new { firmId, userId, documentBaseId });
        }
        
        public Task<List<LinkOfDocumentsDto>> GetLinksFromAsync(
            FirmId firmId,
            UserId userId,
            IReadOnlyCollection<long> baseIds,
            LinkType linkType,
            bool useReadOnly = false,
            CancellationToken ct = default)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<LinkOfDocumentsDto>());
            }
            
            var dto = new LinksFromRequestDto
            {
                LinkFromIds = baseIds.ToList(),
                LinkType = linkType,
                UseReadonlyDb = useReadOnly
            };
            
            return PostAsync<LinksFromRequestDto, List<LinkOfDocumentsDto>>(
                $"/Rest/LinkOfDocuments/GetLinksFromDocs?firmId={firmId}&userId={userId}", 
                dto,
                cancellationToken: ct);
        }

        public Task<List<LinkOfDocumentsDto>> GetLinksToAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<long> baseIds, LinkType linkType)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<LinkOfDocumentsDto>());
            }

            var dto = new LinksToRequestDto
            {
                LinkToIds = baseIds.ToList(),
                LinkType = linkType
            };

            return PostAsync<LinksToRequestDto, List<LinkOfDocumentsDto>>(
                $"/Rest/LinkOfDocuments/GetLinksToDocs?firmId={firmId}&userId={userId}", 
                dto);
        }

        public Task DeleteLinksWithDocOfTypeAsync(FirmId firmId, UserId userId,
            DeleteLinksWithDocOfTypeRequestDto requestDto)
        {
            var uri = $"/Rest/LinkOfDocuments/DeleteLinksWithDocOfType?firmId={firmId}&userId={userId}";
            return PostAsync(uri, requestDto);
        }
    }
}
