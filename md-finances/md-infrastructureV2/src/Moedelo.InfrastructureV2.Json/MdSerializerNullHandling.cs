namespace Moedelo.InfrastructureV2.Json
{
    /// <summary>
    /// Настройки обработки свойств со значение null
    /// </summary>
    public enum MdSerializerNullHandling
    {
        /// <summary>
        /// сериализовать null-значения
        /// </summary>
        Include = 0,
        /// <summary>
        /// игнорировать свойства со значением null
        /// </summary>
        Ignore = 1
    }
}
