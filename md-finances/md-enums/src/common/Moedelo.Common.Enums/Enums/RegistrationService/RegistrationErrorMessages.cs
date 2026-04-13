using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.RegistrationService
{
    public enum RegistrationErrorMessages
    {
        [Description("Неверно указана почта")]
        EmptyEmail,
        [Description("Неверно указаны ФИО")]
        EmptyFio,
        [Description("Не указан пароль")]
        EmptyPassword,
        [Description("Не указан телефон")]
        EmptyPhone,
        [Description("Превышена допустимая длина текста комментария")]
        ExceededMaximumLengthError,
        [Description("Произошла внутренняя ошибка")]
        RegistrationServiceError,
        [Description("Пользователь с данным почтовым адресом уже зарегистрирован")]
        UserAllreadyRegistered,
        [Description("Не совпадает ИНН фирм для связывания аккаунтов")]
        InvalidInn,
        [Description("Не совпадает форма собственности для связывания аккаунтов")]
        InvalidIsOoo,
    }

    public static class RegistrationErrorMessagesExtension{
        public static string GetText(this RegistrationErrorMessages e)
        {
            var attr = (DescriptionAttribute)e.GetType()
                .GetField(e.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)[0];
            return attr.Description;
        }
    }
}