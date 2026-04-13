using Moedelo.Billing.Shared.Enums;

namespace Moedelo.Billing.Abstractions.Bills.Dto.Marketplace;

/// <summary>
/// Опция услуги
/// </summary>
public class MarketplaceOptionDto
{
    /// <summary>
    /// Идентификатор опции (код модификатора)
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Тип модификатора для отображения на фронтенде
    /// </summary>
    public MarketplaceModifierType? Type { get; set; }
    
    /// <summary>
    /// Строковое значение типа модификатора для отображения на фронтенде
    /// </summary>
    public string ModifierType { get; set; }

    /// <summary>
    /// Единицы измерения параметра, описываемого модификатором
    /// </summary>
    public string Measure { get; set; }

    /// <summary>
    /// Значение модификатора
    /// </summary>
    public MarketplaceModifierValueDto Value { get; set; }
}