using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.StockV2.Dto.Stocks
{
    public class StockDocumentTaxInfoDto
    {
        public long Id { get; set; }

        public long? DocumentBaseId { get; set; }

        public bool IsNotTax { get; set; }

        public string Message { get; set; }

        public int? AccountCode { get; set; }
        
        public NomenclatureGroupCode? NomenclatureGroupCode { get; set; }
    }
}
