namespace Moedelo.Billing.Abstractions.Bills.Dto;

public class OverridenModifierRequest
{
    /// <summary>
    /// Код типа модификатора
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Наименование рассчитанной градации
    /// </summary>
    public string GradationName { get; set; }

    /// <summary>
    /// Рассчитанное значение
    /// </summary>
    public decimal? Value { get; set; }
}