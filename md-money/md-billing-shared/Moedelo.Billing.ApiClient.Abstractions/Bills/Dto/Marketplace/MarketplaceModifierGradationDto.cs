namespace Moedelo.Billing.Abstractions.Bills.Dto.Marketplace;

/// <summary>
/// Градация модификатора
/// </summary>
public class MarketplaceModifierGradationDto
{
    /// <summary>
    /// Идентификатор градации
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Значение
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Нижняя граница значений градации
    /// </summary>
    public double Min { get; set; }

    /// <summary>
    /// Верхняя граница значений градации
    /// </summary>
    public double Max { get; set; }
}