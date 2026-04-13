using System.Web.Mvc;
using Moedelo.CommonV2.UserContext.Domain;
using Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;

namespace Moedelo.CommonV2.Auth.Mvc;

/// <summary>
/// Авторизационный фильтр для Mvc приложения, блокирующий обработку неавторизованного запроса
/// на базе <see cref="MvcRejectUnauthorizedRequestAbstractAttribute{TFilterAttribute}"/>
/// 
/// ВНИМАНИЕ: для получения IUserContext используется "родной" Mvc механизм разрешения зависимостей -
/// <see cref="System.Web.Mvc.DependencyResolver"/>
/// который должен быть инициализирован соответвующим образом к моменту использования.
///
/// Этот фильтр может быть зарегистрирован на уровне приложения, контроллера или метода как любой другой аттрибут.
/// </summary>
public class MvcRejectUnauthorizedRequestAttribute : MvcRejectUnauthorizedRequestAbstractAttribute<MvcRejectUnauthorizedRequestAttribute>
{
    protected override IAuthorizationContext UserAuthorizationContext =>
        DependencyResolver.Current.GetService<IAuthorizationContext>();
}