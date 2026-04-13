namespace Moedelo.BankIntegrations.Utils.Abstractions.Services
{
    public interface ISensitiveDataMaskService
    {
        string EncryptToken(string input);
    }
}
