namespace Moedelo.Common.Enums.Enums.TaxPostings
{
    public enum OsnoTransferKind
    {
        None = 0,

        /// <summary>
        /// Реализация услуг
        /// </summary>
        Service = 101,

        /// <summary>
        /// Реализация покупных товаров
        /// </summary>
        ProductSale = 102,

        /// <summary>
        /// Реализация имущественных прав
        /// </summary>
        PropertyRight = 103,

        /// <summary>
        /// Реализация прочего имущества
        /// </summary>
        OtherPropertySale = 104,

        /// <summary>
        /// Реализация продукции
        /// </summary>
        ManufacturedProductSale = 105,

        /// <summary>
        /// Материальные (расход)
        /// </summary>
        Material = 201,

        /// <summary>
        /// Оплата труда
        /// </summary>
        Salary = 202,

        /// <summary>
        /// Амортизация
        /// </summary>
        Amortization = 203,

        /// <summary>
        /// Прочий расход
        /// </summary>
        OtherOutgo = 204,

        /// <summary>
        /// Производство
        /// </summary>
        Manufacturing = 205
    }
}