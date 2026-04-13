using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.StockV2.Dto.Products;

namespace Moedelo.StockV2.Client.Products
{
    [InjectAsSingleton]
    public class ProductTypeCodeApiClient : BaseApiClient, IProductTypeCodeApiClient
    {
        private readonly ISettingRepository settingRepository;

        public ProductTypeCodeApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator,
            IResponseParser responseParser, 
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager, 
            ISettingRepository settingRepository) :
            base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            this.settingRepository = settingRepository;
        }

        protected override string GetApiEndpoint()
        {
            return this.settingRepository.Get("StockApiEndpoint").Value;
        }

        public Task<ProductTypeCodeDto[]> GetAsync()
        {
            return GetAsync<ProductTypeCodeDto[]>($"/ProductTypeCode");
        }

        public Task<ProductTypeCodeDto[]> GetByIdsAsync(int firmId, int userId, IReadOnlyCollection<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Task.FromResult(Array.Empty<ProductTypeCodeDto>());
            }

            return PostAsync<IReadOnlyCollection<int>, ProductTypeCodeDto[]>(
                $"/ProductTypeCode/GetByIds?firmId={firmId}&userId={userId}", ids);
        }

        public Task AddAsync(IReadOnlyCollection<ProductTypeCodeCreateDto> codes, HttpQuerySetting setting = null)
        {
            if (codes == null || !codes.Any())
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/ProductTypeCode/Add", codes, setting: setting ?? new HttpQuerySetting(TimeSpan.FromSeconds(120)));
        }

        public Task SetOutdatedAsync(IReadOnlyCollection<int> ids)
        {
            if (ids == null || !ids.Any())
            {
                return Task.CompletedTask;
            }

            return PutAsync($"/ProductTypeCode/SetOutdated", ids);
        }
    }
}
