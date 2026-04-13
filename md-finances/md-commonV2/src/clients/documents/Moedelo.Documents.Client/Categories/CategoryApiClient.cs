using Moedelo.Documents.Dto.Categories;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Exceptions.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Outsource.Dto;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Moedelo.Documents.Client.Categories
{
    [InjectAsSingleton]
    public class CategoryApiClient : BaseApiClient, ICategoryApiClient
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
            apiEndPoint = settingsRepository.Get("OutsourceDocumentApiEndpoint");
        }

        public async Task<CategoryDto> GetAsync(int accountId, int id)
        {

            try
            {
                var response = await GetAsync<ApiDataResponseDto<CategoryDto>>($"{Endpoint}/{id}", new { accountId })
                    .ConfigureAwait(false);

                return response?.data;
            }
            catch (HttpRequestResponseStatusException e) when (e.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task<CategoryDto> CreateAsync(CategoryPostDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var response = await PostAsync<CategoryPostDto, ApiDataResponseDto<CategoryDto>>(Endpoint, dto)
                .ConfigureAwait(false);

            return response?.data;
        }

        protected override string GetApiEndpoint() => apiEndPoint.Value;
    }

}
