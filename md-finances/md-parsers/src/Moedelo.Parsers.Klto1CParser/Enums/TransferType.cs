using System.ComponentModel;
using Moedelo.Parsers.Klto1CParser.Attributes;

namespace Moedelo.Parsers.Klto1CParser.Enums
{
    public enum TransferType
    {
        /// <summary> Не определено</summary>
        [Description("Не определено")]
        [OperationAllowType(OperationType.Default)]
        NotDefined = -1,

        /// <summary> Продажа товара </summary>
        [Description("Продажа товаров")]
        [OperationAllowType(OperationType.Debit)]
        SaleProduct = 0,

        /// <summary> Оказание услуг </summary>
        [Description("Оказание услуг")]
        [OperationAllowType(OperationType.Debit)]
        ProvisionOfServices = 1,

        /// <summary> Агентское поступление </summary>
        [Description("По агентскому договору")]
        [OperationAllowType(OperationType.Debit)]
        AgentIncoming = 2,

        /// <summary> Взнос в УК </summary>
        [Description("Взнос в УК")]
        [OperationAllowType(OperationType.Debit)]
        UkInpayment = 3,

        /// <summary> Займ учредителя </summary>
        [Description("От учредителя")]
        [OperationAllowType(OperationType.Debit)]
        LoanParent = 4,

        /// <summary> Финансовая помощь </summary>
        [Description("Финансовая помощь")]
        [OperationAllowType(OperationType.Debit)]
        MaterialAid = 5,

        /// <summary> Взнос собственных средств </summary>
        [Description("Взнос собственных средств")]
        [OperationAllowType(OperationType.Debit)]
        ContributionOfOwnFunds = 6,

        /// <summary> Прочие поступления </summary>
        [Description("Прочие поступления")]
        [OperationAllowType(OperationType.Debit)]
        OtherIncoming = 7,

        /// <summary> Займы третьих лиц </summary>
        [Description("Займ от третьих лиц и контрагентов")]
        [OperationAllowType(OperationType.Debit)]
        LoansThirdParties = 8,

        /// <summary> Выручка по кассе или БСО </summary>
        [Description("Выручка по кассе или БСО")]
        [OperationAllowType(OperationType.Debit)]
        CashIncoming = 9,

        /// <summary> Доход в платежной системе </summary>
        [Description("Доход в платежной системе")]
        [OperationAllowType(OperationType.Debit)]
        PurseIncoming = 10,

        /// <summary> Возврат от поставщика </summary>
        [Description("Возврат от поставщика")]
        [OperationAllowType(OperationType.Debit)]
        ReturnFromSupplier = 11,

        /// <summary> Возврат ошибочно переведённых средств </summary>
        [Description("Возврат ошибочно переведённых средств")]
        [OperationAllowType(OperationType.Debit)]
        ReturnFalseMeans = 12,

        /// <summary> Возврат от подотчетного лица </summary>
        [Description("Возврат от подотчетного лица")]
        [OperationAllowType(OperationType.Debit)]
        RefundFromEmployee = 13,

        /// <summary> Поступление (возврат) из бюджета </summary>
        [Description("Поступление (возврат) из бюджета")]
        [OperationAllowType(OperationType.Debit)]
        RefundFromBudgetIncomingOperation = 14,

        /// <summary> Курсовая разница </summary>
        [Description("Курсовая разница")]
        [OperationAllowType(OperationType.Debit)]
        IncomeExchangeDiff = 15,

        /// <summary> Поступление от покупки/продажи валюты </summary>
        [Description("Поступление от покупки/продажи валюты")]
        [OperationAllowType(OperationType.Debit)]
        CurrencyPurchaseAndSaleIncoming = 16,

        /// <summary> Административные расходы </summary>
        [Description("Хозяйственные расходы")]
        [OperationAllowType(OperationType.Credit)]
        OperatingExpenses = 17,

        /// <summary> Покупка основного средства </summary>
        [Description("Покупка основного средства")]
        [OperationAllowType(OperationType.Credit)]
        PurchaseOfFixedAssets = 18,

        /// <summary> Выдача зарплаты </summary>
        [Description("Выплаты по зарплате")]
        [OperationAllowType(OperationType.Credit)]
        PayDays = 19,

        /// <summary> Бюджетный платеж </summary>
        [Description("Бюджетный платеж")]
        [OperationAllowType(OperationType.Credit)]
        BudgetaryPayment = 20,

        /// <summary> Прочие расходы </summary>
        [Description("Прочие списания")]
        [OperationAllowType(OperationType.Credit)]
        OtherOutgoing = 21,

        /// <summary> Снятие прибыли </summary>
        [Description("Снятие прибыли")]
        [OperationAllowType(OperationType.Credit)]
        RemovingTheProfit = 22,

        /// <summary> Погашение займа учредителю </summary>
        [Description("Погашение займа учредителю")]
        [OperationAllowType(OperationType.Credit)]
        LoanParentOutgoing = 23,

        /// <summary> Погашение займа третьим лицам </summary>
        [Description("Погашение займов")]
        [OperationAllowType(OperationType.Credit)]
        LoansThirdPartiesOutgoing = 24,

        /// <summary> Выплата дивидендов </summary>
        [Description("Выплата дивидендов")]
        [OperationAllowType(OperationType.Credit)]
        DividendPayment = 25,

        /// <summary> Расходы по виду деятельности </summary>
        [Description("По основному виду деятельности")]
        [OperationAllowType(OperationType.Credit)]
        MainActivityOutgoing = 26,

        /// <summary> Личный Расход </summary>
        [Description("Личный Расход")]
        [OperationAllowType(OperationType.Credit)]
        PurseOutgoing = 27,

        /// <summary> Выплата физ.лицу </summary>
        [Description("Выплата физ.лицу")]
        [OperationAllowType(OperationType.Credit)]
        WorkerPayment = 28,

        /// <summary> Выплата подотчётному лицу </summary>
        [Description("Выплаты подотчетным лицам")]
        [OperationAllowType(OperationType.Credit)]
        GetMoneyForEmployee = 29,

        /// <summary> Возврат денег клиенту </summary>
        [Description("Возврат денег клиенту")]
        [OperationAllowType(OperationType.Credit)]
        RefundToCustomerOutgoing = 30,

        /// <summary> Комиссия банка </summary>
        [Description("Комиссия банка")]
        [OperationAllowType(OperationType.Credit)]
        BankFeeOutgoing = 31,

        /// <summary> Комиссия платежной системы </summary>
        [Description("Комиссия платежной системы")]
        [OperationAllowType(OperationType.Credit)]
        ElectronicOutgoing = 32,

        /// <summary> Выплата по зарплатному проекту </summary>
        [Description("Выплата по зарплатному проекту")]
        [OperationAllowType(OperationType.Credit)]
        SalaryProject = 33,

        /// <summary> Курсовая разница </summary>
        [Description("Курсовая разница")]
        [OperationAllowType(OperationType.Credit)]
        OutgoExchangeDiff = 34,

        /// <summary> По удержанию </summary>
        [Description("По удержанию")]
        [OperationAllowType(OperationType.Credit)]
        SalaryDeduction = 35,

        /// <summary> Покупка/продажа валюты </summary>
        [Description("Покупка/продажа валюты")]
        [OperationAllowType(OperationType.Credit)]
        CurrencyPurchaseAndSale = 36,

        /// <summary> Перевод с р/с на р/сч </summary>
        [Description("Перевод с р/с на р/сч")]
        [OperationAllowType(new[] { OperationType.Credit, OperationType.Debit })]
        MovementFromSettlementToSettlement = 37,

        /// <summary> Перевод с р/с в кассу </summary>
        [Description("Перевод с р/с в кассу")]
        [OperationAllowType(OperationType.Credit)]
        MovementFromSettlementToCash = 38,

        /// <summary> Перевод с кассы на р/сч </summary>
        [Description("Перевод с кассы на р/сч")]
        [OperationAllowType(OperationType.Debit)]
        MovementFromCashToSettlement = 39,

        /// <summary> Выдача займа </summary>
        [Description("Выдача займа")]
        [OperationAllowType(OperationType.Credit)]
        LoanIssue = 40,

        /// <summary> Возврат займа или процентов </summary>
        [Description("Возврат займа или процентов")]
        [OperationAllowType(OperationType.Debit)]
        LoanReturn = 41
    }
}