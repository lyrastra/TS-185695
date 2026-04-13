using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.Sovcom;
using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.Dto.Movements;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Threading.Tasks;

namespace Moedelo.BankIntegrations.ApiClient.Sovcom
{
    [InjectAsSingleton]
    public class SovcomBankAdapterClient : BaseApiClient, ISovcomBankAdapterClient
    {
        public SovcomBankAdapterClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SovcomBankAdapterClient> logger) : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("SovcombankAdapterEndpoint"),
                logger)
        {
        }

        public async Task<RequestMovementResponseDto> RequestMovementListAsync(RequestMovementRequestDto requestDto)
        {
            var response = await PostAsync<RequestMovementRequestDto, ApiDataResult<RequestMovementResponseDto>>(
                uri: "/BankOperation/RequestMovements",
                data: requestDto);

            return response.data;
        }

    }
}
