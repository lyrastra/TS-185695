using Moedelo.Stock.Enums;

namespace Moedelo.Money.Providing.Business.Stock.Models
{
    public class StockProduct
    {
        public long Id { get; set; }

        /// <summary>
        /// Тип продукта: товар/материал
        /// </summary>
        public StockProductTypeEnum Type { get; set; }
    }
}
