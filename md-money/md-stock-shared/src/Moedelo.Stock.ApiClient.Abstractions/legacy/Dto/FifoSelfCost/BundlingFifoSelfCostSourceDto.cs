using System;

namespace Moedelo.Stock.ApiClient.Abstractions.legacy.Dto.FifoSelfCost
{
    /// <summary>
    /// Комплектация в разрезе расчета себестоимости ФИФО
    /// </summary>
    public class BundlingFifoSelfCostSourceDto
    {
        /// <summary>
        /// Идентификатор (сквозной по всем типам документов)
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime DocumentDate { get; set; }

        /// <summary>
        /// Идентификатор комплектуемого товара
        /// </summary>
        public long BundleId { get; set; }

        /// <summary>
        /// Количество созданных комплектов
        /// </summary>
        public decimal BundleCount { get; set; }

        /// <summary>
        /// Позиции комплектации (списание составляющих)
        /// </summary>
        public ItemDto[] Items { get; set; }
        
        public class ItemDto
        {
            /// <summary>
            /// Идентификатор составляющей комплекта
            /// </summary>
            public long ComponentId { get; set; }

            /// <summary>
            /// Количество, списанное в рамках комплектации
            /// </summary>
            public decimal Count { get; set; }

            /// <summary>
            /// Сумма, списанное в рамках комплектации (до ФИФО - с/с составляющей)
            /// </summary>
            public decimal Sum { get; set; }
        }
    }
}