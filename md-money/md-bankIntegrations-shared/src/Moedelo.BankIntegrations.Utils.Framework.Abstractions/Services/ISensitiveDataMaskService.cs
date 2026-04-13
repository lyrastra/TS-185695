namespace Moedelo.BankIntegrations.Utils.Framework.Abstractions.Services
{
    public interface ISensitiveDataMaskService
    {
        string EncryptToken(string input);
    }
}
