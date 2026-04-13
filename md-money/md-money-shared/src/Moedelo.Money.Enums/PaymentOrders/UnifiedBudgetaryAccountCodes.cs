using System.ComponentModel;

namespace Moedelo.Money.Enums.PaymentOrders
{
    /// <summary>
    /// Тип взноса (Бух. коды)
    /// </summary>
    public enum UnifiedBudgetaryAccountCodes
    {
        /// <summary>
        /// Налог на доходы физических лиц
        /// </summary>
        [Description("Налог на доходы физических лиц")]
        Ndfl = 680100,

        /// <summary>
        /// Налог на добавленную стоимость
        /// </summary>
        [Description("Налог на добавленную стоимость")]
        Nds = 680200,

        /// <summary>
        /// Налог на прибыль
        /// </summary>
        [Description("Налог на прибыль")]
        ProfitTaxes = 680400,

        /// <summary>
        ///Туристический налог
        /// </summary>
        [Description("Туристический налог")]
        TouristTaxes = 680600,

        /// <summary>
        /// Транспортный налог
        /// </summary>
        [Description("Транспортный налог")]
        TransportTaxes = 680700,

        /// <summary>
        /// Налог на имущество
        /// </summary>
        [Description("Налог на имущество")]
        PropertyTaxes = 680800,

        /// <summary>
        /// Торговый сбор
        /// </summary>
        [Description("Торговый сбор")]
        TradingFees = 680900,

        /// <summary>
        /// Прочие налоги и сборы
        /// </summary>
        [Description("Прочие налоги и сборы")]
        OtherTaxes = 681000,

        /// <summary>
        ///Единый сельскохозяйственный налог (ЕСХН)
        /// </summary>
        [Description("Единый сельскохозяйственный налог (ЕСХН)")]
        Eshn = 681100,

        /// <summary>
        ///Единый налог при применении упрощенной системы налогообложения
        /// </summary>
        [Description("Единый налог при применении упрощенной системы налогообложения")]
        EnvdForUsn = 681200,

        /// <summary>
        /// Земельный налог
        /// </summary>
        [Description("Земельный налог")]
        LandTaxes = 681300,

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