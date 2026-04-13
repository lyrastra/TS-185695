namespace Moedelo.RequisitesV2.Dto
{
    public class ApiDataResult<T>
    {
        public T Data { get; set; }

        public int StatusCode { get; set; }
    }
}