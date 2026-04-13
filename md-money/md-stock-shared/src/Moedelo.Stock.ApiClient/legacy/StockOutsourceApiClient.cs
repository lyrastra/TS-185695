using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Stock.ApiClient.Abstractions.legacy;
using Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.Outsouce;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.Stock.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IStockOutsourceApiClient))]
    internal sealed class StockOutsourceApiClient : BaseLegacyApiClient, IStockOutsourceApiClient
    {
        public StockOutsourceApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<StockOutsourceApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("StockApiEndpoint"),
                logger)
        {
        }

        public Task<OutsourceProductOnStockBalanceDto[]> GetProductsBalancesHistoryAsync(
            FirmId firmId, OutsourceProductsBlancesHistoryRequestDto requestDto)
        {
            return PostAsync<OutsourceProductsBlancesHistoryRequestDto, OutsourceProductOnStockBalanceDto[]>(
                $"/Stock/Outsource/GetProductsBalancesHistory?firmId={firmId}", requestDto);
        }

        public Task<OutsourceProductInfoDto[]> GetProductsInfoAsync(FirmId firmId, IReadOnlyCollection<long> productIds)
        {
            return PostAsync<IReadOnlyCollection<long>, OutsourceProductInfoDto[]>(
                $"/Stock/Outsource/GetProductsInfo?firmId={firmId}", productIds);
        }

        public Task<OutsourceProductDto[]> GetProductsAsync(FirmId firmId)
        {
            return PostAsync<OutsourceProductDto[]>(
                $"/Stock/Outsource/GetProducts?firmId={firmId}");
        }

        public Task<OutsourceStockInfoDto[]> GetStocksInfoAsync(FirmId firmId, IReadOnlyCollection<long> stockIds)
        {
            return PostAsync<IReadOnlyCollection<long>, OutsourceStockInfoDto[]>(
                $"/Stock/Outsource/GetStocksInfo?firmId={firmId}", stockIds);
        }

        public Task<OutsourceGetProductSynonymsResponseDto> GetProductSynonymsAsync(
            OutsourceGetProductSynonymsRequestDto requestDto)
        {
            return PostAsync<OutsourceGetProductSynonymsRequestDto, OutsourceGetProductSynonymsResponseDto>(
                "/Stock/Outsource/GetProductSynonyms", requestDto);
        }
    }
}