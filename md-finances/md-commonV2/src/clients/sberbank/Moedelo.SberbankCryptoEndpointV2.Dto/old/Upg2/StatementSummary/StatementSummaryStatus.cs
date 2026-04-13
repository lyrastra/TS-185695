namespace Moedelo.SberbankCryptoEndpointV2.Dto.old.Upg2.StatementSummary
{
    /// <summary> Статус остатков по р/с </summary>
    public enum StatementSummaryStatus
    {
        None = 0,
        /// <summary> Получены </summary>
        Success = 1,
        /// <summary> В процессе формирования на стороне банка </summary>
        Processing = 2,
        /// <summary> Ошибка при получение остатков </summary>
        Fault = 3,
        /// <summary> Нет возможности получить остатки </summary>
        NoAccess = 4,
        /// <summary> Токен инвалидирован </summary>
        InvalidToken = 5
    }
}