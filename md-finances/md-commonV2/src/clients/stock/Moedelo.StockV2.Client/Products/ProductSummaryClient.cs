using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.StockV2.Dto.Products;

namespace Moedelo.StockV2.Client.Products
{
    [InjectAsSingleton]
    public class ProductSummaryClient : BaseApiClient, IProductSummaryClient
    {
        private readonly SettingValue apiEndPoint;

        public ProductSummaryClient(
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

        public Task<List<FirmProductSaleSummaryDto>> GetProductTopSalesAsync(
            IReadOnlyCollection<int> firmIds,
            int topCapacity,
            DateTime startDate,
            DateTime endDate)
        {
            var dto = new ProductSummaryRequestDto
            {
                FirmIds = firmIds,
                Count = topCapacity,
                StartDate = startDate,
                EndDate = endDate
            };
            return PostAsync<ProductSummaryRequestDto, List<FirmProductSaleSummaryDto>>("/ProductSummary/GetProductTopSales", dto);
        }
    }
}
