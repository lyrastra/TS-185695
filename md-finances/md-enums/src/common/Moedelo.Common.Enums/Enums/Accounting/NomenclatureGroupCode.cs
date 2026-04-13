using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Accounting
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
        Manufacturing = 2,

        /// <summary>
        /// Гостиничные услуги
        /// </summary>
        [Description("Гостиничные услуги")]
        HotelServices = 3
    }
}