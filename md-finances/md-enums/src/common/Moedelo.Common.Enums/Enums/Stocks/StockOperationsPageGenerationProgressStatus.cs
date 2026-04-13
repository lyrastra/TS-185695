namespace Moedelo.Common.Enums.Enums.Stocks
{
    /// <summary>
    /// Состояние процесса генерации данных для страницы "движение"
    /// </summary>
    public enum StockOperationsPageGenerationProgressStatus
    {
        InProgress = 0,

        Completed = 1,

        Failed = 2,

        DidntStart = 3
    }
}
