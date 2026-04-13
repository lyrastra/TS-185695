using Moedelo.Common.Enums.Enums.Documents;

namespace Moedelo.StockV2.Dto.ClosingPeriodValidations
{
    public class StockOperationsCountDto
    {
        public AccountingDocumentType Type { get; set; }
        public int DocumentType { get; set; }
        public int Count { get; set; }
    }
}