using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.AccPostings.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccPostings.Client
{
    [InjectAsSingleton]
    public class CustomPostingsClient : BaseApiClient, ICustomPostingsClient
    {
        private readonly SettingValue apiEndPoint;

        public CustomPostingsClient(
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
        
        /// <inheritdoc />
        public Task<List<AccountingPostingDto>> GetByAsync(int firmId, int userId, AccountingPostingsSearchCriteriaDto criteria)
        {
            var url = $"/CustomPosting/FindByCriteria?firmId={firmId}&userId={userId}";
            return PostAsync<AccountingPostingsSearchCriteriaDto, List<AccountingPostingDto>>(url, criteria);
        }

        /// <inheritdoc />
        public Task<List<AccountingPostingDto>> GetByIdsAsync(int firmId, int userId, IReadOnlyCollection<long> potingIds)
        {
            var url = $"/CustomPosting/GetByIds?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyCollection<long>, List<AccountingPostingDto>>(url, potingIds);
        }

        /// <inheritdoc />
        public Task SaveAsync(int firmId, int userId, long documentBaseId, IReadOnlyCollection<CustomPostingDescriptionDto> customPostings)
        {
            if (customPostings?.Any() != true)
            {
                return Task.CompletedTask;
            }

            var url = $"/CustomPosting/Save?firmId={firmId}&userId={userId}&documentBaseId={documentBaseId}";
            return PostAsync(url, customPostings);
        }

        /// <inheritdoc />
        public Task DeleteByDocumentAsync(int firmId, int userId, long documentBaseId)
        {
            return DeleteByDocumentsAsync(firmId, userId, new[] { documentBaseId });
        }

        /// <inheritdoc />
        public Task DeleteByDocumentsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds?.Count == 0)
            {
                return Task.CompletedTask;
            }
            return PostAsync($"/CustomPosting/DeleteByDocuments?firmId={firmId}&userId={userId}", documentBaseIds);
        }
    }
}