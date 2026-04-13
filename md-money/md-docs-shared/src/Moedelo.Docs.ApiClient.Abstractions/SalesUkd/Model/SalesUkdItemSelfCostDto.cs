namespace Moedelo.Docs.ApiClient.Abstractions.SalesUkd.Model;

public class SalesUkdItemSelfCostDto
{
    /// <summary>
    /// Идентификатор позиции
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Идентификатор товара/материала
    /// </summary>
    public long StockProductId { get; set; }

    /// <summary>
    /// Кол-во товара/материала до корректировки
    /// </summary>
    public decimal CountBefore { get; set; }

    /// <summary>
    /// Кол-во товара/материала после корректировки
    /// </summary>
    public decimal CountAfter { get; set; }
}