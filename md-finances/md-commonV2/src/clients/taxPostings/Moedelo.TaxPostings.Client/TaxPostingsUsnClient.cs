using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.TaxPostings.Dto;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.TaxPostings.Client
{
    [InjectAsSingleton]
    public class TaxPostingsUsnClient : BaseApiClient, ITaxPostingsUsnClient
    {
        private readonly SettingValue apiEndpoint;
        
        public TaxPostingsUsnClient(
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

        public Task DeleteAsync(int firmId, int userId, long documentBaseId)
        {
            return DeleteAsync($"/TaxPostingsUsn?firmId={firmId}&userId={userId}&baseId={documentBaseId}");
        }
        
        public Task DeleteByRelatedDocumentAsync(int firmId, int userId, long documentBaseId)
        {
            return DeleteAsync($"/TaxPostingsUsn/ByRelatedDocument/{documentBaseId}?firmId={firmId}&userId={userId}");
        }
        
        public Task DeleteByRelatedDocumentsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }
            
            return DeleteByRequestAsync($"/TaxPostingsUsn/ByRelatedDocuments?firmId={firmId}&userId={userId}", documentBaseIds);
        }

        public Task<List<DocumentTaxSumDto>> GetDocumentTaxSumsAsync(int firmId, int userId, IReadOnlyCollection<long> baseIdList)
        {
            if (baseIdList?.Count > 0)
            {
                return PostAsync<IReadOnlyCollection<long>, List<DocumentTaxSumDto>>(
                    $"/TaxPostingsUsn/GetDocumentTaxSums?firmId={firmId}&userId={userId}", baseIdList);
            }
            return Task.FromResult(new List<DocumentTaxSumDto> ());
		}

        public Task<List<TaxPostingUsnDto>> GetByDocumentIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds?.Any() != true)
            {
                return Task.FromResult(new List<TaxPostingUsnDto>());
            }

            return PostAsync<IReadOnlyCollection<long>, List<TaxPostingUsnDto>>(
                $"/TaxPostingsUsn/ByDocumentIds?firmId={firmId}&userId={userId}",
                documentBaseIds);
        }

        public Task<List<TaxPostingUsnDto>> GetByPeriodsAsync(int firmId, int userId, IReadOnlyCollection<PeriodRequestDto> periods)
        {
            if (periods?.Any() != true)
            {
                return Task.FromResult(new List<TaxPostingUsnDto>());
            }

            return PostAsync<IReadOnlyCollection<PeriodRequestDto>, List<TaxPostingUsnDto>>(
                $"/TaxPostingsUsn/GetByPeriods?firmId={firmId}&userId={userId}",
                periods);
        }

        public Task SaveAsync(int firmId, int userId, IReadOnlyCollection<TaxPostingUsnDto> taxPostings)
        {
            if (taxPostings == null || !taxPostings.Any())
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/TaxPostingsUsn/Save?firmId={firmId}&userId={userId}", taxPostings);
        }
    }
}