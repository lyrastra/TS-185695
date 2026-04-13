using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.TaxPostings.Dto;

namespace Moedelo.TaxPostings.Client
{
    [InjectAsSingleton]
    public class TaxPostingsPsnClient : BaseApiClient, ITaxPostingsPsnClient
    {
        private readonly SettingValue apiEndpoint;
        
        public TaxPostingsPsnClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager) :
            base(
                httpRequestExecutor,
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("TaxPostingsApiEndpoint");
        }
        
        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<List<TaxPostingPsnDto>> GetByBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            return GetAsync<List<TaxPostingPsnDto>>($"/TaxPostingsPsn/ByDocument/{documentBaseId}", new { firmId, userId});
        }

        public Task<List<TaxPostingPsnDto>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds == null || !documentBaseIds.Any())
            {
                return Task.FromResult(new List<TaxPostingPsnDto>());
            }

            return PostAsync<IReadOnlyCollection<long>, List<TaxPostingPsnDto>>(
                $"/TaxPostingsPsn/ByDocumentIds?firmId={firmId}&userId={userId}", 
                documentBaseIds);
        }

        public Task<List<TaxPostingPsnDto>> GetByPeriodAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return GetAsync<List<TaxPostingPsnDto>>("/TaxPostingsPsn/ByPeriod", new { firmId, userId, startDate, endDate });
        }

        public Task SaveAsync(int firmId, int userId, IReadOnlyCollection<TaxPostingPsnDto> taxPostings)
        {
            if (taxPostings == null || !taxPostings.Any())
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/TaxPostingsPsn/Save?firmId={firmId}&userId={userId}", taxPostings);
        }

        public Task DeleteAsync(int firmId, int userId, long documentBaseId)
        {
            return DeleteAsync($"/TaxPostingsPsn?firmId={firmId}&userId={userId}&documentBaseId={documentBaseId}");
        }

        public Task DeleteByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds == null || !documentBaseIds.Any())
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/TaxPostingsPsn/DeleteByDocuments?firmId={firmId}&userId={userId}", documentBaseIds);
        }

        public Task<List<DocumentTaxSumDto>> GetDocumentTaxSumsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds == null || !documentBaseIds.Any())
            {
                return Task.FromResult(new List<DocumentTaxSumDto>()); 
            }

            return PostAsync<IReadOnlyCollection<long>, List<DocumentTaxSumDto>>(
                    $"/TaxPostingsPsn/GetDocumentTaxSums?firmId={firmId}&userId={userId}", 
                    documentBaseIds);
        }

        public Task<List<TaxPostingPsnDto>> GetByRelatedDocumentAsync(int firmId, int userId, long documentBaseId)
        {
            return GetAsync<List<TaxPostingPsnDto>>($"/TaxPostingsPsn/ByRelatedDocument/{documentBaseId}", new { firmId, userId });
        }

        public Task<bool> HasPostingsByAnyDocumentAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds == null || !documentBaseIds.Any())
            {
                return Task.FromResult(false);
            }

            return PostAsync<IReadOnlyCollection<long>, bool>(
                $"/TaxPostingsPsn/HasPostingsByAnyDocument?firmId={firmId}&userId={userId}",
                documentBaseIds);
        }
    }
}