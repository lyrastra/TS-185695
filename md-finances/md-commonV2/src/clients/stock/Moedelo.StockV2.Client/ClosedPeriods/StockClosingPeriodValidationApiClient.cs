using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.StockV2.Dto.ClosingPeriodValidations;
using Moedelo.StockV2.Dto.Products;

namespace Moedelo.StockV2.Client.ClosedPeriods
{
    [InjectAsSingleton]
    public class StockClosingPeriodValidationApiClient : BaseApiClient, IStockClosingPeriodValidationApiClient
    {
        private readonly SettingValue apiEndPoint;

        public StockClosingPeriodValidationApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : 
            base(
                httpRequestExecutor, 
                uriCreator, 
                responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("StockServiceUrl");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<List<StockOperationsCountDto>> GetOperationsNonProvidedInAccountingAsync(
            int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            const string uri = "/ClosingPeriodValidation/OperationsNonProvidedInAccounting";
            var queryParams = new {firmId, userId, startDate, endDate};
            return GetAsync<List<StockOperationsCountDto>>(uri, queryParams);
        }
        
        public Task<List<InventoryWithBundleIncomesDto>> GetInventoriesWithBundleIncomesAsync(int firmId, int userId, DateTime beforeDate)
        {
            return GetAsync<List<InventoryWithBundleIncomesDto>> (
                "/ClosingPeriodValidation/GetInventoriesWithBundleIncomes", 
                new { firmId, userId, beforeDate });
        }
        
        public Task<List<ProductBalanceInfoDto>> GetStockProductNegativeBalancesAsync(int firmId, int userId, DateTime startDate, DateTime endDate)
        {
            return GetAsync<List<ProductBalanceInfoDto>>("/ClosingPeriodValidation/GetStockProductNegativeBalances",
                new {firmId, userId, startDate, endDate},
                setting: new HttpQuerySetting
                {
                    Timeout = TimeSpan.FromSeconds(45)
                });
        }

        public Task<List<ProductBalanceInfoDto>> GetStockProductNegativeBalanceUsingMaterialsAsync(int firmId, int userId,
            DateTime startDate, DateTime endDate)
        {
            return GetAsync<List<ProductBalanceInfoDto>>("/ClosingPeriodValidation/GetStockProductNegativeBalanceUsingMaterials",
                new { firmId, userId, startDate, endDate });
        }

        public Task<List<ProductBalanceInfoDto>> GetProductBalancesOnDateAsync(int firmId, int userId, ProductBalancesRequestDto request)
        {
            return PostAsync<ProductBalancesRequestDto, List<ProductBalanceInfoDto>>($"/ClosingPeriodValidation/GetProductBalancesOnDate?firmId={firmId}&userId={userId}", request);
        }
    }
}