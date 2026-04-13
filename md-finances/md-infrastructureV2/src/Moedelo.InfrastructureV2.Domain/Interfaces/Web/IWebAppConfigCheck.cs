namespace Moedelo.InfrastructureV2.Domain.Interfaces.Web;

/// <summary>
/// Базовый интерфейс для проверок конфигурации веб-приложения.
/// Реализации этого интерфейса автоматически регистрируются в DI и вызываются 
/// через <see cref="IWebAppConfigChecker"/> для проверки корректности конфигурации приложения.
/// </summary>
/// <remarks>
/// Примеры проверок:
/// - Проверка конфигурации Razor view engine
/// - Проверка создания всех контроллеров через DI
/// - Проверка доступности внешних зависимостей
/// </remarks>
public interface IWebAppConfigCheck
{
    /// <summary>
    /// Выполняет проверку конфигурации приложения.
    /// </summary>
    /// <exception cref="System.Exception">
    /// Если конфигурация некорректна или проверка не прошла.
    /// Тип исключения зависит от конкретной реализации.
    /// </exception>
    void Check();
};

/// <summary>
/// Композитный сервис для запуска всех зарегистрированных проверок конфигурации веб-приложения.
/// Собирает все реализации <see cref="IWebAppConfigCheck"/> и запускает их последовательно.
/// Используется в health-check endpoints (например, в PingController) для проверки готовности приложения к работе.
/// </summary>
public interface IWebAppConfigChecker
{
    /// <summary>
    /// Запускает все зарегистрированные проверки конфигурации приложения (<see cref="IWebAppConfigCheck"/>).
    /// </summary>
    /// <exception cref="System.Exception">
    /// Если хотя бы одна проверка не прошла. Тип и сообщение исключения зависят от конкретной проверки.
    /// </exception>
    void CheckWebAppConfiguration();
}
