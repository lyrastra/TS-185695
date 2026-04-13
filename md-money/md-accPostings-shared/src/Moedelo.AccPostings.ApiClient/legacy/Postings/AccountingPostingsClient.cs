using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Postings;
using Moedelo.AccPostings.ApiClient.Abstractions.legacy.Postings.Dto;
using Moedelo.AccPostings.Enums;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Infrastructure.System.Extensions.Collections;

namespace Moedelo.AccPostings.ApiClient.legacy.Postings
{
    [InjectAsSingleton(typeof(IAccountingPostingsClient))]
    internal sealed class AccountingPostingsClient : BaseLegacyApiClient, IAccountingPostingsClient
    {
        public AccountingPostingsClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<AccountingPostingsClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccPostingsApiEndpoint"),
                logger)
        {
        }

        public Task<AccountingPostingDto[]> GetAsync(FirmId firmId, UserId userId, long baseId)
        {
            var url = $"/Posting/ByDocument?firmId={firmId}&userId={userId}&baseId={baseId}";

            return GetAsync<AccountingPostingDto[]>(url);
        }

        public Task<AccountingPostingDto[]> GetAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<long> baseIds)
        {
            if (baseIds.NullOrEmpty())
            {
                return Task.FromResult(Array.Empty<AccountingPostingDto>());
            }

            var url = $"/Posting/ByDocuments?firmId={firmId}&userId={userId}";

            return PostAsync<IReadOnlyCollection<long>, AccountingPostingDto[]>(url,
                baseIds.ToDistinctReadOnlyCollection());
        }

        public Task<AccountingPostingDto[]> GetByAsync(FirmId firmId, UserId userId,
            AccountingPostingsSearchCriteriaDto criteria)
        {
            var url = $"/Posting/FindByCriteria?firmId={firmId}&userId={userId}";

            return PostAsync<AccountingPostingsSearchCriteriaDto, AccountingPostingDto[]>(url, criteria);
        }

        public Task SaveAsync(FirmId firmId, UserId userId, IReadOnlyCollection<AccountingPostingDto> postings, HttpQuerySetting setting = null)
        {
            if (postings.NullOrEmpty())
            {
                return Task.CompletedTask;
            }

            var url = $"/Posting?firmId={firmId}&userId={userId}";

            return PostAsync(url, postings, setting: setting);
        }

        public Task DeleteByDocumentAsync(FirmId firmId, UserId userId, long documentBaseId)
        {
            return DeleteByDocumentAsync(firmId, userId, new[] {documentBaseId});
        }

        public Task DeleteByDocumentAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds.NullOrEmpty())
            {
                return Task.CompletedTask;
            }

            var url = $"/Posting/DeleteByDocuments?firmId={firmId}&userId={userId}";

            return PostAsync(url, documentBaseIds);
        }

        public Task DeleteByDocumentAndOperationTypeAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<long> baseIds,
            OperationType operationType)
        {
            if (baseIds.NullOrEmpty())
            {
                return Task.CompletedTask;
            }

            var url =
                $"/Posting/DeleteByDocumentsAndOperationType?firmId={firmId}&userId={userId}&operationType={(int)operationType}";

            return PostAsync(url, baseIds);
        }
    }
}