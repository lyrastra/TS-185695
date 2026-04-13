using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Docs.Dto.ProductsTrace;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Docs.Client.ProductsTrace
{
    [InjectAsSingleton(typeof(IProductTraceApiClient))]
    public class ProductTraceApiClient : BaseApiClient, IProductTraceApiClient
    {
        private readonly SettingValue apiEndpoint;

        public ProductTraceApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("DocsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<Dictionary<long, List<ProductTraceResponseDto>>> GetByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds?.Any() != true)
            {
                return Task.FromResult(new Dictionary<long, List<ProductTraceResponseDto>>());
            }

            return PostAsync<IReadOnlyCollection<long>, Dictionary<long, List<ProductTraceResponseDto>>>($"/ProductTrace/GetByBaseIds?firmId={firmId}&userId={userId}", documentBaseIds);
        }

        public Task<IReadOnlyCollection<ProductTraceResponseDto>> GetByBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            return GetAsync<IReadOnlyCollection<ProductTraceResponseDto>>($"/ProductTrace/GetByBaseId?firmId={firmId}&userId={userId}&documentBaseId={documentBaseId}");
        }

        public Task DeleteByBaseIdsAsync(int firmId, int userId, IReadOnlyCollection<long> documentBaseIds)
        {
            if (documentBaseIds?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/ProductTrace/DeleteByBaseIds?firmId={firmId}&userId={userId}", documentBaseIds);
        }

        public Task DeleteByBaseIdAsync(int firmId, int userId, long documentBaseId)
        {
            return PostAsync($"/ProductTrace/DeleteByBaseId?firmId={firmId}&userId={userId}&documentBaseId={documentBaseId}");
        }

        public Task SaveAsync(int firmId, int userId, IReadOnlyCollection<ProductTraceSaveDto> dtos)
        {
            if (dtos?.Any() != true)
            {
                return Task.CompletedTask;
            }

            return PostAsync($"/ProductTrace/Save?firmId={firmId}&userId={userId}", dtos);
        }

        public Task<bool> ProductHasTraceAsync(int firmId, int userId, long productId)
        {
            return GetAsync<bool>($"/ProductTrace/ProductHasTrace?firmId={firmId}&userId={userId}&productId={productId}");
        }
    }
}