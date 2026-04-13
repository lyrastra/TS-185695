namespace Moedelo.Pdf417V2.Dto
{
    public class DataDto<T>
    {
        public T Data { get; set; }

        public DataDto(T data)
        {
            this.Data = data;
        }
    }
}