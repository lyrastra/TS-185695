namespace Moedelo.ErptV2.Dto
{
    public class DataDto<T> : BaseDto
    {
        public DataDto(T data)
        {
            Data = data;
        }

        public T Data { get; set; }
    }
}