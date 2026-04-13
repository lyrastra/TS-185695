namespace Moedelo.BankIntegrations.Enums.IntegrationSetup;

/// <summary>
/// Инициатор отключения интеграции
/// </summary>
public enum IntegrationDisableInitiator
{
    /// <summary>
    /// Автоматическое отключение
    /// </summary>
    Auto = 0,
    /// <summary>
    /// Пользователь
    /// </summary>
    User = 1,
    /// <summary>
    /// Сотрудник аута
    /// </summary>
    Outsource = 2
}