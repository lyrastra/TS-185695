namespace Moedelo.Spam.ApiClient.Abastractions.Enums.Sms;

/// <summary>
/// Тип смс, в каких целях используется отправка СМС (не обязательное поле)
/// </summary>
public enum SmsType
{
    /// <summary>
    /// SMS-код для подтверждения
    /// </summary>
    SmsCodeForVerification = 0,
}