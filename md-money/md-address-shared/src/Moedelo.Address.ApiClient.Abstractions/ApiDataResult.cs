namespace Moedelo.Address.ApiClient.Abstractions
{
    public class ApiDataResult<T>
    {
        public T data { get; set; }

        public int StatusCode { get; set; }
    }
}
