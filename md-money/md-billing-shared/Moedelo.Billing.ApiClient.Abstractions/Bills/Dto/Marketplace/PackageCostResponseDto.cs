using System.Collections.Generic;

namespace Moedelo.Billing.Abstractions.Bills.Dto.Marketplace;

/// <summary>
/// Результат рассчитанной стоимости пакета/продуктовой услуги
/// </summary>
public class PackageCostResponseDto
{
    /// <summary>
    /// Идентификатор продуктовой услуги
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Срок действия (длительность)
    /// </summary>
    public string Duration { get; set; }

    /// <summary>
    /// окончательная цена
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// Сумма скидки
    /// </summary>
    public decimal DiscountSum => NormativeTotal - Total;

    /// <summary>
    /// цена до применения промокода
    /// </summary>
    public decimal NormativeTotal { get; set; }

    /// <summary>
    /// Стоимость в месяц
    /// </summary>
    public decimal CostPerMonth { get; set; }

    /// <summary>
    /// Позиции
    /// </summary>
    public IReadOnlyCollection<Position> Positions { get; set; }

    /// <summary>
    /// Технический код продукта
    /// </summary>
    public string ProductCode { get; set; }

    /// <summary>
    /// Текст подсказки к пакету для отображения на фронтенде
    /// </summary>
    public string TooltipText { get; set; }

    /// <summary>
    /// Позиция
    /// </summary>
    public struct Position
    {
        /// <summary>
        /// Наименование позиции
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Идентификаторы типов модификаторов
        /// </summary>
        public IReadOnlyCollection<int> ModifierTypeIds { get; set; }

        /// <summary>
        /// Технический код позиции (может быть пустым)
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Стоимость
        /// </summary>
        public decimal TotalSum { get; set; }

        /// <summary>
        /// Срок действия (длительность)
        /// </summary>
        public string Duration { get; set; }

        /// <summary>
        /// Наменование позиции для отображения в корзине
        /// Наименование ПУ для первой позиции в корзине или наименование ГФ для остальных
        /// </summary>
        public string CartPositionName { get; set; }

        /// <summary>
        /// Признак разовой услуги
        /// </summary>
        public bool IsOneTime { get; set; }

        /// <summary>
        /// Количество месяцев (м.б. не указано)
        /// </summary>
        public int? MonthCount { get; set; }
    }
}