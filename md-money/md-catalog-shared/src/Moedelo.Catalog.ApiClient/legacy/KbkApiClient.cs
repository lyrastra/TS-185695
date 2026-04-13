using Microsoft.Extensions.Logging;
using Moedelo.Catalog.ApiClient.Abstractions.legacy;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Catalog.ApiClient.Enums;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Catalog.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IKbkApiClient))]
    internal sealed class KbkApiClient : BaseLegacyApiClient, IKbkApiClient
    {

        public KbkApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<KbkApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("CatalogApiEndpoint"),
                logger)
        {
        }

        public async Task<KbkDto[]> GetAsync()
        {
            return await GetAsync<KbkDto[]>("/Kbk/V2/GetAll").ConfigureAwait(false);
        }

        public async Task<KbkDto> GetAsync(int id)
        {
            var kbk = await GetAsync().ConfigureAwait(false);
            return kbk?.FirstOrDefault(x => x.Id == id);
        }

        public async Task<KbkDto[]> GetByIdsAsync(IReadOnlyCollection<int> ids)
        {
            var kbk = await GetAsync().ConfigureAwait(false);
            return kbk?.Where(x => ids.Contains(x.Id)).ToArray() ?? Array.Empty<KbkDto>();
        }

        public async Task<KbkDto> GetAsync(string number, DateTime date)
        {
            var kbk = await GetAsync().ConfigureAwait(false);
            return kbk?.FirstOrDefault(x => x.Number == number && x.StartDate <= date && x.EndDate >= date);
        }

        public async Task<KbkDto> GetByTypeAsync(KbkType kbkType,
            KbkPaymentType kbkPaymentType = KbkPaymentType.Payment)
        {
            var kbk = await GetAsync().ConfigureAwait(false);
            return kbk?.FirstOrDefault(x => x.KbkType == kbkType && x.KbkPaymentType == kbkPaymentType);
        }
    }
}