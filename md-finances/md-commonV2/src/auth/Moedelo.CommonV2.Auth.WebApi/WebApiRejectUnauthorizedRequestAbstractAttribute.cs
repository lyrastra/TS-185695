using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Moedelo.CommonV2.UserContext.Domain.AuthorizationContext;

namespace Moedelo.CommonV2.Auth.WebApi;

/// <summary>
/// Базовый класс для авторизационный фильтра для WebApi приложения, блокирующего обработку неавторизованного запроса.
/// В случае, если запрос неавторизован, формируется Http-ответ с кодом HttpStatusCode.Unauthorized
/// Запрос считается неавторизованным, если метод IsAuthorized объекта типа IUserContext возвращает false.
///
/// Классы наследники должны реализовывать получение текущего контекста пользователя типа IUserContext
/// 
/// ВНИМАНИЕ: фильтр не занимается заполнением (=аутентификацией) объекта типа IUserContext -
/// это должен делать другой фильтр или хэндлер, исполняющийся до рассматриваемого. Например, см. Moedelo.CommonV2.Auth.WebApi.WebApiAuthHandler.
/// </summary>
public abstract class WebApiRejectUnauthorizedRequestAbstractAttribute : AuthorizationFilterAttribute
{
    private static object UnauthorizedBodyResponse = new { Message = "Unauthorized" };
        
    protected abstract IAuthorizationContext UserAuthorizationContext { get; }
        
    private static bool AllowAnonymous(HttpActionContext actionContext)
    {
        if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any())
        {
            // анонимный доступ разрешён на уровне метода контроллера
            return true;
        }

        return actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
               && !actionContext.ActionDescriptor.GetCustomAttributes<WebApiRejectUnauthorizedRequestAbstractAttribute>(true).Any();
    }
        
    private static bool AllowAuthByOnlyFirmId(HttpActionContext actionContext)
    {
        if (actionContext.ActionDescriptor.GetCustomAttributes<WebApiAllowAuthByOnlyFirmIdAttribute>().Any())
        {
            // такой доступ разрешён на уровне метода контроллера
            return true;
        }

        return actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<WebApiAllowAuthByOnlyFirmIdAttribute>().Any()
               && !actionContext.ActionDescriptor.GetCustomAttributes<WebApiRejectUnauthorizedRequestAbstractAttribute>(true).Any();
    }

    public override async Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
    {
        var isAuthenticated = UserAuthorizationContext?.IsAuthenticated() == true;

        if (isAuthenticated == false)
        {
            // пользователь не аутентифицирован
            if (AllowAnonymous(actionContext))
            {
                // и разрешён анонимный доступ
                return;
            }

            // иначе требуется аутентификация
            actionContext.Response = actionContext.Request.CreateResponse(
                HttpStatusCode.Unauthorized,
                UnauthorizedBodyResponse);

            return;
        }
            
        if (UserAuthorizationContext.HasOnlyFirmIdDefined() && AllowAuthByOnlyFirmId(actionContext))
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

        if (await UserAuthorizationContext.GetRoleIdAsync() == invalidRoleId)
        {
            // у пользователя нет доступа к этой фирме
            actionContext.Response = actionContext.Request.CreateResponse(
                HttpStatusCode.Unauthorized,
                UnauthorizedBodyResponse);
        }
    }
}