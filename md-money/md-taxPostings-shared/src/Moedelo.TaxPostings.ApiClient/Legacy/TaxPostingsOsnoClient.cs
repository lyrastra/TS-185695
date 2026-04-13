using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.TaxPostings.ApiClient.Abstractions.Legacy;
using Moedelo.TaxPostings.ApiClient.Abstractions.Legacy.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Types;

namespace Moedelo.TaxPostings.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(ITaxPostingsOsnoClient))]
    internal sealed class TaxPostingsOsnoClient : BaseLegacyApiClient, ITaxPostingsOsnoClient
    {
        public TaxPostingsOsnoClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<TaxPostingsOsnoClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("TaxPostingsApiEndpoint"),
                logger
            )
        {
        }

        public Task<List<TaxPostingOsnoDto>> GetByBaseIdAsync(FirmId firmId, UserId userId, long baseId)
        {
            var uri = $"/TaxPostingsOsno/GetByBaseId?firmId={firmId}&userId={userId}&baseId={baseId}";
            return GetAsync<List<TaxPostingOsnoDto>>(uri);
        }

        public Task SaveAsync(FirmId firmId, UserId userId, IReadOnlyCollection<TaxPostingOsnoDto> taxPostings)
        {
            var uri = $"/TaxPostingsOsno/Save?firmId={firmId}&userId={userId}";
            return PostAsync(uri, taxPostings);
        }

        public Task DeleteAsync(FirmId firmId, UserId userId, long baseId)
        {
            var uri = $"/TaxPostingsOsno/Delete?firmId={firmId}&userId={userId}&baseId={baseId}";
            return PostAsync(uri);
        }
    }
}