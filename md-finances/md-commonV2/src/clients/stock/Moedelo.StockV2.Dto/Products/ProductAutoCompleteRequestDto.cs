using Moedelo.Common.Enums.Enums.Stocks;

namespace Moedelo.StockV2.Dto.Products
{
    public class ProductAutoCompleteRequestDto
    {
        public enum DocumentDirection
        {
            Incoming = 0,
            Outgoing = 1
        }

        public string Query { get; set; } = string.Empty;
        public int Count { get; set; } = 10;
        public long? StockId { get; set; } = null;
        public StockProductTypeEnum ProductType { get; set; } = StockProductTypeEnum.All;
        /// <summary>
        /// Для входящего или исходящего документа нужен автокомплит по позиции
        /// </summary>
        public DocumentDirection TargetDocumentDirection { get; set; } = DocumentDirection.Incoming;
    }
}