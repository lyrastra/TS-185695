namespace Moedelo.Common.Enums.Enums.Documents
{
    /// <summary>
    /// Значения свойства "Подписан" (Продажи: акты, наклыдные, счета-фактуры, отчеты посредника)
    /// </summary>
    public enum SignStatus
    {
        /// <summary>
        /// Нет
        /// </summary>
        Default = 0,

        /// <summary>
        /// Скан
        /// </summary>
        OnSigning = 1,

        /// <summary>
        /// Да
        /// </summary>
        Signed = 2
    }
}