namespace Moedelo.Stock.Enums
{
    public enum MarketplaceCodeType
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        Id = 1,

        /// <summary>
        /// Штрих-код
        /// </summary>
        Barcode = 2,

        /// <summary>
        /// Артикул товара от производителя
        /// </summary>
        VendorCode = 3,

        /// <summary>
        /// Гдето соответствует идентификатору гдето соответствует артикулу гдето самостоятельный дополнительный идентификатор
        /// </summary>
        Sku = 4
    }
}
