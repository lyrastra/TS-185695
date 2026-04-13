namespace Moedelo.Salary.Dto
{
    public class ApiDataResult<T>
    {
        public T data { get; set; }

        public int StatusCode { get; set; }
    }
}
