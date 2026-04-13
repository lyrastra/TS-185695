using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.AccountingV2.Dto.StockDebitOperation
{
    public class SubcontoDto
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public SubcontoType SubcontoType { get; set; }

        public long SubcontoId { get; set; }
    }
}
