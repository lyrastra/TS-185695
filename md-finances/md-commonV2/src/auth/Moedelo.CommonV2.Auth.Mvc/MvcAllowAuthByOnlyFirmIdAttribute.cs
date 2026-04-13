
using System.Web.Mvc;

namespace Moedelo.CommonV2.Auth.Mvc;

/// <summary>
/// Разрешает "авторизацию" запроса только по FirmId > 0, если UserId is {0 || 1}
/// Позволяет таким запросам проходить "сквозь" <see cref="MvcRejectUnauthorizedRequestAttribute"/>
/// и <see cref="MvcRedirectUnauthorizedRequestAttribute"/>
/// Запросы с FirmId > 0 && UserId > 0 обрабатываются штатным образом (проверяется доступность фирмы для пользователя)
/// </summary>
public sealed class MvcAllowAuthByOnlyFirmIdAttribute : FilterAttribute
{
}