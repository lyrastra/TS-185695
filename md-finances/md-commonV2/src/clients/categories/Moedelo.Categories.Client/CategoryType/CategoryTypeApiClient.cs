using Moedelo.Categories.Dto;
using Moedelo.Categories.Dto.CategoryType;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Outsource.Dto;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.Categories.Client.CategoryType
{
    [InjectAsSingleton]
    public sealed class CategoryTypeApiClient : BaseApiClient, ICategoryTypeApiClient
    {
        private readonly SettingValue apiEndPoint;
        private const string Endpoint = "/v1/categories/types";

        public CategoryTypeApiClient(
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

        public async Task<CategoryTypeDto> InsertAsync(string name, int accountId)
        {
            if (name is null)
                throw new ArgumentNullException(nameof(name));

            var response = await PostAsync<CategoryTypePostDto, ApiDataResponseDto<CategoryTypeDto>>(
                    Endpoint,
                    new CategoryTypePostDto(name, accountId)
            ).ConfigureAwait(false);

            return response?.data;
        }

        protected override string GetApiEndpoint() => apiEndPoint.Value;
    }
}
