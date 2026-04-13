namespace Moedelo.Common.Enums.Enums.Home
{
    public enum RegistrationResponseStatusCode
    {
        Ok = 0,

        Error = 1,

        LoginIsBusy = 2,

        NotFound = 3,

        EmailWasNotSent = 4,

        UnableGrantAccess = 5,

        SentForReprocessing = 6,

        NoAccessInBilling = 7
    }
}