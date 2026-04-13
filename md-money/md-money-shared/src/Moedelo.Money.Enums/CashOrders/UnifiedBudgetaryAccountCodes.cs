using System.ComponentModel;

namespace Moedelo.Money.Enums.CashOrders
{
    /// <summary>
    /// Тип взноса (Бух. коды)
    /// </summary>
    public enum UnifiedBudgetaryAccountCodes
    {
        /// <summary>
        ///Единый налог при применении упрощенной системы налогообложения
        /// </summary>
        [Description("Единый налог при применении упрощенной системы налогообложения")]
        EnvdForUsn = 681200,

        /// <summary>
        /// Налог при патентной системе налогообложения
        /// </summary>
        [Description("Налог при патентной системе налогообложения")]
        Patent = 681400,

        /// <summary>
        /// Страховые взносы (ОПС, ОМС и ОСС по ВНиМ)
        /// </summary>
        [Description("Страховые взносы (ОПС, ОМС и ОСС по ВНиМ)")]
        InsuranceFee = 691000,
    }
}