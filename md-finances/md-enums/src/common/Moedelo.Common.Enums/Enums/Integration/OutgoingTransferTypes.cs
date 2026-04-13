using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Integration
{
    /// <summary> При добавлении типа денежной операции добавить тип в таблицу MoneyTransferOperationType </summary>
    public enum OutgoingTransferTypes
    {
        /// <summary> Не определено </summary>
        [Description("Не определено")]
        NotDefined = -1,
        
        /// <summary> Административные расходы </summary>
        [Description("Хозяйственные расходы")]
        OperatingExpenses,

        /// <summary> Покупка основного средства </summary>
        [Description("Покупка основного средства")]
        PurchaseOfFixedAssets,

        /// <summary> Выдача зарплаты </summary>
        [Description("Выплаты по зарплате")]
        PayDays,

        /// <summary> Бюджетный платеж </summary>
        [Description("Бюджетный платеж")]
        BudgetaryPayment,

        /// <summary> Прочие расходы </summary>
        [Description("Прочие списания")]
        OtherOutgoing,

        /// <summary> Снятие прибыли </summary>
        [Description("Снятие прибыли")]
        RemovingTheProfit,

        /// <summary> Погашение займа учредителю </summary>
        [Description("Погашение займа учредителю")]
        LoanParentOutgoing,

        /// <summary> Погашение займа третьим лицам </summary>
        [Description("Погашение займов")]
        LoansThirdPartiesOutgoing,

        /// <summary> Выплата дивидендов </summary>
        [Description("Выплата дивидендов")]
        DividendPayment,

        /// <summary> Расходы по виду деятельности </summary>
        [Description("По основному виду деятельности")]
        MainActivityOutgoing,

        /// <summary> Личный Расход </summary>
        [Description("Личный Расход")]
        PurseOutgoing,

        /// <summary> Выплата физ.лицу </summary>
        [Description("Выплата физ.лицу")]
        WorkerPayment,

        /// <summary> Выплата подотчётному лицу </summary>
        [Description("Выплаты подотчетным лицам")]
        GetMoneyForEmployee,

        /// <summary> Возврат денег клиенту </summary>
        [Description("Возврат денег клиенту")]
        RefundToCustomerOutgoing,

        /// <summary> Комиссия банка </summary>
        [Description("Комиссия банка")]
        BankFeeOutgoing,

        /// <summary> Эл.деньги или специальные счета </summary>
        [Description("Эл.деньги или специальные счета")]
        ElectronicOutgoing,

        /// <summary> Выплата по зарплатному проекту </summary>
        [Description("Выплата по зарплатному проекту")]
        SalaryProject
    }
}