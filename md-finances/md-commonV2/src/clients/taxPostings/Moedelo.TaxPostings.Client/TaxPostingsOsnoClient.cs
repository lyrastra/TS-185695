using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.TaxPostings;
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
    public class TaxPostingsOsnoClient : BaseApiClient, ITaxPostingsOsnoClient
    {
        private readonly SettingValue apiEndpoint;
        
        public TaxPostingsOsnoClient(
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
        
        public Task<List<TaxPostingOsnoDto>> GetByBaseIdAsync(int firmId, int userId, long baseId,
            bool filterBadOperationStates = true)
        {
            return GetAsync<List<TaxPostingOsnoDto>>("/TaxPostingsOsno/GetByBaseId", new { firmId, userId, baseId, filterBadOperationStates});
        }

        public Task SaveAsync(int firmId, int userId, IReadOnlyCollection<TaxPostingOsnoDto> taxPostings)
        {
            if (taxPostings == null || !taxPostings.Any())
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/TaxPostingsOsno/Save?firmId={firmId}&userId={userId}", taxPostings);
        }

        public Task DeleteAsync(int firmId, int userId, long documentBaseId)
        {
            return PostAsync($"/TaxPostingsOsno/Delete?firmId={firmId}&userId={userId}&baseId={documentBaseId}");
        }

        public Task DeleteByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds)
        {
            if (baseIds is not { Count: > 0 })
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/TaxPostingsOsno/DeleteByBaseIds?firmId={firmId}&userId={userId}", baseIds);
        }

        public Task<List<DocumentTaxSumDto>> GetDocumentTaxSumsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIdList)
        {
            if (baseIdList?.Count > 0)
            {
                return PostAsync<IReadOnlyCollection<long>, List<DocumentTaxSumDto>>(
                    $"/TaxPostingsOsno/GetDocumentTaxSums?firmId={firmId}&userId={userId}", baseIdList);
            }
            return Task.FromResult(new List<DocumentTaxSumDto>());
		}

        public Task<List<TaxPostingOsnoDto>> GetByDocumentIdsAsync(
            int firmId,
            int userId,
            IReadOnlyCollection<long> documentBaseIds,
            bool filterBadOperationStates = true)
        {
            if (documentBaseIds?.Any() != true)
            {
                return Task.FromResult(new List<TaxPostingOsnoDto>());
            }

            return PostAsync<IReadOnlyCollection<long>, List<TaxPostingOsnoDto>>(
                $"/TaxPostingsOsno/ByDocumentIds?firmId={firmId}&userId={userId}&filterBadOperationStates={filterBadOperationStates}", 
                documentBaseIds);
        }

        public Task<List<TaxPostingOsnoDto>> GetByPeriodAsync(
            int firmId,
            int userId,
            DateTime startDate,
            DateTime endDate,
            bool filterBadOperationStates = true,
            NormalizedCostType? excludeNormalizedCostType = null,
            int? limit = null)
        {
            return GetAsync<List<TaxPostingOsnoDto>>(
                "/TaxPostingsOsno/ByPeriod",
                new
                {
                    firmId,
                    userId,
                    startDate,
                    endDate,
                    filterBadOperationStates,
                    excludeNormalizedCostType,
                    limit
                }
            );
        }

        public Task UpdateNormalizedSumForQuarterAsync(int firmId, int userId, IReadOnlyCollection<OsnoNormalizedSumForQuarterDto> request)
        {
            if (request is not { Count:> 0 })
            {
                return Task.CompletedTask;
            }

            return PostAsync(
                $"/TaxPostingsOsno/UpdateNormalizedSumForQuarter?firmId={firmId}&userId={userId}", 
                request);
        }
    }
}