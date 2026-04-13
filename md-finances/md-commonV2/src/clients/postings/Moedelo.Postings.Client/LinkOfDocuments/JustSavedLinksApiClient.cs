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
    [InjectAsSingleton(typeof(IJustSavedLinksApiClient))]
    public class JustSavedLinksApiClient : BaseApiClient, IJustSavedLinksApiClient
    {
        private readonly SettingValue apiEndpoint;

        public JustSavedLinksApiClient(
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
            return apiEndpoint.Value + "/Rest/JustSavedDocumentLinks";
        }

        public Task SaveAsync(long baseId, List<RelationWithDto> links)
        {
            return PostAsync($"/{baseId}", links);
        }

        public Task<List<RelationWithDto>> GetAsync(long baseId)
        {
            return GetAsync<List<RelationWithDto>>($"/{baseId}");
        }

        public async Task<List<LinkOfDocumentsDto>> GetAsLinkOfDocumentsAsync(int firmId, long baseId)
        {
            var response = await GetAsync(baseId).ConfigureAwait(false);

            return response?.Select(x => new LinkOfDocumentsDto
            {
                Id = 0,
                FirmId = firmId,
                LinkedFromId = baseId,
                LinkedToId = x.RelatedDocumentId,
                Date = x.Date,
                Sum = x.Sum,
                LinkType = x.Type
            }).ToList();
        }
    }
}