using System.ComponentModel;

namespace Moedelo.AccPostings.Enums
{
    public enum NomenclatureGroupCode
    {
        /// <summary>
        /// Реализация работ и услуг
        /// </summary>
        [Description("Реализация работ и услуг")]
        Service = 0,

        /// <summary>
        /// Реализация товар
        /// </summary>
        [Description("Реализация товаров")]
        ProductSales = 1,

        /// <summary>
        /// Производство
        /// </summary>
        [Description("Реализация продукции")]
        Manufacturing = 2
    }
}
