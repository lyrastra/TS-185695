#nullable enable
using System;
using System.ComponentModel.DataAnnotations;
using Moedelo.Accounts.Abstractions.Validation;
using Moedelo.Accounts.ApiClient.Enums.UserActions;

namespace Moedelo.Accounts.Abstractions.Dto.UserActions;

/// <summary>
/// Заявка на создание записи о подтверждаемом действии пользователя
/// </summary>
public class NewUserConfirmableActionDto
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    [Range(1, int.MaxValue)]
    public int UserId { get; set; }

    /// <summary>
    /// Тип действия
    /// </summary>
    public UserConfirmableActionType ActionType { get; set; }

    /// <summary>
    /// Тип подтверждения
    /// </summary>
    public ConfirmationType ConfirmationType { get; set; }

    /// <summary>
    /// Дата и время создания заявки 
    /// </summary>
    public DateTime CreateDate { get; set; }

    /// <summary>
    /// Дата и время устаревания заявки 
    /// </summary>
    public DateTime ExpiryDate { get; set; }

    /// <summary>
    /// Хост, на котором была размещена заявка
    /// </summary>
    [Required(AllowEmptyStrings = false), MinLength(4), MaxLength(260)]
    public string Host { get; set; } = null!;

    /// <summary>
    /// IP-адрес, с которого была размещена заявка
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [ValidIpAddress]
    public string IpAddress { get; set; } = null!;

    /// <summary>
    /// Код подтверждения, который должен предъявить пользователь
    /// </summary>
    [Required(AllowEmptyStrings = false), MinLength(4)]
    public string ConfirmationCode { get; set; } = null!;

    /// <summary>
    /// Дополнительные данные: любой объект в json-формате
    /// </summary>
    public string? JsonPayload { get; set; }
}
