using Moedelo.Common.Enums.Attributes;

namespace Moedelo.Common.Enums.Enums.FinancialOperations
{
    public enum MoneyBayType
    {
        /// <summary> Расчетный счет (по умолчанию)</summary>
        [Settlement]
        Settlement = 0,

        /// <summary> Касса </summary>
        [Cash]
        Cash = 1,

        /// <summary> Наличными без расчетного счета </summary>
        [Cash]
        CashWithoutSettlement = 2,

        /// <summary> Движение со счета на кассу </summary>
        [Cash]
        FromSettlementToCash = 3,

        /// <summary> Движение с кассы на счет </summary>
        [Cash]
        FromCashToSettlement = 4,

        /// <summary> Движение со счета на счет </summary>
        FromSettlementToSettlement = 5,

        /// <summary> Движение со счета на личный банковский кошелек </summary>
        [Settlement]
        FromSettlementToBankPurse = 6,

        /// <summary> Платежная система </summary>
        Purse = 10,

        /// <summary> Безденежные </summary>
        WithoutMoney = 11,

        /// <summary> Наличными на расчетный счет </summary>
        [Cash]
        CashWithSettlement = 12,

        /// <summary> По расчетной ведомости </summary>
        [Cash]
        CashPaybill = 13,

        /// <summary> Списание проданного товара </summary>
        WriteOff = 14,

        /// <summary> Имущество </summary>
        Effects = 15,

        /// <summary> Движение из платежной системы на р/сч </summary>
        [Settlement]
        FromPurseToSettlement = 16,

        /// <summary> По зарплатному проекту </summary>
        [Settlement]
        SalaryProject = 17
    }
}