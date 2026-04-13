using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.System.Extensions.Collections;
using Moedelo.Konragents.ApiClient.Abstractions.legacy;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos;
using Moedelo.Konragents.ApiClient.legacy.models;

namespace Moedelo.Konragents.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IKontragentContactApiClient))]
    internal sealed class KontragentContactApiClient : BaseLegacyApiClient, IKontragentContactApiClient
    {
        public KontragentContactApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<KontragentContactApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("KontragentsPrivateApiEndpoint"),
                logger)
        {
        }

        public async Task<List<KontragentContactDto>> GetByKontragentsAsync(FirmId firmId, UserId userId,
            IReadOnlyCollection<int> ids)
        {
            if (ids?.Any() != true)
            {
                return new List<KontragentContactDto>();
            }

            var result = await PostAsync<IReadOnlyCollection<int>, DataResult<List<KontragentContactDto>>>(
                    $"/Contact/GetByKontragents?firmId={firmId}&userId={userId}", ids.ToDistinctReadOnlyCollection())
                .ConfigureAwait(false);

            return result.Data;
        }
    }
}