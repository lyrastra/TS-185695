namespace Moedelo.Billing.Abstractions.Bills.Dto.Marketplace;

/// <summary>
/// Данные пакета/продуктовой услуги
/// </summary>
public class MarketplacePackageDto
{
    /// <summary>
    /// Идентификатор пакета/продуктовой услуги
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Код продуктовой услуги
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Название пакета/продуктовой услуги
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Значения модификатора "Срок действия" пакета
    /// </summary>
    public int[] Duration { get; set; }

    /// <summary>
    /// Значения модификатора "Срок действия" пакета. доступные для автопродления
    /// </summary>
    public int[] AutoRenewalDurations { get; set; }

    /// <summary>
    /// Технический код продукта
    /// </summary>
    public string ProductCode { get; set; }

    /// <summary>
    /// Предоплаченные услуги. Разделение выполнено для удобства отображения на фронте
    /// </summary>
    public MarketplaceOptionDto[] BaseOptions { get; set; }

    /// <summary>
    /// Дополнительные, настраиваемые услуги
    /// </summary>
    public MarketplaceOptionDto[] AdditionalOptions { get; set; }
}