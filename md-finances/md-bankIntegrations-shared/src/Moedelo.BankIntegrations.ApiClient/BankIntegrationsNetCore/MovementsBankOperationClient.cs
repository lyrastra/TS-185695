using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.BankIntegrationsNetCore;
using Moedelo.BankIntegrations.ApiClient.Dto.BankOperation;
using Moedelo.BankIntegrations.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.BankIntegrationsNetCore
{
    [InjectAsSingleton(typeof(IMovementsBankOperationClient))]
    public class MovementsBankOperationClient : BaseApiClient, IMovementsBankOperationClient
    {
        private const string RequestMovementsEndpoint = "/private/api/v1/BankOperation";

        public MovementsBankOperationClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<MovementsBankOperationClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("IntegrationMovementsApi"),
                logger)
        {
        }

        public async Task<RequestMovementListResponseDto> RequestMovementsAsync(
            RequestMovementListRequestDto dto,
            CancellationToken cancellationToken = default)
        {
            var response = await PostAsync<RequestMovementListRequestDto, ApiDataResult<RequestMovementListResponseDto>>(
                $"{RequestMovementsEndpoint}/RequestMovements",
                dto,
                cancellationToken: cancellationToken);

            return response.data;
        }
    }
}
