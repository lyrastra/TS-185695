using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Stocks;

namespace Moedelo.StockV2.Dto.Stocks
{
    public class StockDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public StockType Type { get; set; }

        public bool IsMain { get; set; }

        public long? SubcontoId { get; set; }

        public string SubcontoName { get; set; }

        public SubcontoType SubcontoType { get; set; }
    }
}