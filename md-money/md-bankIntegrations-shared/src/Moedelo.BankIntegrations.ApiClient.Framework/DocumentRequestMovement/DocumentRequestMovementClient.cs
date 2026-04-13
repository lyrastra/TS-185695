using Moedelo.BankIntegrations.ApiClient.Dto.MovementHash;
using Moedelo.BankIntegrations.ApiClient.Dto.MovementHashService;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.DocumentRequestMovement;
using Moedelo.BankIntegrations.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Framework.IntegrationMonitor
{
    [InjectAsSingleton]
    public class DocumentRequestMovementClient : BaseApiClient, IDocumentRequestMovementClient
    {
        private const string ControllerName = "/private/api/v1/DocumentRequestMovement/";
        private readonly SettingValue apiEndPoint;

        public DocumentRequestMovementClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                auditTracer,
                auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("IntegrationApiNetCore");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        /// <summary>Удаление из документа документов которые ранее были загруженны через ночные выписки</summary>
        public async Task<RemoveDuplicateDocumentsResponseDto> RemoveDuplicateDocumentsAsync(RemoveDuplicateDocumentsRequestDto requestDto, int firmId, int partnerId)
        {
            var response = await PostAsync<RemoveDuplicateDocumentsRequestDto, ApiDataResult<RemoveDuplicateDocumentsResponseDto>>(
                $"{ControllerName}/RemoveDuplicateDocuments",
                requestDto
            ).ConfigureAwait(false);

            return response.data;
        }

        public Task HashRequestsMovementsSaveAsync(List<MovementHashDto> movementHashDtos)
        {
            return PostAsync<List<MovementHashDto>>($"{ControllerName}/HashRequestsMovementsSave", movementHashDtos);
        }
    }
}