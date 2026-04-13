namespace Moedelo.StockV2.Dto.Stocks
{
    public class UnfinishedManufacturingItemDto
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Идентификатор отдела
        /// </summary>
        public int? DivisionId { get; set; }

        /// <summary>
        /// Идентификатор складского продукта в разрезе которого хранятся остатки 
        /// </summary>
        public long ProductId { get; set; }

        /// <summary>
        /// Остаток в незавершенном производстве (количество)
        /// </summary>
        public decimal Count { get; set; }

        /// <summary>
        /// Остаток в незавершенном производстве (сумма)
        /// </summary>
        public decimal Sum { get; set; }
    }
}