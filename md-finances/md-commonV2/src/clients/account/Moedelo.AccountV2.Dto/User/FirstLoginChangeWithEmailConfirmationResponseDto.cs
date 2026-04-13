using System;

namespace Moedelo.AccountV2.Dto.User;

public class FirstLoginChangeWithEmailConfirmationResponseDto
{
    public enum StatusCode
    {
        /// <summary>
        /// Код подтверждения отправлен на почту
        /// </summary>
        ConfirmationSent = 0,
        /// <summary>
        /// Операция не разрешена
        /// </summary>
        OperationNotAllowed = 1,
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

    /// <summary>
    /// Уникальный идентификатор действия
    /// </summary>
    public Guid? ActionId { get; set; }
}