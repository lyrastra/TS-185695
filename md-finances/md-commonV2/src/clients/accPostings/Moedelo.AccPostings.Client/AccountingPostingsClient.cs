using Moedelo.AccPostings.Dto;
using Moedelo.Common.Enums.Enums.PostingEngine;
using Moedelo.Common.Enums.Enums.SyntheticAccounts;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.AccPostings.Client
{
    [InjectAsSingleton]
    public class AccountingPostingsClient : BaseApiClient, IAccountingPostingsClient
    {
        private readonly SettingValue apiEndPoint;

        public AccountingPostingsClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("AccPostingsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<List<AccountingPostingDto>> GetAsync(int firmId, int userId, long baseId)
        {
            return GetAsync<List<AccountingPostingDto>>("/Posting/ByDocument", new { firmId, userId, baseId });
        }

        public Task<List<AccountingPostingDto>> GetAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<AccountingPostingDto>());
            }

            var url = $"/Posting/ByDocuments?firmId={firmId}&userId={userId}";

            return PostAsync<IReadOnlyCollection<long>, List<AccountingPostingDto>>(url, baseIds);
        }

        public Task<List<AccountingPostingDto>> GetByAsync(int firmId, int userId, AccountingPostingsSearchCriteriaDto criteria, HttpQuerySetting setting = null)
        {
            var url = $"/Posting/FindByCriteria?firmId={firmId}&userId={userId}";
            return PostAsync<AccountingPostingsSearchCriteriaDto, List<AccountingPostingDto>>(url, criteria, setting: setting);
        }

        public Task SaveAsync(int firmId, int userId, IReadOnlyCollection<AccountingPostingDto> postings, HttpQuerySetting setting = null)
        {
            if (postings == null || !postings.Any())
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/Posting?firmId={firmId}&userId={userId}", postings, setting: setting);
        }

        public Task DeleteByDocumentAsync(int firmId, int userId, long documentBaseId)
        {
            return DeleteByDocumentAsync(firmId, userId, new[] { documentBaseId });
        }

        public Task DeleteByDocumentAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/Posting/DeleteByDocuments?firmId={firmId}&userId={userId}", documentBaseIds);
        }

        public Task DeleteByDocumentAndOperationTypeAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds,
            OperationType operationType)
        {
            if (baseIds?.Any() != true)
            {
                return Task.FromResult(new List<AccountingPostingDto>());
            }

            var url = $"/Posting/DeleteByDocumentsAndOperationType?firmId={firmId}&userId={userId}&operationType={operationType}";

            return PostAsync<IReadOnlyCollection<long>, List<AccountingPostingDto>>(url, baseIds);
        }

        public Task MergeAsync(int firmId, int userId, long primarySubcontoId, IReadOnlyCollection<long> secondarySubcontoIds, SyntheticAccountCode primaryDebitCode)
        {
            return PostAsync($"/PostingMerge/Merge?firmId={firmId}&userId={userId}&primarySubcontoId={primarySubcontoId}&primaryDebitCode={primaryDebitCode}", secondarySubcontoIds);
        }

        public Task DeleteOrphanedRecordsAsync(int firmId, int userId, DeleteOrphanedRecordsRequestDto request)
        {
            return PostAsync($"/Posting/DeleteOrphanedRecords?firmId={firmId}&userId={userId}", request);
        }
    }
}