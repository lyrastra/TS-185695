using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Moedelo.InfrastructureV2.Mvc.Internals;

namespace Moedelo.InfrastructureV2.Mvc;

/// <summary>
/// Возвращает ответ с HttpStatus = 400, если полученный POST или PUT запрос содержит пустое тело
/// ВНИМАНИЕ: даже если тело пустое или содержит некорректные данные, mvc в любом случае создает
/// объект типа, требуемого методом контроллера, и передаёт его в данный метод: объект не будет равен null,
/// его свойства будут заполнены значениями по умолчанию. Аналогичное поведение наблюдается и тогда,
/// когда запрос содержит неверно отформатированные данные или данные другого вида/типа/формата.
/// Данный атрибут проверяет только случай, когда тело пустое (не содержит данных)
/// </summary>
public class MvcRejectPostAndPutRequestWithEmptyRequestBodyAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext actionContext)
    {
        var httpMethod = actionContext.HttpContext.Request.HttpMethod;

        if (httpMethod != HttpMethod.Post.Method && httpMethod != HttpMethod.Put.Method)
        {
            return;
        }

        var inputStream = actionContext.RequestContext.HttpContext.Request.InputStream;
        var isEmpty = inputStream.CanRead && inputStream.Length == 0;

        if (isEmpty)
        {
            actionContext.Result = new CustomHttpStatusCodeTextActionResult(
                HttpStatusCode.BadRequest,
                "Request validation failed",
                "Empty request body is not allowed");
        }
    }
}
