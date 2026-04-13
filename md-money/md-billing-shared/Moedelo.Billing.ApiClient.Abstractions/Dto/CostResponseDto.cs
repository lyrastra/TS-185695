using System.Collections.Generic;

namespace Moedelo.Billing.Abstractions.Dto;

public class CostResponseDto
{
    public int Id { get; set; }

    /// <summary>
    /// Код ПУ
    /// </summary>
    public string ProductConfigurationCode { get; set; }

    public string Duration { get; set; }

    /// <summary>
    /// окончательная цена
    /// </summary>
    public decimal Total { get; set; }

    /// <summary>
    /// цена до применения промокода
    /// </summary>
    public decimal NormativeTotal { get; set; }

    public decimal CostPerMonth { get; set; }

    public IReadOnlyCollection<Position> Positions { get; set; }

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

        public string Duration { get; set; }

        /// <summary>
        /// Наименование позиции для отображения в корзине
        /// Наименование ПУ для первой позиции в корзине или наименование ГФ для остальных
        /// </summary>
        public string CartPositionName { get; set; }

        /// <summary>
        /// Признак разовой услуги
        /// </summary>
        public bool IsOneTime { get; set; }
    }
}