using System.Web.Mvc;
using Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;

namespace Moedelo.CommonV2.Auth.Mvc;

/// <summary>
/// Базовый класс для авторизационный фильтра для MVC приложения, блокирующего обработку неавторизованного запроса.
/// В случае, если запрос неавторизован, формируется Http-ответ с кодом HttpStatusCode.Unauthorized
/// Запрос считается неавторизованным, если метод IsAuthorized объекта типа IUserContext возвращает false.
/// 
/// Классы наследники должны реализовывать получение текущего контекста пользователя типа
/// <see cref="IAuthorizationContext"/>
/// 
/// ВНИМАНИЕ: фильтр не занимается заполнением (=аутентификацией) объекта типа IUserContext -
/// это должен делать другой фильтр (либо иной механизм), исполняющийся до рассматриваемого.
/// Например, см. <see cref="Moedelo.CommonV2.Auth.Mvc.MvcAuthentificationFilter"/>.
/// </summary>
public abstract class MvcRejectUnauthorizedRequestAbstractAttribute<TFilterAttribute> : MvcUnauthorizedRequestFilterAttribute<TFilterAttribute>
where TFilterAttribute : MvcRejectUnauthorizedRequestAbstractAttribute<TFilterAttribute>
{
    protected sealed override void OnRequestIsUnauthorized(AuthorizationContext filterContext)
    {
        filterContext.Result = new HttpUnauthorizedResult();
    }
}