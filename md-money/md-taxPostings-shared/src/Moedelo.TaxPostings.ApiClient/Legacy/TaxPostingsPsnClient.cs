using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.TaxPostings.ApiClient.Abstractions.Legacy;
using Moedelo.TaxPostings.ApiClient.Abstractions.Legacy.Dto;

namespace Moedelo.TaxPostings.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(ITaxPostingsPsnClient))]
    internal sealed class TaxPostingsPsnClient : BaseLegacyApiClient, ITaxPostingsPsnClient
    {
        public TaxPostingsPsnClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<TaxPostingsPsnClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("TaxPostingsApiEndpoint"),
                logger
            )
        {
        }

        public Task<TaxPostingPsnDto[]> GetByBaseIdAsync(FirmId firmId, UserId userId, long documentBaseId)
        {
            var uri = $"/TaxPostingsPsn/ByDocument/{documentBaseId}?firmId={firmId}&userId={userId}";
            return GetAsync<TaxPostingPsnDto[]>(uri);
        }

        public Task<TaxPostingPsnDto[]> GetByRelatedDocumentAsync(FirmId firmId, UserId userId, long documentBaseId)
        {
            var uri = $"/TaxPostingsPsn/ByRelatedDocument/{documentBaseId}?firmId={firmId}&userId={userId}";
            return GetAsync<TaxPostingPsnDto[]>(uri);
        }

        public Task SaveAsync(FirmId firmId, UserId userId, IReadOnlyCollection<TaxPostingPsnDto> taxPostings)
        {
            var uri = $"/TaxPostingsPsn/Save?firmId={firmId}&userId={userId}";
            return PostAsync(uri, taxPostings);
        }

        public Task DeleteByRelatedDocumentAsync(FirmId firmId, UserId userId, long documentBaseId)
        {
            var uri = $"/TaxPostingsPsn/ByRelatedDocument/{documentBaseId}?firmId={firmId}&userId={userId}";
            return DeleteAsync(uri);
        }
        
        public Task<TaxPostingsPsnByFirmDto[]> GetByPeriodAndFirmIdsAsync(TaxPostingsQuery query, HttpQuerySetting setting = null)
        {
            const string uri = "/TaxPostingsPsn/ByPeriodAndFirmIds";
            return PostAsync<TaxPostingsQuery, TaxPostingsPsnByFirmDto[]>(uri, query, setting: setting);
        }
    }
}