namespace Moedelo.Common.Enums.Enums.RegistrationService
{
    public enum RegistrationErrorCode
    {
        None = 0,

        EmptyRequest = 1,

        Validation = 2,

        UserAlreadyRegistered = 3,

        NoAccessInBilling = 4,

        InnerError = 999,
    }
}