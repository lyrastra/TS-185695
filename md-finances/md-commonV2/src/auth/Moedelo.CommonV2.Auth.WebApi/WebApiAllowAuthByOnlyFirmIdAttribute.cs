using System.Web.Http.Filters;

namespace Moedelo.CommonV2.Auth.WebApi;

/// <summary>
/// Разрешает "авторизацию" запроса только по FirmId > 0, если UserId is {0 or 1}
/// Позволяет таким запросам проходить "сквозь" <see cref="WebApiRejectUnauthorizedRequestAttribute"/>
/// Запросы с FirmId > 0 && UserId > 0 обрабатываются штатным образом (проверяется доступность фирмы для пользователя)
/// </summary>
public sealed class WebApiAllowAuthByOnlyFirmIdAttribute : FilterAttribute
{
}