namespace Moedelo.Common.Enums.Enums.Calendar
{
    /// <summary>
    /// Типы календарных событий.
    /// Используются по именам.
    /// ВОЗМОЖНО лучше использовать CalendarEventProtoId
    /// </summary>
    public enum CalendarEventTypeName
    {
        TradingTaxPaymentCalendarEvent,
        CloseAccountingYearCalendarEvent,
        CloseAccountingPeriodCalendarEvent,
        DeclarationByUSN2010CalendarEvent,
        DeclarationByUSN2011CalendarEvent,
        DeclarationByUSN2012CalendarEvent,
        DeclarationByUSN2013CalendarEvent,
        DeclarationByUSN2014CalendarEvent,
        DeclarationByUSN2015CalendarEvent
    }
}