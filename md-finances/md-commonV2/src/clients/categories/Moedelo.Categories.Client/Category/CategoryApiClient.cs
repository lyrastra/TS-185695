using Moedelo.Categories.Dto;
using Moedelo.Categories.Dto.Category;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Outsource.Dto;
using System;
using System.Threading.Tasks;

namespace Moedelo.Categories.Client.Category
{
    [InjectAsSingleton]
    public sealed class CategoryApiClient : BaseApiClient, ICategoryApiClient
    {
        private readonly SettingValue apiEndPoint;
        private const string Endpoint = "/v1/categories";

        public CategoryApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            ISettingRepository settingsRepository,
            IAuditScopeManager auditScopeManager
        )
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingsRepository.Get("OutsourceTaskApiEndpoint");
        }

        public async Task<CategoryDto> InsertAsync(CategoryPostDto dto)
        {
            if (dto is null)
                throw new ArgumentNullException(nameof(dto));

            var response = await PostAsync<CategoryPostDto, ApiDataResponseDto<CategoryDto>>(Endpoint, dto).ConfigureAwait(false);

            return response?.data;
        }

        protected override string GetApiEndpoint() => apiEndPoint.Value;
    }
}
