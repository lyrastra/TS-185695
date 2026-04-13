using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.CommonV2.Auth.Domain;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;

namespace Moedelo.CommonV2.Auth.WebApi;

/// <summary>
/// Аутентификационный хэндлер для WebAPI приложений.<br />
/// Отвечает за извлечение аутентификационных данных посредством аутентификационного сервиса
/// и инициализирование ими текущего контекста.<br />
/// ВНИМАНИЕ: не останавливает обработку неавторизованных запросов - приложение должно это реализовывать самостоятельно, опираясь на состояние контекста,
/// например с помощью авторизационного фильтра на контроллерах, требующих авторизации
/// (пример фильтра см. Moedelo.CommonV2.Auth.WebApi.WebApiRejectUnauthorizedRequestAttribute).
/// <br /><br />
/// Пример встраивания:<br />
/// где-то в Global.asax.cs:<br />   
///     <![CDATA[GlobalConfiguration.Configuration.MessageHandlers.Add(DiInstaller.GetInstance<WebApiAuthHandler>());]]>
/// </summary>
[InjectAsSingleton(typeof(WebApiAuthHandler))]
public sealed class WebApiAuthHandler : DelegatingHandler
{
    private readonly IAuthenticationService authenticationService;
    private readonly IUserFirmContextInitializer contextInitializer;

    public WebApiAuthHandler(
        IAuthenticationService authenticationService,
        IUserFirmContextInitializer contextInitializer)
    {
        this.authenticationService = authenticationService;
        this.contextInitializer = contextInitializer;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authInfo = await authenticationService
            .AuthenticateAsync(cancellationToken)
            .ConfigureAwait(true);
        contextInitializer.InitializeContext(authInfo);

        return await base.SendAsync(request, cancellationToken).ConfigureAwait(true);
    }
}