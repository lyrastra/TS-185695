#nullable enable
namespace Moedelo.AccountV2.Dto.User;

public sealed class LoginChangeRequestDto
{
    /// <summary>
    /// Идентификатор пользователя, которому меняется логин
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Новое значение логина
    /// </summary>
    public string NewLogin { get; set; } = null!;
}
