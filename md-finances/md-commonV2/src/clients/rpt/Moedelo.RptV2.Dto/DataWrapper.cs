namespace Moedelo.RptV2.Dto
{
    public class DataWrapper<T>
    {
        public DataWrapper(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}
