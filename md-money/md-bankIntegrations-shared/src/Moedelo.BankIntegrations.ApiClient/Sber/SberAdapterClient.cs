using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.Sber;
using Moedelo.BankIntegrations.ApiClient.Dto.Acceptance.Sber;
using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.Dto.Movements;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Sber
{
    [InjectAsSingleton(typeof(ISberAdapterClient))]
    public class SberAdapterClient : BaseApiClient, ISberAdapterClient
    {
        public SberAdapterClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SberAdapterClient> logger) : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("SberbankCryptoEndpoint"),
                logger)
        {
        }

        public async Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto, CancellationToken cancellationToken)
        {
            var response = await PostAsync<RequestMovementRequestDto, ApiDataResult<RequestMovementResponseDto>>(
                uri: "/BankOperation/RequestMovements",
                data: requestDto,
                cancellationToken: cancellationToken);

            return response.data;
        }

        public async Task<AcceptanceAdvanceResponseDto> CreateAcceptanceAdvanceAsync(AcceptanceAdvanceRequestDto requestDto, CancellationToken cancellationToken)
        {
            var response = await PostAsync<AcceptanceAdvanceRequestDto, ApiDataResult<AcceptanceAdvanceResponseDto>>(
                uri: "/Acceptance/CreateAcceptanceAdvance",
                data: requestDto,
                cancellationToken: cancellationToken);

            return response.data;
        }

        public async Task<AcceptanceAdvanceResponseDto> GetAcceptanceAdvanceAsync(int firmId, string externalId, CancellationToken cancellationToken)
        {
            var response = await GetAsync<ApiDataResult<AcceptanceAdvanceResponseDto>>(
                uri: "/Acceptance/GetAcceptanceAdvance",
                queryParams: new { firmId, externalId },
                cancellationToken: cancellationToken);

            return response.data;
        }

        public async Task<AcceptanceAdvanceStateResponseDto> GetAcceptanceAdvanceStateAsync(int firmId, string externalId, CancellationToken cancellationToken)
        {
            var response = await GetAsync<ApiDataResult<AcceptanceAdvanceStateResponseDto>>(
                uri: "/Acceptance/GetAcceptanceAdvanceState",
                queryParams: new { firmId, externalId },
                cancellationToken: cancellationToken);

            return response.data;
        }
    }
}
