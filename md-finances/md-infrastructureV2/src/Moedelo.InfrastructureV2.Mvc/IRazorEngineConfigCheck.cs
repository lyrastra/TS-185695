using Moedelo.InfrastructureV2.Domain.Interfaces.Web;

namespace Moedelo.InfrastructureV2.Mvc;

/// <summary>
/// Сервис проверки конфигурации Razor view engine.
/// Используется для обнаружения проблем с конфигурацией ASP.NET MVC Razor (binding redirects, версии сборок, Views\Web.config) 
/// до того, как пользователь попытается открыть view.
/// Проверяет, что Razor view engine правильно сконфигурирован и может компилировать .cshtml файлы.
/// Метод выполняет реальную компиляцию первого найденного .cshtml файла через BuildManager,
/// что вызывает полную инициализацию Razor и чтение Views\Web.config.
/// Проверка выполняется только один раз - при первом вызове. Последующие вызовы игнорируются.
/// <exception cref="System.InvalidOperationException">
/// Если Razor не может быть инициализирован из-за проблем с конфигурацией:
/// - отсутствуют или неправильные binding redirects для System.Web.WebPages.Razor, System.Web.Mvc
/// - несоответствие версий сборок в Views\Web.config
/// - конфликт версий сборок
/// - ошибки при компиляции view
/// </summary>
public interface IRazorEngineConfigCheck : IWebAppConfigCheck;
