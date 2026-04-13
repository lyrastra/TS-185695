#nullable enable

namespace Moedelo.AccountV2.Dto.User;

public class FirstLoginChangeWithEmailConfirmationRequestDto
{
    /// <summary>
    /// Идентификатор пользователя
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Новое значение логина
    /// </summary>
    public string NewLogin { get; set; } = null!;

    /// <summary>
    /// Хост, на который пришёл запрос
    /// </summary>
    public string Host { get; set; } = null!;

    /// <summary>
    /// IP-адрес, с которого пришёл запрос
    /// </summary>
    public string RequestIpAddress { get; set; } = null!;
}