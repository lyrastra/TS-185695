#nullable enable
namespace Moedelo.AccountV2.Dto.User;

public class ConfirmUserLoginChangingResponseDto
{
    public enum StatusCode
    {
        Success = 0,
        ConfirmationCodeIsInvalid = 1,
        ConfirmationCodeIsExpired = 2,
        UnknownError = 3
    }

    public StatusCode Status { get; set; }
    
    /// <summary>
    /// Идентификатор пользователя (может быть не заполнен в случае ошибки)
    /// </summary>
    public int? UserId { get; set; }
    /// <summary>
    /// Старый логин пользователя
    /// </summary>
    public string? OldLogin { get; set; }
    /// <summary>
    /// Новый логин пользователя
    /// </summary>
    public string? NewLogin { get; set; }
}
