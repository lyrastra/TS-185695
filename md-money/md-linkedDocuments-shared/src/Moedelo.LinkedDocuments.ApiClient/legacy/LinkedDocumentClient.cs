using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.legacy;
using Moedelo.LinkedDocuments.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.LinkedDocuments.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.LinkedDocuments.ApiClient.legacy
{
    [InjectAsSingleton(typeof(ILinkedDocumentClient))]
    public class LinkedDocumentClient : BaseApiClient, ILinkedDocumentClient
    {
        public LinkedDocumentClient(
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

        public Task<List<LinkedDocumentDto>> GetByIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> ids,
            bool useReadonlyDb = false)
        {
            if (ids?.Any() != true)
            {
                return Task.FromResult(new List<LinkedDocumentDto>());
            }

            var uri = $"/Rest/LinkedDocument/GetByIds?firmId={firmId}&userId={userId}&useReadonlyDb={useReadonlyDb}";

            return PostAsync<IReadOnlyCollection<long>, List<LinkedDocumentDto>>(
                uri, ids);
        }

        public Task<List<LinkedDocumentDto>> GetCreatedByUserAsync(FirmId firmId, UserId userId,
            bool useReadonlyDb, CancellationToken cancellationToken)
        {
            var uri = $"/Rest/LinkedDocument/CreatedByUser?firmId={(int)firmId}&userId={(int)userId}&useReadonlyDb={useReadonlyDb}";

            return GetAsync<List<LinkedDocumentDto>>(uri, cancellationToken: cancellationToken);
        }

        public Task<long> CreateOrUpdateAsync(FirmId firmId, UserId userId, LinkedDocumentSaveRequestDto saveRequest)
        {
            return PostAsync<LinkedDocumentSaveRequestDto, long>(
                $"/Rest/LinkedDocument/CreateOrUpdate?firmId={firmId}&userId={userId}",
                saveRequest);
        }

        public Task DeleteAsync(FirmId firmId, UserId userId, long id)
        {
            return PostAsync($"/Rest/LinkedDocument/Delete?firmId={firmId}&userId={userId}&id={id}");
        }

        public Task DeleteAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> ids)
        {
            if (ids?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/Rest/LinkedDocument/DeleteByIds?firmId={firmId}&userId={userId}", ids);
        }

        public Task UpdateTaxStatusesAsync(FirmId firmId, UserId userId, IReadOnlyDictionary<long, TaxPostingStatus> docTaxStatusMap)
        {
            if (docTaxStatusMap?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync(
                $"/Rest/LinkedDocument/UpdateTaxStatuses?firmId={firmId}&userId={userId}",
                docTaxStatusMap);
        }

        public Task<IReadOnlyDictionary<long, long>> CreateMultipleAsync(FirmId firmId, UserId userId, IReadOnlyCollection<CreateLinkedDocumentRequestDto> documents, HttpQuerySetting setting = null)
        {
            if (documents?.Any() != true)
            {
                return Task.FromResult<IReadOnlyDictionary<long, long>>(new Dictionary<long, long>());
            }

            return PostAsync<IReadOnlyCollection<CreateLinkedDocumentRequestDto>, IReadOnlyDictionary<long, long>>(
                $"/Rest/LinkedDocument/CreateMultiple?firmId={firmId}&userId={userId}",
                documents,
                setting: setting);
        }
    }
}