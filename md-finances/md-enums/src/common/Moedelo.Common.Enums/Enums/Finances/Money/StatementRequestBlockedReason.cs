namespace Moedelo.Common.Enums.Enums.Finances.Money
{
    /// <summary>
    /// Причина, по которой запрос выписки невозможен
    /// </summary>
    public enum StatementRequestBlockedReason
    {
        /// <summary>
        /// Р/сч невалиден с т. з. интеграции
        /// </summary>
        SettlementAccountNotValid = 1,
        
        /// <summary>
        /// Интеграция отключена
        /// </summary>
        IntegrationDisabled = 2,
        
        /// <summary>
        /// Необработанный запрос в очереди запросов к банку
        /// </summary>
        ExistsUnprocessedRequest = 3,
    }
}