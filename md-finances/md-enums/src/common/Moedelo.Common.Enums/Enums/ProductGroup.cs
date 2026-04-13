namespace Moedelo.Common.Enums.Enums
{
    /// <summary> ProductGroup </summary>
    public enum ProductGroups
    {
        FREE = 0,

        BIZ = 1,

        SPS = 2,

        OUT = 3,

        CASH = 4,
    }

    /// <summary>
    /// готовые строковые представления для значений enum ProductGroups 
    /// </summary>
    public static class ProductGroup
    {
        public static readonly string Free = ProductGroups.FREE.ToString();
        public static readonly string Biz = ProductGroups.BIZ.ToString();
        public static readonly string Sps = ProductGroups.SPS.ToString();
        public static readonly string Out = ProductGroups.OUT.ToString();
        public static readonly string Cash = ProductGroups.CASH.ToString();
    }

    public enum TariffProduct
    {
        /// <summary>
        /// Не указана
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// Интернет-бухгалтерия
        /// </summary>
        Accounting = 1,
        /// <summary>
        /// Аутсорсинг
        /// </summary>
        Outsourcing = 2,
        /// <summary>
        /// Бюро
        /// </summary>
        Sps = 3,
        /// <summary>
        /// Кассы
        /// </summary>
        Cash = 4,
        /// <summary>
        /// Профессиональный ассистент
        /// </summary>
        PersonalAssistant = 5,
        /// <summary>
        /// Проф. бухгалтер
        /// </summary>
        ProfessionalAccounting = 6,
        /// <summary>
        /// Доставка
        /// </summary>
        Shipping = 7,
    }
}