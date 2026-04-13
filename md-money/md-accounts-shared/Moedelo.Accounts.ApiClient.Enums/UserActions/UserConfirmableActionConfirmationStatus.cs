namespace Moedelo.Accounts.ApiClient.Enums.UserActions;

/// <summary>
/// Статус подтверждения действия пользователя
/// </summary>
public enum UserConfirmableActionConfirmationStatus
{
    /// <summary>
    /// Действие успешно подтверждено
    /// </summary>
    SuccessfullyConfirmed = 1,
    /// <summary>
    /// Ошибка: действие не найдено 
    /// </summary>
    ErrorActionNotFound = 2,
    /// <summary>
    /// Ошибка: неверный код подтверждения 
    /// </summary>
    ErrorCodeIsInvalid = 3,
    /// <summary>
    /// Ошибка: превышено максимальное время ожидания подтверждения 
    /// </summary>
    ErrorCodeIsExpired = 4,
    /// <summary>
    /// Ошибка: действие уже подтверждено 
    /// </summary>
    ErrorActionIsAlreadyConfirmed = 5,
    /// <summary>
    /// Ошибка: неверный тип подтверждения 
    /// </summary>
    ErrorConfirmationTypeIsInvalid = 6,
    /// <summary>
    /// Неверная дата подтверждения (раньше даты создания кода)
    /// </summary>
    ErrorConfirmationDateIsInvalid = 7
}
