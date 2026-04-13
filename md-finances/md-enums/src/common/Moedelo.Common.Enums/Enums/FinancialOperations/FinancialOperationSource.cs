namespace Moedelo.Common.Enums.Enums.FinancialOperations
{
    /// <summary>
    /// Источник создания/изменения денежной операции
    /// </summary>
    public enum FinancialOperationSource
    {
        Unknown = 0,
        /// <summary> из UI раздел деньги </summary>
        MoneyManual = 1,
        /// <summary> из авансового отчета </summary>
        AdvanceStatement = 2,
        /// <summary> платежи за сервис </summary>
        PaymentForService = 3,
        /// <summary> из зарплаты </summary>
        Payroll = 4,
        /// <summary> из мастера торгового сбора </summary>
        TradingTaxMaster = 5,
        /// <summary> из мастера оплаты патента </summary>
        PatentMaster = 6,
        /// <summary> из внешнего апи </summary>
        External = 7,
        /// <summary> </summary>
        EWalletComission = 8,
        /// <summary> </summary>
        PurseOperation = 9,
        /// <summary> </summary>
        BusinessTrip = 10,
        /// <summary> из импорта </summary>
        Import = 11,
        /// <summary> фиксы разработчиков по TS и т.п. </summary>
        Deveveloper = 12,
    }
}