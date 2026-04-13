#nullable enable
using Moedelo.Accounts.ApiClient.Enums.UserActions;

namespace Moedelo.Accounts.Abstractions.Dto.UserActions;

/// <summary>
/// Результат обработки заявки на подтверждение действия пользователя
/// </summary>
public class UserConfirmableActionConfirmationResultDto
{
    /// <summary>
    /// Итоговый статус обработки заявки на подтверждение действия пользователя
    /// </summary>
    public UserConfirmableActionConfirmationStatus Status { get; set; }
    /// <summary>
    /// Данные о подтверждённом действии (если оно было подтверждено)
    /// </summary>
    public UserConfirmableActionDto? ConfirmedAction { get; set; }
}
