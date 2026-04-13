using Moedelo.Common.Enums.Enums.Stocks;

namespace Moedelo.StockV2.Dto.Inventories
{
    public class InventoryLossReasonDto
    {
        public long Id { get; set; }

        /// <summary> Причина недостачи </summary>
        public LossReasonType Type { get; set; }

        /// <summary> Кол-во недостающей номенклатуры </summary>
        public decimal Count { get; set; }

        /// <summary> Счет расхода </summary>
        public int ExpenseAccount { get; set; }

        /// <summary> Субконто 1 </summary>
        public long? SubcontoId { get; set; }

        /// <summary> Субконто 2 </summary>
        public long? SubcontoSecondId { get; set; }

        /// <summary> Субконто 3 </summary>
        public long? SubcontoThirdId { get; set; }
    }
}