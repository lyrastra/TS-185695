namespace Moedelo.Common.Enums.Enums.Wizard
{
    public enum WizardType
    {
        Unknown = 0,

        /// <summary>
        /// Декларация по НДС
        /// </summary>
        NdsDeclaration = 1,

        /// <summary>
        /// Декларация по налогу на прибыль
        /// </summary>
        ProfitDeclaration = 2,

        /// <summary>
        /// Бухгалтерский баланс и отчет о прибылях и убытках
        /// </summary>
        BalanceAndIncomeReport = 3,

        /// <summary>
        /// Авансовый платеж по налогу на прибыль
        /// </summary>
        ProfitAdvancePayment = 4,

        /// <summary>
        /// Бухгалтерская отчетность до 2024 года включительно
        /// </summary>
        AccountingReportUntil2024 = 5,

        /// <summary>
        /// Мастер закрытия периода Биз
        /// </summary>
        CloseBizPeriod = 6,

        /// <summary>
        /// Декларация по налогу на имущество
        /// </summary>
        EstateDeclaration = 7,

        /// <summary>
        /// Ответ на требование о предоставлении пояснений по НДС
        /// </summary>
        NdsExplanation = 8,

        /// <summary>
        /// Уведомление ИФНС о торговом сборе
        /// </summary>
        TradingTaxNotification = 9,

        /// <summary>
        /// Оплата торгового сбора
        /// </summary>
        TradingTaxPayment = 10,

        /// <summary>
        /// Отчет в Росстат
        /// </summary>
        Rosstat = 11,

        /// <summary>
        /// Отчитаться в налоговую и заплатить налог по УСН
        /// </summary>
        DeclarationByUsn = 12,

        /// <summary>
        /// Отчет по форме СЗВ-М
        /// </summary>
        Szvm = 13,

        /// <summary>
        /// Мастер заполнения реквизитов
        /// </summary>
        RequisitesMaster = 14,

        /// <summary>
        /// Расчет по страховым взносам
        /// </summary>
        Rsv = 15,

        /// <summary>
        /// Отчет по страховым взносам в ФСС
        /// </summary>
        Fss = 16,

        /// <summary>
        /// Отправка заявки на декларацию по налогу на имущество (ОСНО)
        /// </summary>
        EstateDeclarationOsnoRequest = 17,

        /// <summary>
        /// Отправка заявки на декларацию по налогу на имущество (УСН)
        /// </summary>
        EstateDeclarationUsnRequest = 18,

        /// <summary>
        /// Авансы по транспортному налогу
        /// </summary>
        TransportTaxAdvance = 19,

        /// <summary>
        /// Отчёт СЗВ-стаж
        /// </summary>
        Szv = 20,

        /// <summary>
        /// Фиксированные взносы в ПФР
        /// </summary>
        PfrFixes = 21,

        /// <summary>
        /// Патент
        /// </summary>
        Patent = 22,

        /// <summary>
        /// Закрытие ИП
        /// </summary>
        ClosingIp = 23,

        /// <summary>
        /// Авансовые платежи по 3-НДФЛ до 2024 года включительно
        /// </summary>
        Ndfl3AdvancePaymentUntil2024 = 24,

        /// <summary>
        /// Декларация по 3-НДФЛ до 2024 года включительно
        /// </summary>
        Ndfl3DeclarationUntil2024 = 25,

        /// <summary>
        /// Мастер годовой отчетности ГИС ЖКХ
        /// </summary>
        GisGkhYearReport = 26,

        /// <summary>
        /// Мастер ежемесячной отчетности ГИС ЖКХ
        /// </summary>
        GisGkhMonthReport = 27,

        /// <summary>
        /// Мастер передачи МКД (многоквартирный дом) в другую управляющую компанию
        /// </summary>
        GisGkhTransferMkd = 28,

        /// <summary>
        /// Ответ на требование налоговой
        /// </summary>
        ResponseToDemand = 29,

        /// <summary>
        /// Сверка с налоговой
        /// </summary>
        TaxReconciliation = 30,

        /// <summary>
        /// Мастер единого налогового платежа
        /// </summary>
        Enp = 31,

        /// <summary>
        /// Фиксированные взносы в СФР
        /// </summary>
        SfrFixes = 32,

        /// <summary>
        /// Авансовые платежи по 3-НДФЛ
        /// </summary>
        Ndfl3AdvancePayment = 33,

        /// <summary>
        /// Бухгалтерская отчетность
        /// </summary>
        AccountingReport = 34,

        /// <summary>
        /// Декларация по 3-НДФЛ
        /// </summary>
        Ndfl3Declaration = 35
    }
}
