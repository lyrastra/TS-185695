using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Stock.ApiClient.Abstractions.legacy;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moedelo.Stock.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IProductTypeCodeApiClient))]
    internal sealed class ProductTypeCodeApiClient : BaseLegacyApiClient, IProductTypeCodeApiClient
    {
        public ProductTypeCodeApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<ProductApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("StockApiEndpoint"),
                logger)
        {
        }

        public async Task<ProductTypeCodeDto[]> GetByIdsAsync(FirmId firmId, UserId userId, IReadOnlyCollection<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Array.Empty<ProductTypeCodeDto>();
            }

            var response = await PostAsync<IReadOnlyCollection<int>, ProductTypeCodeDto[]>(
                $"/ProductTypeCode/GetByIds?firmId={firmId}&userId={userId}",
                ids).ConfigureAwait(false);

            return response;
        }
    }
}