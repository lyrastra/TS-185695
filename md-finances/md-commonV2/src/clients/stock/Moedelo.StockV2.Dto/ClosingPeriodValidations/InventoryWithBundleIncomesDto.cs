using System;

namespace Moedelo.StockV2.Dto.ClosingPeriodValidations
{
    /// <summary>
    /// Инвентиризация с приходом комплекта(ов)
    /// </summary>
    public class InventoryWithBundleIncomesDto
    {
        /// <summary>
        /// Базовый идентификатор инвентаризации
        /// </summary>
        public long DocumentBaseId { get; set; }
        
        /// <summary>
        /// Номер инвентаризации
        /// </summary>
        public string Number { get; set; }
        
        /// <summary>
        /// Дата инвентаризации
        /// </summary>
        public DateTime Date { get; set; }
        
        /// <summary>
        /// Список комплектов, по которым зафиксирован излишек (приход)
        /// </summary>
        public Bundle[] Bundles { get; set; }
        
        /// <summary>
        /// Модель товара-комплекта
        /// </summary>
        public class Bundle
        {
            /// <summary>
            /// Идентификатор комплекта
            /// </summary>
            public long Id { get; set; }
            
            /// <summary>
            /// Название комплекта
            /// </summary>
            public string Name { get; set; }
        }
    }
}