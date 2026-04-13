using System.Web.Http;
using Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;

namespace Moedelo.CommonV2.Auth.WebApi;

/// <summary>
/// Авторизационный фильтр для WebApi приложения, блокирующий обработку неавторизованного запроса
/// на базе WebApiRejectUnauthorizedRequestAbstractAttribute.
/// 
/// ВНИМАНИЕ: для получения IUserContext используется "родной" WebApi механизм разрешения зависимостей - GlobalConfiguration.Configuration.DependencyResolver -
/// который должен быть инициализирован соответвующим образом к моменту использования.
///
/// Этот фильтр может быть зарегистрирован на уровне приложения, контроллера или метода как любой другой аттрибут.
/// </summary>
public class WebApiRejectUnauthorizedRequestAttribute : WebApiRejectUnauthorizedRequestAbstractAttribute
{
    protected override IAuthorizationContext UserAuthorizationContext =>
        GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IAuthorizationContext)) as IAuthorizationContext;

}