using System.Threading;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using Moedelo.CommonV2.Auth.Domain;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;

namespace Moedelo.CommonV2.Auth.Mvc;

/// <summary>
/// Аутентификационный аттрибут для Mvc приложений.
/// Отвечает за извлечение аутентификационных данных посредством аутентификационного сервиса
/// и инициализирование ими текущего контекста.
/// ВНИМАНИЕ: не останавливает обработку неавторизованных запросов - приложение должно это реализовывать самостоятельно, опираясь на состояние контекста,
/// например с помощью авторизационного фильтра на контроллерах, требующих авторизации
/// (пример фильтра см. Moedelo.CommonV2.Auth.Mvc.MvcRejectUnauthorizedRequestAttribute)
/// Пример встраивания: 
/// ... где-то в Glbal.asax.cs   
///     <![CDATA[
/// protected void Application_Start()
/// {
///  ...
///     GlobalFilters.Filters.Add(new MvcAuthentificationFilter(
///         DependencyResolver.Current.GetService<IAuthenticationService>(),
///         DependencyResolver.Current.GetService<IUserFirmContextInitializer>()
///     ));
///  ...
/// }]]>
/// </summary>
[InjectAsSingleton]
public class MvcAuthentificationFilter : FilterAttribute, IAuthenticationFilter, IDI
{
    private readonly IAuthenticationService authenticationService;
    private readonly IUserFirmContextInitializer contextInitializer;

    public MvcAuthentificationFilter(
        IAuthenticationService authenticationService,
        IUserFirmContextInitializer contextInitializer)
    {
        this.authenticationService = authenticationService;
        this.contextInitializer = contextInitializer;
    }

    public void OnAuthentication(AuthenticationContext filterContext)
    {
        using var cancellation = CancellationTokenSource.CreateLinkedTokenSource(
            filterContext.HttpContext.Response.ClientDisconnectedToken,
            filterContext.HttpContext.Request.TimedOutToken);

        var authInfo = authenticationService.AuthenticateAsync(cancellation.Token).Result;
        contextInitializer.InitializeContext(authInfo);
    }

    public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
    {
    }
}
