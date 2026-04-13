namespace Moedelo.Billing.Abstractions.Bills.Dto.Marketplace;

/// <summary>
/// Значение модификатора
/// </summary>
public class MarketplaceModifierValueDto
{
    /// <summary>
    /// Текстовое значение модификатора
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Булево значение модификатора
    /// </summary>
    public bool IsOn { get; set; }

    /// <summary>
    /// Целочисленное значение модификатора для счётчика на фронтенде
    /// </summary>
    public int Count { get; set; }

    /// <summary>
    /// Минимальное значение счётчика
    /// </summary>
    public int Min { get; set; }

    /// <summary>
    /// Макисмальное значение счётчика
    /// </summary>
    public int Max { get; set; }

    /// <summary>
    /// Приращение значения счётчика
    /// </summary>
    public int Step { get; set; }

    /// <summary>
    /// Переопределенное значение модификатора
    /// </summary>
    public int? OverriddenValue { get; set; }

    /// <summary>
    /// Массив значений градаций модификатора
    /// </summary>
    public MarketplaceModifierGradationDto[] Options { get; set; }
}