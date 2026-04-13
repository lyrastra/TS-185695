namespace Moedelo.BankIntegrationsV2.Client
{
    // Для совместимости со старыми endpoint-ами
    internal class DataResponseWrapper<T>
    {
        public T Data { get; set; }
    }
}