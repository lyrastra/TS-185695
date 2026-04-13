using System;

namespace Moedelo.Accounts.Abstractions.Dto.UserActions;

/// <summary>
/// Созданное действие пользователя, требующее подтверждения
/// </summary>
public class CreatedUserConfirmableActionDto
{
    /// <summary>
    /// Уникальный идентификатор действия
    /// </summary>
    public Guid ActionId { get; set; }
    /// <summary>
    /// Код подтверждения действия
    /// </summary>
    public string ConfirmationCode { get; set; }
}
