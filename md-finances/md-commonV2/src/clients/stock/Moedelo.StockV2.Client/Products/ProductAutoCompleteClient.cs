using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.StockV2.Dto.Products;

namespace Moedelo.StockV2.Client.Products
{
    [InjectAsSingleton]
    public class ProductAutoCompleteClient : BaseApiClient, IProductAutoCompleteClient
    {
        private readonly SettingValue apiEndPoint;

        public ProductAutoCompleteClient(
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

        public Task<List<ProductAutoCompleteDto>> GetAsync(int firmId, int userId, ProductAutoCompleteRequestDto request)
        {
            return PostAsync<ProductAutoCompleteRequestDto, List<ProductAutoCompleteDto>>(
                $"/Product/AutoComplete/GetForDocument?firmId={firmId}&userId={userId}",
                request);
        }
    }
}