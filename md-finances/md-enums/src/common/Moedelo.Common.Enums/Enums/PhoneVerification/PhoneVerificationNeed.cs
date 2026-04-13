namespace Moedelo.Common.Enums.Enums.PhoneVerification
{
    public enum PhoneVerificationNeed
    {
        // Не нужно подтверждать
        NoNeed = 0,

        // Нужно подтвердить в стандартном режиме
        Need = 1,

        // Нужно подтвердить, при этом сразу ввести новый номер, 
        // т.к. текущий стационарный или не прошёл форматный контроль
        NeedWithEnteringNewPhone = 2
    }
}