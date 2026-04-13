namespace Moedelo.BankIntegrations.Utils.Framework.Abstractions.Services
{
    public interface ICryptoService
    {
        string EncryptText(string text);
        string DecryptText(string text);
    }
}