using System.ComponentModel;

namespace Moedelo.Docs.Enums
{
    public enum AdvanceStatementItemDataType
    {
        /// <summary>
        /// Товары и материалы
        /// </summary>
        [Description("Товары и материалы")] 
        ProductAndMaterial = 1,

        /// <summary>
        /// Оплата поставщикам
        /// </summary>
        [Description("Оплата поставщикам")] 
        PaymentToSupplier = 2,

        /// <summary>
        /// Работы и услуги
        /// </summary>
        [Description("Работы и услуги")] 
        Service = 3,

        /// <summary>
        /// Хозяйственные расходы
        /// </summary>
        [Description("Хозяйственные расходы")] 
        AdministrativeExpenses = 5,
    }
}