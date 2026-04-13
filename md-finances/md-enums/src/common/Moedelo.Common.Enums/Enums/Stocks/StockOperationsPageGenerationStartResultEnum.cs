namespace Moedelo.Common.Enums.Enums.Stocks
{
    /// <summary>
    /// Возможные результаты запуска генерации "Движения"
    /// </summary>
    public enum StockOperationsPageGenerationStartResultEnum
    {
        /// <summary>
        /// Генерация запущена
        /// </summary>
        Started = 0,

        /// <summary>
        /// Генерация была запущена ранее, надо дождаться завершения
        /// </summary>
        AlreadyInProgress = 1
    }
}
