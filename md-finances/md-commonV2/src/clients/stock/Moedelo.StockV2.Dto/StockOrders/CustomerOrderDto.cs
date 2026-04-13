using System;

namespace Moedelo.StockV2.Dto.StockOrders
{
    /// <summary>
    /// Заказ от покупателя
    /// </summary>
    public sealed class CustomerOrderDto
    {
        /// <summary>
        /// Идентификатор заказа
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор контрагента
        /// </summary>
        public int KontragentId { get; set; }

        /// <summary>
        /// Номер заказа
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Дата заказа
        /// </summary>
        public DateTime Date { get; set; }
    }
}