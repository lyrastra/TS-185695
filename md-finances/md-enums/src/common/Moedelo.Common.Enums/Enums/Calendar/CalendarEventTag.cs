namespace Moedelo.Common.Enums.Enums.Calendar
{
    public enum CalendarEventTag
    {
        /// <summary>
        /// Отчёт
        /// </summary>
        Report = 1,
        /// <summary>
        /// Закрытие (месяца, года etc)
        /// </summary>
        ClosingPeriod = 2,
        /// <summary>
        /// Оплата
        /// </summary>
        Payment = 3,
        /// <summary>
        /// Ввод остатков
        /// </summary>
        Balances = 4,
        /// <summary>
        /// Событие в "Учётной системе"
        /// </summary>
        Accounting = 5,
        /// <summary>
        /// Событие в БИЗ
        /// </summary>
        Biz = 6,
        /// <summary>
        /// Это событие нельзя закрыть в рамках операции "Закрыть все события указанного года"
        /// </summary>
        ExcludeFromYearlyBatchComplete = 7,
        /// <summary>
        /// бывшие наследники EnvdCalendarEventBase
        /// </summary>
        Envd = 100,
        /// <summary>
        /// бывшие наследники RptWizardEvent
        /// </summary>
        RptWizard = 101,
        /// <summary>
        /// бывшие наследники WizardEvent
        /// </summary>
        Wizard = 102,
        /// <summary>
        /// бывшие наследники PayrollTaxesCalendarEvent
        /// </summary>
        PayrollTaxes = 103,
        /// <summary>
        /// При завершении/переоткрытии события с данным тегом необходимо уведомить 
        /// движок мастеров отчетности
        /// </summary>
        WizardEngine = 104
    }
}