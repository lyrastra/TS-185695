using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.TaxPostings.ApiClient.Abstractions.Legacy;
using Moedelo.TaxPostings.ApiClient.Abstractions.Legacy.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.TaxPostings.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(ITaxPostingsUsnClient))]
    internal sealed class TaxPostingsUsnClient : BaseLegacyApiClient, ITaxPostingsUsnClient
    {
        public TaxPostingsUsnClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<TaxPostingsUsnClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("TaxPostingsApiEndpoint"),
                logger
            )
        {
        }

        public Task<TaxPostingUsnDto[]> GetByDocumentIdAsync(FirmId firmId, UserId userId, long documentBaseId)
        {
            var uri = $"/TaxPostingsUsn/{documentBaseId}?firmId={firmId}&userId={userId}";
            return GetAsync<TaxPostingUsnDto[]>(uri);
        }

        public Task<TaxPostingUsnDto[]> GetByDocumentIdsAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<long> documentBaseIds)
        {
            var uri = $"/TaxPostingsUsn/ByDocumentIds?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyCollection<long>, TaxPostingUsnDto[]>(uri, documentBaseIds);
        }

        public Task<TaxPostingUsnDto[]> GetByPeriodAsync(FirmId firmId, UserId userId, PeriodRequestDto period)
        {
            var uri = $"/TaxPostingsUsn/GetByPeriods?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyCollection<PeriodRequestDto>, TaxPostingUsnDto[]>(uri, new[] { period });
        }
        
        public Task<TaxPostingUsnByFirmDto[]> GetByPeriodAndFirmIdsAsync(IReadOnlyCollection<PeriodRequestByFirmDto> periodByFirmsDto, 
            HttpQuerySetting setting = null)
        {
            var uri = "/TaxPostingsUsn/ByPeriodAndFirmIds";
            return PostAsync<IReadOnlyCollection<PeriodRequestByFirmDto>, TaxPostingUsnByFirmDto[]>(uri, periodByFirmsDto, setting: setting);
        }

        public Task SaveAsync(FirmId firmId, UserId userId, IReadOnlyCollection<TaxPostingUsnDto> taxPostings)
        {
            var uri = $"/TaxPostingsUsn/Save?firmId={firmId}&userId={userId}";
            return PostAsync(uri, taxPostings);
        }

        public Task DeleteAsync(FirmId firmId, UserId userId, long documentBaseId)
        {
            var uri = $"/TaxPostingsUsn?firmId={firmId}&userId={userId}&baseId={documentBaseId}";
            return DeleteAsync(uri);
        }

        public Task DeleteByRelatedDocumentIdAsync(FirmId firmId, UserId userId, long documentBaseId)
        {
            var uri = $"/TaxPostingsUsn/ByRelatedDocument/{documentBaseId}?firmId={firmId}&userId={userId}";
            return DeleteAsync(uri);
        }

        public Task DeleteByRelatedDocumentsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }
            
            return DeleteByRequestAsync($"/TaxPostingsUsn/ByRelatedDocuments?firmId={firmId}&userId={userId}", documentBaseIds);
        }

        public Task DeleteByRelatedDocumentIdNotInDocumentIdAsync(FirmId firmId, UserId userId, long documentBaseId)
        {
            var uri = $"/TaxPostingsUsn/ByRelatedDocumentIdNotInDocumentId?firmId={firmId}&userId={userId}&baseId={documentBaseId}";
            return DeleteAsync(uri);
        }
    }
}