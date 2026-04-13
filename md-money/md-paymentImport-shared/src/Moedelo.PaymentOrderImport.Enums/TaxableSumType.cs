namespace Moedelo.PaymentOrderImport.Enums;

public enum TaxableSumType
{
    /// <summary>
    /// Вся сумма операции
    /// </summary>
    Full = 2,
    /// <summary>
    /// Сумма операции - НДС (если есть)
    /// </summary>
    WithoutNds = 3
}