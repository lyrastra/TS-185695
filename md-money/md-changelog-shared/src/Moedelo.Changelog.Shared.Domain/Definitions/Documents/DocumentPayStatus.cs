namespace Moedelo.Changelog.Shared.Domain.Definitions.Documents
{
    public enum DocumentPayStatus
    {
        Default = 0,
        OnSigning = 1,
        Signed = 2,
        Client = 3,
        NotPayed = 4,
        PayPartly = 5,
        Payed = 6
    }
}
