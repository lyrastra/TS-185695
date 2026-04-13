using System.ComponentModel;

namespace Moedelo.Docs.Enums
{
    /// <summary>
    /// Значения свойства "Подписан" (Продажи: акты, наклыдные, счета-фактуры, отчеты посредника)
    /// </summary>
    public enum SignStatus
    {
        /// <summary>
        /// Нет
        /// </summary>
        [Description("Нет")]
        Default = 0,
        
        /// <summary>
        /// Скан
        /// </summary>
        [Description("Скан")]
        OnSigning = 1,
        
        /// <summary>
        /// Да
        /// </summary>
        [Description("Да")]
        Signed = 2
    }
}