using System.ComponentModel;

namespace Moedelo.AccPostings.Enums
{
    public enum CostItemGroupCode
    {
        /// <summary>
        /// Прочие затраты
        /// </summary>
        [Description("Прочие затраты")]
        Other = 0,

        /// <summary>
        /// Сырье и материалы
        /// </summary>
        [Description("Сырье и материалы")]
        Materials = 1,

        /// <summary>
        /// Оплата труда
        /// </summary>
        [Description("Заработная плата")]
        Salary = 2,

        /// <summary>
        /// Амортизация
        /// </summary>
        [Description("Амортизация")]
        Amortization = 3,

        /// <summary>
        /// Страховые взносы
        /// </summary>
        [Description("Страховые взносы")]
        SocialPayments = 4,

        /// <summary>
        /// Налоги, сборы и другие платежи
        /// </summary>
        [Description("Налоги, сборы и другие платежи")]
        Taxes = 5,

        /// <summary>
        /// Командировочные расходы
        /// </summary>
        [Description("Командировочные расходы")]
        BusinessTripExpenses = 6,

        /// <summary>
        /// Работы и услуги сторонних лиц
        /// </summary>
        [Description("Работы и услуги сторонних лиц")]
        Services = 7,

        /// <summary>
        /// Недостатча в пределах нормы
        /// </summary>
        [Description("Недостачи в пределах норм")]
        DeficitNormal = 8,

        /// <summary>
        /// Доставка товаров
        /// </summary>
        [Description("Доставка товаров")]
        GoodsDelivery = 9
    }
}
