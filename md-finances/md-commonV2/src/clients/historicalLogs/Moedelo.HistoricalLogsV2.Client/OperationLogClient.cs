using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Hosting;
using Moedelo.HistoricalLogsV2.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.HistoricalLogsV2.Client;

[InjectAsSingleton(typeof(IOperationLogClient))]
internal sealed class OperationLogClient : BaseApiClient, IOperationLogClient
{
    private readonly IOperationLogWriteApiClient writeApiClient;
    private readonly SettingValue endpointSetting;

    public OperationLogClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        ISettingRepository settingRepository,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager,
        IOperationLogWriteApiClient writeApiClient)
        : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
    {
        this.writeApiClient = writeApiClient;
        endpointSetting = settingRepository.Get("HistoricalLogsApiEndpoint")
            .ThrowExceptionIfNull(true);
    }

    protected override string GetApiEndpoint()
    {
        return endpointSetting.Value;
    }

    public void Log(LogOperationDto data)
    {
        HostingEnvironment.QueueBackgroundWorkItem(cancellation =>
            writeApiClient.LogAsync(data, querySetting: default, cancellationToken: cancellation));
    }

    public Task LogAsync(LogOperationDto data, HttpQuerySetting querySetting, CancellationToken cancellationToken)
    {
        return writeApiClient.LogAsync(data, querySetting, cancellationToken);
    }

    public Task LogAsync(IReadOnlyCollection<LogOperationDto> records, HttpQuerySetting querySetting = default,
        CancellationToken cancellationToken = default)
    {
        return writeApiClient.LogAsync(records, querySetting, cancellationToken);
    }

    public void Log(IReadOnlyCollection<LogOperationDto> records)
    {
        HostingEnvironment.QueueBackgroundWorkItem(cancellation =>
            writeApiClient.LogAsync(records, cancellationToken: cancellation));
    }

    public Task<List<OperationDto>> GetAsync(ReadOperationLogDto dto)
    {
        return GetAsync<List<OperationDto>>("/Operations", dto);
    }
}