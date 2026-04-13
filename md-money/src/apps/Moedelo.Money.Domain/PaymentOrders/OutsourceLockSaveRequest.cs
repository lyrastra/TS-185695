namespace Moedelo.Money.Domain.PaymentOrders;

/// <summary>
/// Для КАЖДОГО типа п/п: управление признаком "в жёлтой таблице"
/// </summary>
/// <remarks>
/// Признак устанавливается в импорте и снимается через подтверждение на странице "Массовая работа с выписками.
/// Прочие обновления не должны влиять на значение. Поэтому оформлено отдельной структурой. 
/// </remarks>
public struct OutsourceLockSaveRequest
{
    /// <summary> Значение признака </summary>
    public bool Value { get; set; }
    /// <summary>
    /// При обновлении: нужно ли сохранять значение признака
    /// </summary>
    public bool NeedSave { get; set; }

    public static OutsourceLockSaveRequest Disable => new OutsourceLockSaveRequest
    {
        Value = false,
        NeedSave = true
    };
    
    public static OutsourceLockSaveRequest Enable => new OutsourceLockSaveRequest
    {
        Value = true,
        NeedSave = true
    };
}