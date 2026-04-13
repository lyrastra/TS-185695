namespace Moedelo.AccountingV2.Dto.AccountingStatement
{
    public class ApiDataResult<T>
    {
        public T data { get; set; }

        public int StatusCode { get; set; }
    }
}
