namespace Moedelo.OfficeV2.Client
{
    // для совместимости с v1 api
    internal class DataDto<T>
    {
        public T Data { get; set; }
    }
}