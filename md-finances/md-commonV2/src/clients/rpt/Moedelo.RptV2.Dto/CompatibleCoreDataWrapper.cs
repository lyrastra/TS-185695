namespace Moedelo.RptV2.Dto
{
    public class CompatibleCoreDataWrapper<T>
    {
        public CompatibleCoreDataWrapper(T Data)
        {
            data = Data;
        }

        public T data { get; set; }
    }
}