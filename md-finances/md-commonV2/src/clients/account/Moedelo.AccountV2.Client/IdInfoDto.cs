namespace Moedelo.AccountV2.Client
{
    internal class IdInfoDto<T> where T : struct
    {
        public T Id { get; set; }

        public IdInfoDto(T id)
        {
            Id = id;
        }
    }
}
