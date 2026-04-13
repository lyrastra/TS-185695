namespace Moedelo.AstralV3.Client.AstralInteractionsLogger.Enums
{
    /// <summary>
    /// Возможные режимы логирования для метода
    /// </summary>
    public enum MethodLoggingMode
    {
        /// <summary>
        /// Не логировать ни запрос, ни ответ
        /// </summary>
        LogNothing = 0,

        /// <summary>
        /// Логировать запрос
        /// </summary>
        LogRequest = 1,

        /// <summary>
        /// Логировать ответ
        /// </summary>
        LogResponse = 2,

        /// <summary>
        /// Логировать и запрос, и ответ
        /// </summary>
        LogAll = 3
    }
}
