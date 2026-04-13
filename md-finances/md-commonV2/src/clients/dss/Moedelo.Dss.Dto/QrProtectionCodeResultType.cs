
namespace Moedelo.Dss.Dto
{
    public enum QrProtectionCodeResultType
    {
        /// <summary>
        /// Запрос выполнен успешно
        /// </summary>
        Success = 0,
        /// <summary>
        /// Ошибка отправки СМС
        /// </summary>
        SmsSendError = 1,
        /// <summary>
        /// Указан неверный код подтверждения
        /// </summary>
        InvalidConfirmCode = 2,
        /// <summary>
        /// Неверный телефонный номер для отправки
        /// </summary>
        InvalidPhoneNumber = 3,
    }
}