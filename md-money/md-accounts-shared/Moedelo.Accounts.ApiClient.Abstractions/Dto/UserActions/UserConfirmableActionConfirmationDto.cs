#nullable enable
using System;
using System.ComponentModel.DataAnnotations;
using Moedelo.Accounts.Abstractions.Validation;
using Moedelo.Accounts.ApiClient.Enums.UserActions;

namespace Moedelo.Accounts.Abstractions.Dto.UserActions;

/// <summary>
/// Заявка на подтверждение ранее созданного подтверждаемого действия пользователя
/// </summary>
public class UserConfirmableActionConfirmationDto
{
    /// <summary>
    /// Уникальный идентификатор действия
    /// </summary>
    public Guid ActionId { get; set; }

    /// <summary>
    /// Предъявленный код подтверждения
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string ConfirmationCode { get; set; } = null!;
    
    /// <summary>
    /// Тип действия
    /// </summary>
    public UserConfirmableActionType ActionType { get; set; }

    /// <summary>
    /// Тип подтверждения
    /// </summary>
    public ConfirmationType ConfirmationType { get; set; }

    /// <summary>
    /// Хост, с которого пришло подтверждение
    /// </summary>
    [Required(AllowEmptyStrings = false), MinLength(4), MaxLength(260)]
    public string Host { get; set; } = null!;

    /// <summary>
    /// IP-адрес, с которого пришло подтверждение 
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    [ValidIpAddress]
    public string IpAddress { get; set; } = null!;

    /// <summary>
    /// Дата и время, когда пришло подтверждение 
    /// </summary>
    public DateTime ConfirmationDate { get; set; }
}
