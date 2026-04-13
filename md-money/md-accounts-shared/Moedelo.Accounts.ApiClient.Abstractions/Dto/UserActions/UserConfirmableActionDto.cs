#nullable enable
using System;

namespace Moedelo.Accounts.Abstractions.Dto.UserActions;

/// <summary>
/// Заявка на создание записи о подтверждаемом действии пользователя
/// </summary>
public class UserConfirmableActionDto
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Дата и время создания заявки 
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// Дополнительные данные: любой объект в json-формате
    /// </summary>
    public string? JsonPayload { get; set; }
}
