using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.CommonApiV2.Dto.FirmFlag;

namespace Moedelo.CommonApiV2.Client.FirmFlags;

[InjectAsSingleton(typeof(IFirmFlagsApiClient))]
internal sealed class FirmFlagsApiClient : BaseApiClient, IFirmFlagsApiClient
{
    private readonly SettingValue apiEndPoint;
        
    public FirmFlagsApiClient(IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        ISettingRepository settingRepository,
        IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
        : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
    {
        apiEndPoint = settingRepository.Get("CommonApiPrivateApiEndpoint");
    }

    protected override string GetApiEndpoint()
    {
        return apiEndPoint.Value;
    }

    public Task<List<FirmFlagDto>> GetAsync(int firmId)
    {
        return GetAsync<List<FirmFlagDto>>($"/FirmFlags/?firmId={firmId}");
    }

    public Task<List<int>> GetEnabledByFirms(GetEnabledByFirmsDto dto)
    {
        return PostAsync<GetEnabledByFirmsDto, List<int>>($"/FirmFlags/GetEnabledByFirms", dto);
    }

    public Task<bool> IsEnableAsync(int firmId, int userId, string name, CancellationToken cancellationToken)
    {
        return GetAsync<bool>($"/FirmFlags/IsEnable?firmId={firmId}&userId={userId}&name={name}", cancellationToken: cancellationToken);
    }

    public Task<bool> HasAnyEnabledAsync(int firmId, string[] names, CancellationToken ct)
    {
        const string url = "/FirmFlags/HasAnyEnabled";
        var queryParams = new { firmId, flags = names };
            
        return GetAsync<bool>(url, queryParams, cancellationToken: ct);
    }

    public Task EnableAsync(int firmId, int userId, string name)
    {
        return PostAsync($"/FirmFlags/Enable?firmId={firmId}&userId={userId}&name={name}");
    }

    public Task DisableAsync(int firmId, int userId, string name)
    {
        return PostAsync($"/FirmFlags/Disable?firmId={firmId}&userId={userId}&name={name}");
    }

    public Task<int> GetCountFirmWithFlag(string name)
    {
        return GetAsync<int>("/FirmFlags/CountFirmWithFlag", new { name });
    }

    public Task RemoveAsync(int firmId, int userId, string name)
    {
        return DeleteAsync($"/FirmFlags/Remove?firmId={firmId}&userId={userId}&name={name}");
    }
}
