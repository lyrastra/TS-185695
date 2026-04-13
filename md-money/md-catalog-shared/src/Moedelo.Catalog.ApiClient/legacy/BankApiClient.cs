using System;
using System.Collections.Generic;
using Moedelo.Catalog.ApiClient.Abstractions.legacy;
using Moedelo.Catalog.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Infrastructure.System.Extensions.Collections;

namespace Moedelo.Catalog.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IBankApiClient))]
    internal sealed class BankApiClient : BaseLegacyApiClient, IBankApiClient
    {
        public BankApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<BankApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("CatalogApiEndpoint"),
                logger)
        {
        }

        public Task<BankDto[]> GetByIdsAsync(IReadOnlyCollection<int> ids, CancellationToken cancellationToken = default)
        {
            return ids.NullOrEmpty()
                ? Task.FromResult(Array.Empty<BankDto>())
                : PostAsync<IReadOnlyCollection<int>, BankDto[]>("/Banks/V2/GetByIds",
                    ids.ToDistinctReadOnlyCollection(), cancellationToken: cancellationToken);
        }

        public async Task<BankDto> GetByBikAsync(string bik)
        {
            var response = await PostAsync<string[], IReadOnlyCollection<BankDto>>("/Banks/V2/GetByBiks", new[] {bik})
                .ConfigureAwait(false);
            return response.FirstOrDefault();
        }

        public Task<BankDto[]> GetByBiksAsync(IReadOnlyCollection<string> biks)
        {
            return biks.NullOrEmpty()
                ? Task.FromResult(Array.Empty<BankDto>())
                : PostAsync<IReadOnlyCollection<string>, BankDto[]>("/Banks/V2/GetByBiks",
                    biks.ToDistinctReadOnlyCollection());
        }

        public Task<BankDto[]> GetByInnsAsync(IReadOnlyCollection<string> inns)
        {
            return inns.NullOrEmpty()
                ? Task.FromResult(Array.Empty<BankDto>())
                : PostAsync<IReadOnlyCollection<string>, BankDto[]>("/Banks/V2/GetByInns",
                    inns.ToDistinctReadOnlyCollection());
        }
    }
}