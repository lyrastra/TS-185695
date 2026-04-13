using Moedelo.Documents.Dto.DocumentTypes;
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

namespace Moedelo.Documents.Client.DocumentTypes
{
    [InjectAsSingleton]
    public class DocumentTypeApiClient : BaseApiClient, IDocumentTypeApiClient
    {
        private readonly SettingValue apiEndPoint;
        private const string Endpoint = "/v1/types";

        public DocumentTypeApiClient(
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

        public async Task<DocumentTypeDto> GetAsync(int accountId, int id)
        {
            try
            {
                var response = await GetAsync<ApiDataResponseDto<DocumentTypeDto>>($"{Endpoint}/{id}", new { accountId })
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


        public async Task<DocumentTypeDto> CreateAsync(DocumentTypePostDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var response = await PostAsync<DocumentTypePostDto, ApiDataResponseDto<DocumentTypeDto>>(Endpoint, dto)
                .ConfigureAwait(false);

            return response?.data;
        }

        protected override string GetApiEndpoint() => apiEndPoint.Value;
    }
}
