namespace Moedelo.BankIntegrations.Dto
{
    public class ApiDataResult<T>
    {
        public T data { get; set; }
        public ApiDataResult(T data)
        {
            this.data = data;
        }
    }
}
