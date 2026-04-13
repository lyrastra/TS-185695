namespace Moedelo.BankIntegrations.Utils.Abstractions.Services
{
    public interface ICryptoService
    {
        string EncryptText(string text);
        string DecryptText(string text);
    }
}