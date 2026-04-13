namespace Moedelo.KontragentsV2.Dto
{
    public sealed class KontragentResponse<T>
    {
        public T Data { get; set; }

        public KontragentResponse()
        {
        }

        public KontragentResponse(T data)
        {
            Data = data;
        }
    }
}