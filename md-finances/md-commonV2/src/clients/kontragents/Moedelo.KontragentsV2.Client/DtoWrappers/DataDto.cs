namespace Moedelo.KontragentsV2.Client.DtoWrappers
{
    /// <summary>
    /// Для поддержания контракта старого API в v2-client'е 
    /// </summary>
    internal class DataDto<T>
    {
        public T Data { get; set; }
    }
}