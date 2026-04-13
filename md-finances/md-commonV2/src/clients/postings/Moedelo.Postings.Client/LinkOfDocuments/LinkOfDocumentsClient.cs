using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.Postings.Dto;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Postings.Client.LinkOfDocuments
{
    [InjectAsSingleton]
    public class LinkOfDocumentsClient : BaseApiClient, ILinkOfDocumentsClient
    {
        private readonly SettingValue apiEndpoint;

        public LinkOfDocumentsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("PostingPrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/Rest/LinkOfDocuments";
        }

        public Task<List<LinkOfDocumentsDto>> GetLinksFromAsync(int firmId, int userId, long documentBaseId, LinkType linkType)
        {
            return GetAsync<List<LinkOfDocumentsDto>>(
                "/GetLinksFrom",
                new {firmId, userId, documentBaseId, linkType});
        }

        public Task<List<LinkOfDocumentsDto>> GetLinksFromAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds, LinkType linkType)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<LinkOfDocumentsDto>());
            }

            var dto = new LinksFromRequestDto
            {
                LinkFromIds = baseIds.ToList(),
                LinkType = linkType
            };

            return GetLinksFromAsync(firmId, userId, dto);
        }

        public Task<List<LinkOfDocumentsDto>> GetLinksFromAsync(int firmId, int userId, LinksFromRequestDto request)
        {
            var uri = $"/GetLinksFromDocs?firmId={firmId}&userId={userId}";

            return PostAsync<LinksFromRequestDto, List<LinkOfDocumentsDto>>(uri, request);
        }

        public Task<List<LinkOfDocumentsDto>> GetLinksFromAsync(int firmId, int userId, long documentBaseId)
        {
            return GetAsync<List<LinkOfDocumentsDto>>(
                "/GetLinksFrom",
                new { firmId, userId, documentBaseId });
        }

        public Task<List<LinkOfDocumentsDto>> GetLinksFromAsync(int firmId, int userId,
            IReadOnlyCollection<long> baseIds, bool useReadOnlyDb,
            HttpQuerySetting httpQuerySettings,
            CancellationToken cancellationToken)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<LinkOfDocumentsDto>());
            }

            var uri = $"/GetLinksFrom?firmId={firmId}&userId={userId}&useReadonlyDb={useReadOnlyDb}";

            return PostAsync<IReadOnlyCollection<long>, List<LinkOfDocumentsDto>>(uri,
                baseIds, setting: httpQuerySettings, cancellationToken: cancellationToken);
        }

        public Task<List<LinkOfDocumentsDto>> GetLinksToAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds?.Any() != true)
            {
                return Task.FromResult(new List<LinkOfDocumentsDto>());
            }

            var uri = $"/GetLinksTo?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyCollection<long>, List<LinkOfDocumentsDto>>(uri, documentBaseIds);
        }

        public Task<List<LinkOfDocumentsDto>> GetLinksToAsync(int firmId, int userId,
            IReadOnlyCollection<long> baseIds, LinkType linkType, bool useReadOnlyDb,
            HttpQuerySetting httpQuerySettings,
            CancellationToken cancellationToken)
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

            var uri = $"/GetLinksToDocs?firmId={firmId}&userId={userId}&useReadonlyDb={useReadOnlyDb}";

            return PostAsync<LinksToRequestDto, List<LinkOfDocumentsDto>>(uri, dto, setting: httpQuerySettings, cancellationToken: cancellationToken);
        }

        public Task<List<LinkOfDocumentsDto>> GetLinksToAsync(int firmId, int userId, long documentBaseId)
        {
            return GetAsync<List<LinkOfDocumentsDto>>(
                "/GetLinksToDocs",
                new { firmId, userId, documentBaseId });
        }

        public Task ReplaceAllForDocumentAsync(ReplaceAllForDocumentRequestDto dto, int firmId, int userId)
        {
            return PostAsync($"/ReplaceAllForDocument?firmId={firmId}&userId={userId}", dto);
        }

        public Task CreateMultipleAsync(int firmId, int userId, IReadOnlyCollection<ReplaceAllForDocumentRequestDto> dtos)
        {
            if (dtos?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/CreateMultiple?firmId={firmId}&userId={userId}", dtos,
                setting: new HttpQuerySetting { Timeout = TimeSpan.FromSeconds(60) });
        }

        public Task<List<DocumentLinksCountDto>> CountRealDocumentsLinksFromAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<DocumentLinksCountDto>());
            }

            var uri = $"/CountRealDocumentsLinksFrom?firmId={firmId}&userId={userId}";

            return PostAsync<IReadOnlyCollection<long>, List<DocumentLinksCountDto>>(uri, baseIds);
        }

        public Task DeleteAllDocumentLinksAsync(int firmId, int userId, long documentBaseId)
        {
            var uri = $"/DeleteAllDocumentsLinks?firmId={firmId}&userId={userId}";

            return PostAsync(uri, new[] {documentBaseId});
        }

        public Task DeleteAllDocumentLinksAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<DocumentLinksCountDto>());
            }

            var uri = $"/DeleteAllDocumentsLinks?firmId={firmId}&userId={userId}";

            return PostAsync(uri, baseIds);
        }

        public Task DeleteLinksWithDocOfTypeAsync(int firmId, int userId, DeleteLinksWithDocOfTypeRequestDto requestDto)
        {
            var uri = $"/DeleteLinksWithDocOfType?firmId={firmId}&userId={userId}";

            return PostAsync(uri, requestDto);
        }

        public Task DeleteLinksWithDocByLinkTypeAsync(int firmId, int userId, DeleteLinksWithDocByLinkTypeRequestDto requestDto)
        {
            var uri = $"/DeleteLinksWithDocByLinkType?firmId={firmId}&userId={userId}";

            return PostAsync(uri, requestDto);
        }

        public Task UpdateLinksAsync(int firmId, int userId, OneWayLinksUpdateRequestDto requestDto)
        {
            if (requestDto.ToSave?.Count > 0 || requestDto.ToDelete?.Count > 0)
            {
                var uri = $"/UpdateLinks?firmId={firmId}&userId={userId}";

                return PostAsync(uri, requestDto);
            }

            return Task.CompletedTask;
        }

        public Task UpdateLinksAsync(int firmId, int userId, IList<TwoWayLinkOfDocumentsDto> links)
        {
            if (links?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/UpdateDocumentsLinks?firmId={firmId}&userId={userId}", links);
        }

        public Task DeleteLinksAsync(int firmId, int userId, IList<TwoWayLinkOfDocumentsIdDto> linkIds)
        {
            if (linkIds?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/DeleteDocumentsLinks?firmId={firmId}&userId={userId}", linkIds);
        }
    }
}