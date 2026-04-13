using System;
using System.Web.Mvc;
using Moedelo.CommonV2.Utils.ServerUrl;

namespace Moedelo.CommonV2.Auth.Mvc;

/// <summary>
/// Базовый класс для авторизационный фильтра для MVC приложения, блокирующего обработку неавторизованного запроса.
/// В случае, если запрос неавторизован, формируется редирект на форму авторизации
/// При этом, после авторизации, происходит редирект именно на ту страницу, куда пользователь пытался попасть
/// Запрос считается неавторизованным, если метод IsAuthorized объекта типа IUserContext возвращает false.
/// 
/// Классы наследники должны реализовывать получение текущего контекста пользователя типа IUserContext
/// 
/// ВНИМАНИЕ: фильтр не занимается заполнением (=аутентификацией) объекта типа IUserContext -
/// это должен делать другой фильтр (либо иной механизм), исполняющийся до рассматриваемого. Например, см. Moedelo.CommonV2.Auth.Mvc.MvcAuthentificationFilter.
/// </summary>
public abstract class MvcRedirectUnauthorizedRequestAbstractAttribute<TFilterAttribute>
    : MvcUnauthorizedRequestFilterAttribute<TFilterAttribute>
    where TFilterAttribute : MvcRedirectUnauthorizedRequestAbstractAttribute<TFilterAttribute>
{
    private readonly IServerUriService serverUriService = DependencyResolver.Current.GetService<IServerUriService>();

    protected sealed override void OnRequestIsUnauthorized(AuthorizationContext filterContext)
    {
        var requestUrl = GetWithModifiedScheme(filterContext.HttpContext.Request.Url);
        var authUrl = serverUriService.GetAuthWithBackUrl(requestUrl);

        filterContext.Result = new RedirectResult(authUrl);
    }

    private static Uri GetWithModifiedScheme(Uri requestUrl)
    {
        if (requestUrl.Host == "localhost")
        {
            return requestUrl;
        }

        var uriBuilder = new UriBuilder(requestUrl)
        {
            Scheme = Uri.UriSchemeHttps,
            Port = -1
        };

        return uriBuilder.Uri;
    }
}