using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.HistoricalLogsV2.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HistoricalLogsV2.Client;

[InjectAsSingleton(typeof(IOperationLogWriteApiClient))]
internal sealed class OperationLogWriteApiClient : BaseApiClient, IOperationLogWriteApiClient
{
    private readonly SettingValue endpointSetting;
    
    public OperationLogWriteApiClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        ISettingRepository settingRepository,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager)
        : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
    {
        endpointSetting = settingRepository.Get("HistoricalLogsWriteApiEndpoint")
            .ThrowExceptionIfNull(true);
    }

    protected override string GetApiEndpoint()
    {
        return endpointSetting.Value;
    }

    public Task LogAsync(LogOperationDto data, HttpQuerySetting querySetting, CancellationToken cancellationToken)
    {
        return PostAsync("/Operations", data, setting: querySetting, cancellationToken: cancellationToken);
    }

    public Task LogAsync(IReadOnlyCollection<LogOperationDto> records, HttpQuerySetting querySetting,
        CancellationToken cancellationToken)
    {
        return PostAsync("/Operations/List", records, setting: querySetting, cancellationToken: cancellationToken);
    }
}
