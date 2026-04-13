namespace Moedelo.Common.Enums.Enums.BalanceBiz
{
    public enum BizSubcontoType
    {
        /// <summary>
        /// Касса
        /// </summary>
        Cash = 1,

        /// <summary>
        /// Доходы будущих периодов
        /// </summary>
        FutureIncome = 2,

        /// <summary>
        /// Расходы будущих периодов
        /// </summary>
        FutureOutcome = 3,

        /// <summary>
        /// Выручка
        /// </summary>
        Income = 4,

        /// <summary>
        /// Расходы
        /// </summary>
        Outcome = 5,

        /// <summary>
        /// Прочие доходы
        /// </summary>
        OtherIncome = 6,

        /// <summary>
        /// Прочие расходы
        /// </summary>
        OtherOutcome = 7,

        /// <summary>
        /// Нераспределенная прибыль
        /// </summary>
        SaldoIncome = 8,

        /// <summary>
        /// Непокрытый убыток
        /// </summary>
        SaldoOutcome = 9,

        /// <summary>
        /// Непокрытый убыток прошлых лет
        /// </summary>
        SaldoOutcomeSalary = 10,

        /// <summary>
        /// Проценты к уплате
        /// </summary>
        PercentsForPayment = 11,

        /// <summary>
        /// Налог УСН
        /// </summary>
        TaxUsn = 12,

        /// <summary>
        /// Налог ЕНВД
        /// </summary>
        TaxEnvd = 13,

        /// <summary>
        /// УСН
        /// </summary>
        Usn = 14,

        /// <summary>
        /// ЕНВД
        /// </summary>
        Envd = 15,

        /// <summary>
        /// Депозиты
        /// </summary>
        Deposit = 16,

        /// <summary>
        /// Расходы по обычному виду деятельности
        /// </summary>
        MainActivity = 17,

        /// <summary>
        /// НДФЛ
        /// </summary>
        Ndfl = 18,

        /// <summary>
        /// ФСС
        /// </summary>
        Fss = 19,

        /// <summary>
        /// ПФР
        /// </summary>
        Pfr = 20,

        /// <summary>
        /// ОМС
        /// </summary>
        Oms = 21,

        /// <summary>
        /// Страхование от несчастных случаев
        /// </summary>
        AccidentInsurance = 22,

        /// <summary>
        /// Контрагент
        /// </summary>
        Kontragent = 24,

        /// <summary>
        /// Сотрудник
        /// </summary>
        Worker = 25,

        /// <summary>
        /// Расчетный счет
        /// </summary>
        Settlement = 26,

        /// <summary>
        /// Платежная система
        /// </summary>
        Purse = 27,

        /// <summary>
        /// Тип бюджетной операции
        /// </summary>
        Budgetary = 28,

        /// <summary>
        /// Товар
        /// </summary>
        Product = 29,

        /// <summary>
        /// Материал
        /// </summary>
        Material = 30,

        /// <summary>
        /// Остновное средство
        /// </summary>
        Asset = 31,

        /// <summary>
        /// Агентский НДС
        /// </summary>
        AgentNds = 32,

        /// <summary>
        /// Основное средство
        /// </summary>
        TargetFund = 33,

        /// <summary>
        /// Торговый сбор
        /// </summary>
        TradingTax = 34,

        /// <summary>
        /// Товар на складе (составное субконто Товар-Склад)
        /// </summary>
        ProductOnStock = 35,

        /// <summary>
        /// Товары
        /// </summary>
        Products = 36,

        /// <summary>
        /// Сырье и метриалы
        /// </summary>
        Materials = 37

    }
}
