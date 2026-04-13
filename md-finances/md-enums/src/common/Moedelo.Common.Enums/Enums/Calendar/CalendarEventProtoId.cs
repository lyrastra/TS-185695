namespace Moedelo.Common.Enums.Enums.Calendar
{
    /// <summary>
    ///     Идентификатор прототипа календарного события
    ///     В БД исторически хранятся строковые значения под именем Type
    ///     Это означает, что переименовывать значения этого Enum нельзя
    /// </summary>
    public enum CalendarEventProtoId
    {
        // from Moedelo.Domain.Models.Accounting.CalendarEvents
        /// <summary> Закрытие месяца </summary>
        CloseAccountingPeriodCalendarEvent = 100,

        /// <summary> Закрытие года </summary>
        CloseAccountingYearCalendarEvent = 101,

        // from Moedelo.Domain.Models.AccountingBalances
        /// <summary> Календарное для информирования о необходимости ввода остатков </summary>
        AccountingBalancesCalendarEvent = 200,

        // from Moedelo.Domain.Models.Funds.AccountingPolicies.CalendarEvents
        AccountingPolicies2014CalendarEvent = 300,
        AccountingPoliciesCalendarEvent = 301,

        // from Moedelo.Domain.Models.Funds.FNS.Payments.CalendarEvents
        /// <summary> Бухгалтерская отчетность </summary>
        AccountingReportCalendarEvent = 400,

        /// <summary> Бухгалтерский баланс и отчет о финансовых результатах </summary>
        BalanceAndIncomeCalendarEvent2013 = 401,
        CloseBizPeriodCalendarEvent = 402,
        EstateDeclarationCalendarEvent = 403,

        /// <summary> Декларация по НДС за ... </summary>
        NdsDeclarationCalendarEvent = 404,
        PatentPaymentCalendarEvent = 405,

        /// <summary> Оплатить аванс по УСН за </summary>
        PrepaymentByUsn2011CalendarEvent = 406,

        /// <summary> Оплатить аванс по УСН за </summary>
        PrepaymentByUsn2012CalendarEvent = 407,

        /// <summary> Оплатить аванс по УСН за </summary>
        PrepaymentByUsn2013CalendarEvent = 408,

        /// <summary> Оплатить аванс по УСН за </summary>
        PrepaymentByUsn2014CalendarEvent = 409,

        /// <summary> Оплатить аванс по УСН за </summary>
        PrepaymentByUsnCalendarEvent = 410,

        /// <summary> Авансовый платеж по налогу на прибыль </summary>
        ProfitAdvancePaymentCalendarEvent = 411,

        /// <summary> Декларация по налогу на прибыль (form md-biz) </summary>
        ProfitDeclarationCalendarEvent = 412,
        UsnDeclarationCalendarEvent = 413,

        // from Moedelo.Domain.Models.Funds.FNS.Reports.CalendarEvents
        /// <summary> Подать сведения о среднесписочной численности за ... </summary>
        AverageWorkerNumbersCalendarEvents = 500,
        AverageWorkerNumbersUserEvent = 501,
        DeclarationByUSN2010CalendarEvent = 502,
        DeclarationByUSN2011CalendarEvent = 503,
        DeclarationByUSN2012CalendarEvent = 504,
        DeclarationByUSN2013CalendarEvent = 505,
        DeclarationByUSN2014CalendarEvent = 506,
        DeclarationByUSN2015CalendarEvent = 507,

        /// <summary> Отчитаться в налоговую и заплатить ЕНВД за ... </summary>
        Envd2012CalendarEvent = 508,

        /// <summary> Отчитаться в налоговую и заплатить ЕНВД за ... </summary>
        Envd2013CalendarEvent = 509,

        /// <summary> Отчитаться в налоговую и заплатить ЕНВД за ... </summary>
        Envd2014CalendarEvent = 510,

        /// <summary> Отчитаться в налоговую и заплатить ЕНВД за ... </summary>
        Envd2015CalendarEvent = 511,

        /// <summary> Отчитаться в налоговую и заплатить ЕНВД за ... </summary>
        Envd2016CalendarEvent = 512,

        /// <summary> Отчитаться в налоговую и заплатить ЕНВД за ... </summary>
        Envd2017CalendarEvent = 518,

        /// <summary> Отчитаться в налоговую и заплатить ЕНВД за ... </summary>
        Envd2018CalendarEvent = 519,

        /// <summary> Отчитаться в налоговую и заплатить ЕНВД за ... </summary>
        Envd2019CalendarEvent = 520,

        /// <summary> Отчитаться в налоговую и заплатить ЕНВД за ... </summary>
        Envd2020CalendarEvent = 521,

        /// <summary> Отчитаться в налоговую и заплатить ЕНВД за ... </summary>
        ENVDCalendarEvent = 513,

        /// <summary> Сдать расчет по НДФЛ за ... </summary>
        Fns6NdflCalendarEvent = 514,

        /// <summary> Подать сведения по выплатам физическим лицам за ... </summary>
        Info2NDFLCalendarEvent = 515,

        /// <summary> Уведомить ИФНС о торговом сборе </summary>
        TradingTaxNotificationCalendarEvent = 516,

        /// <summary> Оплатить торговый сбор за ... </summary>
        TradingTaxPaymentCalendarEvent = 517,

        // from Moedelo.Domain.Models.Funds.FSS.Reports.CalendarEvents
        ConfirmMainOkvedCalendarEvent = 600,

        /// <summary> Отчет в ФСС за ... </summary>
        FSSQuarterReport2010CalendarEvent = 601,

        /// <summary> Отчет в ФСС за ... </summary>
        FSSQuarterReport2011CalendarEvent = 602,

        /// <summary> Отчет в ФСС за ... (is it real event type?)/// </summary>
        FSSQuarterReportCalendarEvent = 603,

        // from Moedelo.Domain.Models.Funds.Letters.CalendarEvents
        CloseSettlementAccountCalendarEvent = 700,
        OpenNewSettlementAccountCalendarEvent = 701,
        OpenSettlementAccountCalendarEvent = 702,

        // from Moedelo.Domain.Models.Funds.Payments.CalendarEvents
        /// <summary> Зарплатные налоги 2010 год </summary>
        PayrollTaxes2010CalendarEvent = 800,

        /// <summary> Зарплатные налоги 2011 год </summary>
        PayrollTaxes2011CalendarEvent = 801,

        /// <summary> Зарплатные налоги 2012 год </summary>
        PayrollTaxes2012CalendarEvent = 802,

        /// <summary> Зарплатные налоги 2013 год </summary>
        PayrollTaxes2013CalendarEvent = 803,

        /// <summary> Зарплатные налоги 2014 год </summary>
        PayrollTaxes2014CalendarEvent = 804,

        /// <summary> Зарплатные налоги 2015 год </summary>
        PayrollTaxes2015CalendarEvent = 805,

        /// <summary> Зарплатные налоги 2016 год </summary>
        PayrollTaxes2016CalendarEvent = 806, // copied from md-biz

        /// <summary>
        /// Зарплатные налоги 2017 год
        /// </summary>
        /// <remarks>
        /// 29.11.2017 было принято решение больше не заводить типы на каждый год для данного отчета
        /// </remarks>
        PayrollTaxes2017CalendarEvent = 799,

        /// <summary>Зарплатыне налоги с 2023 года</summary>
        PayrollTaxes2023CalendarEvent = 798,

        // from Moedelo.Domain.Models.Funds.PFR.Payments.CalendarEvents
        /// <summary> Фиксированные взносы в ПФР за 2011 год </summary>
        FinalInpaymentsToPFR2011CalendarEvent = 807,

        /// <summary> Фиксированные взносы в ПФР за 2012 год </summary>
        FinalInpaymentsToPFR2012CalendarEvent = 808,

        /// <summary> Фиксированные взносы в ПФР за текущий год </summary>
        FinalInpaymentsToPFRCalendarEvent = 809,

        /// <summary> Оплатить взносы в ПФР за 2010 год </summary>
        InpaymentsToPFR2010CalendarEvent = 810,

        /// <summary> Оплатить взносы в ПФР за 2011 год </summary>
        InpaymentsToPFR2011CalendarEvent = 811,

        /// <summary> Оплатить взносы в ПФР за 2012 год </summary>
        InpaymentsToPFR2012CalendarEvent = 812,

        /// <summary> Оплатить взносы в ПФР текущий год </summary>
        InpaymentsToPFRCalendarEvent = 813,

        // from Moedelo.Domain.Models.Funds.PFR.Reports.CalendarEvents
        /// <summary> Календарный ивент для отчёта в ПФР по фиксированным взносам </summary>
        FixedInpaymentsCalendarEvent = 900,

        /// <summary> Календарный ивент для персонифицированного отчёта за 2012 год </summary>
        PfrSzvmCalendarEvent = 901,

        /// <summary> Календарный ивент для персонифицированного отчёта за 2010 год </summary>
        PfrWorkerReport2010CalendarEvent = 902,

        /// <summary> Календарный ивент для персонифицированного отчёта за 2011 год </summary>
        PfrWorkerReport2011CalendarEvent = 903,

        /// <summary> Календарный ивент для персонифицированного отчёта за 2012 год </summary>
        PfrWorkerReport2012CalendarEvent = 904,

        // from Moedelo.Domain.Models.Funds.ProfitAndLossReport
        ProfitAndLossReportCalendarEvent = 1000,

        // from Moedelo.Domain.Models.Funds.RosStat.CalendarEvents
        /// <summary> Отчёт в РосСтат за ... год </summary>
        RosStatReportCalendarEvent = 1100,

        /// <summary> Отчёт в РосСтат за 2012 год </summary>
        RosStatReportCalendarEvent2012 = 1101,

        /// <summary> Отчёт в РосСтат за 2013 год </summary>
        RosStatReportCalendarEvent2013 = 1102,

        /// <summary> Отчёт в РосСтат за 2014 год </summary>
        RosStatReportCalendarEvent2014 = 1103,

        /// <summary> Отчёт в РосСтат за 2015 год </summary>
        RosStatReportCalendarEvent2015 = 1104,
        RosStatReportIpCalendarEvent2012 = 1105,
        RosStatReportIpCalendarEvent2013 = 1106,
        RosStatReportIpCalendarEvent2014 = 1107,

        // from Moedelo.Domain.Models.MoneyBalanceMaster
        MoneyBalanceMasterCalendarEvent = 1200,

        // from Moedelo.Domain.Models.Patents
        PatentUserCalendarEvent = 1300,

        /// <summary>
        /// Подать заявление на продление ПСН
        /// </summary>
        PatentNotifyAboutProlongationUserEvent = 1310,

        /// <summary>
        /// Подать заявление на вычет по ПСН
        /// </summary>
        PatentNotifyAboutDeductionUserEvent = 1320,

        // from Moedelo.Domain.Rpt.PfrFixedPayments
        /// <summary> Оплатить дополнительные взносы в ПФ за ... </summary>
        PfrFixedPaymentsCalendarEvent = 1400,

        /// <summary>
        /// Расчет по страховым взносам за %
        /// Расчет по страховым взносам за 9 месяцев % года
        /// Расчет по страховым взносам за I полугодие % года
        /// Расчет по страховым взносам за I квартал % года
        /// </summary>
        CalcInsuranceContributionsCalendarEvent = 1500,

        /// <summary>
        /// Отчет по страховым взносам в ФСС с 2017 года
        /// </summary>
        FssCalendarEvent = 1600,

        /// <summary>
        /// Расчет по страховым взносам с 2017 года
        /// </summary>
        RsvCalendarEvent = 1700,

        /// <summary>
        /// Расчет по страховым взносам с 2023 года
        /// </summary>
        Rsv2023CalendarEvent = 1701,

        /// <summary>
        /// Отчёт СЗВ-стаж
        /// </summary>
        SzvExpCalendarEvent = 1800,

        /// <summary>
        /// Транспортный налог
        /// </summary>
        TransportTaxEvent = 1900,

        /// <summary>
        /// Отчёт СЗВ-ТД
        /// </summary>
        SzvTdCalendarEvent = 2000,

        /// <summary>
        /// Отчёт СЗВ-ТД: при приеме/увольнении
        /// </summary>
        SzvTdCalendarUserEvent = 2001,

        /// <summary>
        /// Заявка аутсорсу на "Налог на имещуство"
        /// </summary>
        EstateTaxOsnoRequestEvent = 2050,
        EstateTaxUsnRequestEvent = 2051,

        /// <summary>
        /// Земельный налог
        /// </summary>
        LandTaxEvent = 2100,

        /// <summary>
        /// Оплатить аванс по НДФЛ за ... квартал ... года
        /// </summary>
        NdflAdvancePaymentCalendarEvent = 2200,

        /// <summary>
        /// Подать декларацию 3-НДФЛ и оплатить налог за год
        /// </summary>
        Fns3NdflDeclarationCalendarEvent = 2201,

        /// <summary> Сдать расчет по НДФЛ за ... с 2021 г</summary>
        Ndfl6CalendarEvent = 2300,

        /// <summary>
        /// Сверка с налоговой
        /// </summary>
        TaxReconciliationCalendarEvent = 2400,

        /// <summary> Оплатить налоги/взносы единым платежом (ЕНП) ... с 2023 г</summary>
        EnpCalendarEvent = 2500,

        /// <summary> Оплатить НДФЛ за первую половину месяца по ЕНП с 2024 г</summary>
        NdflEnpPart1CalendarEvent = 2501,

        /// <summary> Оплатить НДФЛ за вторую половину месяца по ЕНП с 2024 г</summary>
        NdflEnpPart2CalendarEvent = 2502,

        /// <summary> Оплатить взносы за сотрудников за ... c 2024 года (ЕНП)</summary>
        SfrEnpCalendarEvent = 2503,

        /// <summary>
        /// Отчёт Персонифицированные сведения о физических лицах в ФНС
        /// </summary>
        PsvCalendarEvent = 2600,

        /// <summary>
        /// Отчёт ЕФС-1: Сведения о заключении/расторжении ТД и ГПД
        /// </summary>
        EfsEmploymentCalendarUserEvent = 2700,

        /// <summary>Отчёт ЕФС-1: Сведения о трудовой деятельности</summary>
        EfsEmploymentHistoryCalendarEvent = 2701,

        /// <summary>Отчёт ЕФС-1: Сведения о взносах на травматизм</summary>
        EfsInjuredCalendarEvent = 2702,
        
        /// <summary>
        /// Отчёт ЕФС-1: Сведения о страховом стаже
        /// </summary>
        EfsExperienceCalendarEvent = 2703,

        /// <summary>
        /// Отчет ЕФС-1: Сведения о выходе в декретный/отпуск по уходу
        /// </summary>
        EfsChildCareCalendarUserEvent = 2704,

        /// <summary>
        /// Кастомное пользовательское событие
        /// </summary>
        CustomUserEvent = 2800,

        /// <summary>
        /// Кастомное событие из партнёрки
        /// </summary>
        CustomCommonEvent = 2801,

        /// <summary>
        /// Срок действия ЭЦП истекает
        /// </summary>
        EdsExpiringUserEvent = 2900,
        
        /// <summary>
        /// Срок действия ЭЦП истек
        /// </summary>
        EdsExpiredUserEvent = 2901,
    }
}