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
    [InjectAsSingleton(typeof(IActivityCategoryApiClient))]
    internal sealed class ActivityCategoryApiClient : BaseLegacyApiClient, IActivityCategoryApiClient
    {
        public ActivityCategoryApiClient(
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

        public Task<IReadOnlyCollection<ActivityCategoryDto>> GetByCodesAsync(IReadOnlyCollection<string> codes)
        {
            return codes.NullOrEmpty()
                ? Task.FromResult<IReadOnlyCollection<ActivityCategoryDto>>(Array.Empty<ActivityCategoryDto>())
                : PostAsync<IReadOnlyCollection<string>, IReadOnlyCollection<ActivityCategoryDto>>(
                    "/ActivityCategory/V2/GetByCodes",
                    codes.ToDistinctReadOnlyCollection());
        }

        public async Task<IReadOnlyCollection<ActivityCategoryDto>> GetByIdsAsync(IReadOnlyCollection<int> ids,
            CancellationToken cancellationToken)
        {
            if (ids?.Any() != true)
            {
                return [];
            }

            return await PostAsync<IReadOnlyCollection<int>, IReadOnlyCollection<ActivityCategoryDto>>(
                "/ActivityCategory/V2/GetByIdList", ids, cancellationToken: cancellationToken);
        }
    }
}