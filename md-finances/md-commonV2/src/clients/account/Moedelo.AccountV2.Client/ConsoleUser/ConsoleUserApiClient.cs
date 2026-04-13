using System.Threading;
using System.Threading.Tasks;
using Moedelo.AccountV2.Dto.User;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.AccountV2.Client.ConsoleUser;

[InjectAsSingleton(typeof(IConsoleUserApiClient))]
public class ConsoleUserApiClient : BaseApiClient, IConsoleUserApiClient
{
    private readonly SettingValue apiEndPoint;

    public ConsoleUserApiClient(
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
        apiEndPoint = settingRepository.Get("AccountPrivateApiEndpoint");
    }

    protected override string GetApiEndpoint()
    {
        return apiEndPoint.Value;
    }

    public Task<UserDto> GetOrCreateByLoginAsync(string login, CancellationToken cancellationToken = default)
    {
        return PostAsync<UserDto>($"/Rest/ConsoleUser/GetOrCreate?login={login}", cancellationToken: cancellationToken);
    }
}