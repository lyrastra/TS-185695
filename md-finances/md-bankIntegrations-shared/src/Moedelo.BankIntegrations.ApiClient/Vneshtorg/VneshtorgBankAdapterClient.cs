using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.Vneshtorg;
using Moedelo.BankIntegrations.Dto;
using Moedelo.BankIntegrations.Dto.Movements;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.BankIntegrations.ApiClient.Vneshtorg;

[InjectAsSingleton(typeof(IVneshtorgBankAdapterClient))]
public class VneshtorgBankAdapterClient : BaseApiClient, IVneshtorgBankAdapterClient
{
    public VneshtorgBankAdapterClient(
        IHttpRequestExecuter httpRequestExecuter, 
        IUriCreator uriCreator, 
        IAuditTracer auditTracer, 
        IAuthHeadersGetter authHeadersGetter, 
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository, 
        ILogger<VneshtorgBankAdapterClient> logger) : base(
        httpRequestExecuter, 
        uriCreator, 
        auditTracer, 
        authHeadersGetter, 
        auditHeadersGetter,
        settingRepository.Get("VneshtorgBankAdapterEndpoint"), 
        logger)
    {
    }
    
    public async Task<RequestMovementResponseDto> RequestMovementListAsync(
        RequestMovementRequestDto requestDto, 
        CancellationToken cancellationToken = default)
    {
        var response = await PostAsync<RequestMovementRequestDto, ApiDataResult<RequestMovementResponseDto>>(
            uri: "/BankOperation/RequestMovements",
            data: requestDto, 
            cancellationToken: cancellationToken);

        return response.data;
    }
}