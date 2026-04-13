namespace Moedelo.CommissionAgents.Client.Dto
{
    public class ApiDataResult<T>
    {
        public T data { get; set; }

        public int StatusCode { get; set; }
    }
}
