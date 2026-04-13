#nullable enable
namespace Moedelo.AccountV2.Dto.User;

public class LoginChangeWithEmailConfirmationResponseDto
{
    public enum StatusCode
    {
        /// <summary>
        /// Код подтверждения отправлен на почту
        /// </summary>
        ConfirmationSent = 0,
        /// <summary>
        /// Неверный пароль
        /// </summary>
        PasswordIsInvalid = 1,
        /// <summary>
        /// Такой логин не может быть выбран
        /// </summary>
        LoginIsAlreadyBusy = 2,
        /// <summary>
        /// Неизвестная ошибка
        /// </summary>
        UnknownError = 3
    }

    public StatusCode Status { get; set; }
}
