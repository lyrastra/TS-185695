namespace Moedelo.AccountingStatements.Enums
{
    public enum AccountingStatementType
    {
        Default = 0,
        /// <summary> Бух-справка из покупок или продаж </summary>
        AccountingStatement = 1,
        /// <summary> Бух-справка на подтверждение амортизации ОС </summary>
        Amortization = 2,
        /// <summary> Бух-справка на подтверждение себестоимости </summary>
        SelfCost = 3,
        /// <summary> Системные бух справки, создающиеся вместе с первичным документом </summary>
        SystemAccountingStatement = 4,
        /// <summary> Бухсправка, описывающая формирование уставного капитала </summary>
        AuthorizedCapital = 5,
        /// <summary> Из декларации по налогу на прибыль </summary>
        ProfitWizard = 6,
        /// <summary> Бух-справка на создание внеоборотного актива амортизации ОС </summary>
        FixedAssetInvestment = 7,
        /// <summary> Бух-справка для налога на имущество </summary>
        EstateTax = 8,
        /// <summary> Бух-справка создающаяся при вводе остатков </summary>
        AccountingBalance = 9,
        /// <summary> Бух-справка создающаяся при завершении декларации/аванса по УСН </summary>
        UsnDeclaration = 10,
        /// <summary> Бух-справка создающаяся при завершении декларации по ЕНВД </summary>
        EnvdDeclaration = 11,
        /// <summary> Формирование расходов за месяц </summary>
        ClosingExpenses = 12,
        /// <summary> Формирование финансового результата за месяц </summary>
        FinancialResult = 13,
        /// <summary> Закрытие года </summary>
        ClosingYear = 14,
        /// <summary> Торговый сбор </summary>
        TradingFeesPayment = 15,
        /// <summary> Комиссия за эквайринг </summary>
        AcquiringComission = 16,
        /// <summary> Начисление по займу/кредиту</summary>
        ReceivedLoan = 17,
        /// <summary> Выбытие ОС </summary>
        DismissFixedAsset = 18,
        /// <summary> Возврат себестоимости (при розничном возврате) </summary>
        RefundSelfCost = 19,

        /// <summary>
        /// Списание расходов в виде амортизации на право пользования активом [наименование ОС], а также арендных платежей по нему
        /// </summary>
        FixedAssetRentPayments = 20,

        /// <summary>
        /// Прекращение права пользования активом (выкуп) [Наменование ОС]
        /// </summary>
        FixedAssetBuyout = 21,

        /// <summary>
        /// Зачет авансового платежа в оплату месячной арендной платы по [наименование ОС]"
        /// </summary>
        FixedAssetRentAdvancePayments = 22,

        /// <summary>
        /// Признание расходов на оплату арендной платы
        /// </summary>
        RecognitionExpenseRentPayments = 23,

        /// <summary>
        /// Начисление процентов по полученному/выданному займу
        /// </summary>
        Loan = 24,

        /// <summary>
        /// Переоценка валюты по итогу каждого месяца
        /// </summary>
        RecalculateCurrencyRemains = 25,

        /// <summary>
        /// Перерасчёт себестоимости остатка на складе комиссионера
        /// </summary>
        RecalculateRemainsOnCommissionAgentStock = 26,

        /// <summary>
        /// Себестоимость НУ
        /// </summary>
        SelfCostTax = 27,

        /// <summary>
        /// Перенос остатков по налогам и взносам на ЕНС (разовый за январь 2023 из МЗМ)
        /// </summary>
        UnifiedBudgetarAccountsRemainsTransfer = 28,

        /// <summary>
        /// Перенос сальдо по налогам и взносам на ЕНС (переодический из мастера ЕНП)
        /// </summary>
        UnifiedBudgetarAccountsTransfer = 29,

        /// <summary>
        /// Перенос сальдо по НДФЛ и взносам на ЕНС (по начислениям из МЗМ с 2024 года)
        /// </summary>
        NdflAndFundsAccountsTransfer = 30,

        /// <summary> 
        /// Формирование расходов по взносам (фиксированному и дополнительному — ФВ и ДВ)
        /// </summary>
        UsnFundPaymentExpenses = 31
    }
}