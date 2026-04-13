namespace Moedelo.Docs.Dto.Common
{
    public class ApiDataResult<T>
    {
        public T data { get; set; }

        public int StatusCode { get; set; }
    }
}