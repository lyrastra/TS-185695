using System;
using System.Web.Mvc;
using Moedelo.CommonV2.Auth.Mvc.Extensions;
using Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;

namespace Moedelo.CommonV2.Auth.Mvc;

/// Классы наследники должны реализовывать получение текущего контекста пользователя типа
/// <see cref="IAuthorizationContext"/>
public abstract class MvcUnauthorizedRequestFilterAttribute<TFilterAttribute> : FilterAttribute, IAuthorizationFilter
    where TFilterAttribute : MvcUnauthorizedRequestFilterAttribute<TFilterAttribute>
{
    protected abstract IAuthorizationContext UserAuthorizationContext { get; }

    private static bool AllowAnonymous<TAttribute>(AuthorizationContext filterContext)
        where TAttribute : Attribute
    {
        if (filterContext.HasAttributeOnAction<AllowAnonymousAttribute>())
        {
            // анонимный доступ разрешён на уровне метода контроллера
            return true;
        }

        return filterContext.HasAttributeOnController<AllowAnonymousAttribute>()
               && !filterContext.HasAttributeOnAction<TAttribute>();
    }
    
    private static bool AllowAuthByOnlyFirmId<TAttribute>(AuthorizationContext filterContext)
        where TAttribute : Attribute
    {
        if (filterContext.HasAttributeOnAction<MvcAllowAuthByOnlyFirmIdAttribute>())
        {
            // анонимный доступ разрешён на уровне метода контроллера
            return true;
        }

        return filterContext.HasAttributeOnController<MvcAllowAuthByOnlyFirmIdAttribute>()
               && !filterContext.HasAttributeOnAction<TAttribute>();
    }


    public void OnAuthorization(AuthorizationContext filterContext)
    {
        var isAuthenticated = UserAuthorizationContext?.IsAuthenticated() == true;

        if (isAuthenticated == false)
        {
            // пользователь не аутентифицирован
            if (AllowAnonymous<TFilterAttribute>(filterContext))
            {
                // и разрешён анонимный доступ
                return;
            }

            // иначе требуется авторизация
            OnRequestIsUnauthorized(filterContext);

            return;
        }

        if (UserAuthorizationContext.HasOnlyFirmIdDefined() && AllowAuthByOnlyFirmId<TFilterAttribute>(filterContext))
        {
            // для этого метода разрешена комбинация FirmId > 0 && UserId is {0 || -1}
            return;
        }

        if (UserAuthorizationContext.HasOnlyUserIdDefined())
        {
            // это профаутсорсер вне контекста какой-либо фирмы
            // допустимость такой пары проверяется в процессе аутентификации (если это требуется, например в oauth)
            return;
        }

        const int invalidRoleId = 0;

        if (UserAuthorizationContext.GetRoleIdAsync().Result == invalidRoleId)
        {
            // у пользователя нет доступа к этой фирме
            OnRequestIsUnauthorized(filterContext);
        }
    }

    protected abstract void OnRequestIsUnauthorized(AuthorizationContext filterContext);
}