using Moedelo.Common.Enums.Enums.Stocks;
using System;

namespace Moedelo.StockV2.Dto.StockOrders
{
    /// <summary>
    /// Заказ (заказ поставщику, заказ покупателя, сборка), ссылающийся на продукт, из-за чего продукт невозможно удалить
    /// </summary>
    public class OrderToProductReferenceResponse
    {
        /// <summary>
        /// ID заказа/сборки
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// ID товара, на коий ссылается заказ
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Номер заказа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата заказа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Тип заказа (поставщику, от покупателя, сборка)
        /// </summary>
        public StockOrderType Type { get; set; }
    }
}
