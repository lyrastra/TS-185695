namespace Moedelo.Stock.Enums
{
    /// <summary>
    /// source: https://github.com/moedelo/md-enums/blob/7f9d3fe47d57705fc1b7a14a5c907cda020b5751/src/common/Moedelo.Common.Enums/Enums/Documents/NdsPositionType.cs#L3
    /// </summary>
    public enum NdsPositionType
    {
        /// <summary>
        /// НДС не используется
        /// </summary>
        None = 1,

        /// <summary>
        /// Сверху
        /// </summary>
        Top = 2,

        /// <summary>
        /// В том числе
        /// </summary>
        Inside = 3
    }
}