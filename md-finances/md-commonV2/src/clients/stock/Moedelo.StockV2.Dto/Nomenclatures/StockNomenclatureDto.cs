using System.Collections.Generic;
using Moedelo.Common.Enums.Enums.Stocks;

namespace Moedelo.StockV2.Dto.Nomenclatures
{
    public class StockNomenclatureDto
    {
        public StockNomenclatureDto()
        {
            ChildNomenclatures = new List<StockNomenclatureDto>();
        }

        public StockNomenclatureDto(IList<StockNomenclatureDto> childCategory)
        {
            ChildNomenclatures = childCategory;
        }

        public long Id { get; set; }

        public long ParentId { get; set; }

        public string Name { get; set; }

        public int TemporaryId { get; set; }

        public NomenclatureType NomenclatureType { get; set; }

        public IList<StockNomenclatureDto> ChildNomenclatures { get; set; }
    }
}