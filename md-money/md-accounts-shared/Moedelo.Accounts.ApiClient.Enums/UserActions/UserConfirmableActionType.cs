using System.ComponentModel;

namespace Moedelo.Accounts.ApiClient.Enums.UserActions;

/// <summary>
/// Тип подтверждаемого действия пользователя
/// </summary>
public enum UserConfirmableActionType
{
    /// <summary>
    /// Тестовый метод
    /// </summary>
    [Description("Тестовый метод")]
    TestMethod = 0,
    /// <summary>
    /// Смена логина пользователя
    /// </summary>
    [Description("Смена логина пользователя")]
    ChangeUserLogin = 1,
    /// <summary>
    /// Смена пароля пользователя
    /// </summary>
    [Description("Смена пароля пользователя")]
    ChangeUserPassword = 2,
    /// <summary>
    /// Вход в ЛК
    /// </summary>
    [Description("Вход в ЛК")]
    Login = 3
}