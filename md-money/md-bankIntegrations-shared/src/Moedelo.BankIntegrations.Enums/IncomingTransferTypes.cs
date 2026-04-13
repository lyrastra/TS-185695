using System.ComponentModel;

namespace Moedelo.BankIntegrations.Enums
{
    public enum IncomingTransferTypes
    {
        /// <summary> Не определено</summary>
        [Description("Не определено")]
        NotDefined = -1,

        /// <summary> Продажа товара </summary>
        [Description("Продажа товаров")]
        SaleProduct,

        /// <summary> Оказание услуг </summary>
        [Description("Оказание услуг")]
        ProvisionOfServices,

        /// <summary> Агентское поступление </summary>
        [Description("По агентскому договору")]
        AgentIncoming,

        /// <summary> Взнос в УК </summary>
        [Description("Взнос в УК")]
        UkInpayment,

        /// <summary> Займ учредителя </summary>
        [Description("От учредителя")]
        LoanParent,

        /// <summary> Финансовая помощь </summary>
        [Description("Финансовая помощь")]
        MaterialAid,

        /// <summary> Взнос собственных средств </summary>
        [Description("Взнос собственных средств")]
        ContributionOfOwnFunds,

        /// <summary> Прочие поступления </summary>
        [Description("Прочие поступления")]
        OtherIncoming,

        /// <summary> Займы третьих лиц </summary>
        [Description("Займ от третьих лиц и контрагентов")]
        LoansThirdParties,

        /// <summary> Выручка по кассе или БСО </summary>
        [Description("Выручка по кассе или БСО")]
        CashIncoming,

        /// <summary> Личный доход </summary>
        [Description("Доход из платежной системы")]
        PurseIncoming,

        /// <summary> Возврат от поставщика </summary>
        [Description("Возврат от поставщика")]
        ReturnFromSupplier,

        /// <summary> Возврат ошибочно переведённых средств </summary>
        [Description("Возврат ошибочно переведённых средств")]
        ReturnFalseMeans,

        /// <summary> Возврат от подотчетного лица </summary>
        [Description("Возврат от подотчетного лица")]
        RefundFromEmployee
    }
}
