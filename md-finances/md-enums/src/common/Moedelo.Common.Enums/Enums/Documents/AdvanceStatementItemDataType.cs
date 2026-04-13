using System.ComponentModel;

namespace Moedelo.Common.Enums.Enums.Documents
{
    public enum AdvanceStatementItemDataType
    {
        Default = 0,

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
        /// Командировочные
        /// </summary>
        [Description("Командировочные")] 
        BusinessTrip = 4,

        /// <summary>
        /// Хозяйственные расходы
        /// </summary>
        [Description("Хозяйственные расходы")] 
        AdministrativeExpenses = 5,

        /// <summary>
        /// Товары БИЗ
        /// </summary>
        [Description("Товары")] 
        BizProduct = 6,

        /// <summary>
        /// Материалы БИЗ
        /// </summary>
        [Description("Материалы")] 
        BizMaterial = 7,

        /// <summary>
        /// Основное средство
        /// </summary>
        [Description("Основное средство")] 
        FixedAssets = 8,
    }
}
