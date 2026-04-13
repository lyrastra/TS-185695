using System;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Accounts.Abstractions.Dto.UserActions;
using Moedelo.Accounts.Abstractions.Interfaces;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Accounts.ApiClient.NetFramework;

[InjectAsSingleton(typeof(IUserConfirmableActionsApiClient))]
internal sealed class UserConfirmableActionsApiClient : BaseCoreApiClient, IUserConfirmableActionsApiClient
{
    private readonly SettingValue endpoint;

    public UserConfirmableActionsApiClient(
        IHttpRequestExecutor httpRequestExecutor,
        IUriCreator uriCreator,
        IResponseParser responseParser,
        ISettingRepository settingRepository,
        IAuditTracer auditTracer,
        IAuditScopeManager auditScopeManager)
        : base(httpRequestExecutor,
            uriCreator,
            responseParser,
            settingRepository,
            auditTracer,
            auditScopeManager)
    {
        endpoint = settingRepository.Get("UserContextNetCoreApiEndpoint")
            .ThrowExceptionIfNull(true);
    }

    protected override string GetApiEndpoint()
    {
        return endpoint.Value;
    }

    public Task<CreatedUserConfirmableActionDto> CreateNewAsync(NewUserConfirmableActionDto dto,
        CancellationToken cancellationToken)
    {
        const string uri = "/v1/UserConfirmableActions";

        return PostAsync<NewUserConfirmableActionDto, CreatedUserConfirmableActionDto>(
            uri, dto, cancellationToken: cancellationToken);
    }

    public Task<UserConfirmableActionConfirmationResultDto> ConfirmAsync(UserConfirmableActionConfirmationDto dto,
        CancellationToken cancellationToken)
    {
        const string uri = "/v1/UserConfirmableActions/confirm";

        return PostAsync<UserConfirmableActionConfirmationDto, UserConfirmableActionConfirmationResultDto>(
            uri, dto, cancellationToken: cancellationToken);
    }
}
