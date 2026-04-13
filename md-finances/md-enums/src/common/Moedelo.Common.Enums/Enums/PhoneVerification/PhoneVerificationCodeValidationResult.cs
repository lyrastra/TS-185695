namespace Moedelo.Common.Enums.Enums.PhoneVerification
{
    public enum PhoneVerificationCodeValidationResult
    {
        /// <summary>
        /// Некорректная длина проверечного кода
        /// </summary>
        IncorrectLength = 0,

        /// <summary>
        /// Действительный код
        /// </summary>
        Valid = 1,

        /// <summary>
        /// Недействительный код
        /// </summary>
        Invalid = 2,
    }
}