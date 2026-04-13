using System;
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
using Moedelo.Stock.ApiClient.Abstractions.legacy;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.Balance;

namespace Moedelo.Stock.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IStockClosingPeriodValidationApiClient))]
    internal sealed class StockClosingPeriodValidationApiClient : BaseLegacyApiClient, IStockClosingPeriodValidationApiClient
    {
        public StockClosingPeriodValidationApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<StockClosingPeriodValidationApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("StockServiceUrl"),
                logger)
        {
        }
        public Task<IReadOnlyList<ProductBalanceInfoDto>> GetStockProductNegativeBalancesAsync(FirmId firmId, UserId userId, DateTime startDate, DateTime endDate)
        {
            return GetAsync<IReadOnlyList<ProductBalanceInfoDto>>("/ClosingPeriodValidation/GetStockProductNegativeBalances",
                new { firmId = (int)firmId, userId = (int)userId, startDate, endDate },
                setting: new HttpQuerySetting(TimeSpan.FromSeconds(60)));
        }

    }
}