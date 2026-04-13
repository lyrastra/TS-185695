namespace Moedelo.BillingV2.Client
{
    public class DataRequestWrapper<T>
    {
        public T Data { get; set; }

        public bool ResponseStatus { get; set; }

        public string ResponseMessage { get; set; }
    }
}