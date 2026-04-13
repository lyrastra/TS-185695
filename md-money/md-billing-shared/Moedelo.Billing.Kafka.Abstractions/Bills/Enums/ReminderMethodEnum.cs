namespace Moedelo.Billing.Kafka.Abstractions.Bills.Enums;

/// <summary>
/// Причина отправки напоминания об оплате счета
/// </summary>
public enum ReminderMethodEnum
{
    /// <summary>
    /// на 5 день после выставления счета
    /// </summary>
    FiveDaysAfterBilling,
    
    /// <summary>
    /// на 9 день после выставления счета
    /// </summary>
    NineDaysAfterBilling,
    
    /// <summary>
    /// За день до окончания срока действия счета 
    /// </summary>
    DayBeforeBillExpires,
}