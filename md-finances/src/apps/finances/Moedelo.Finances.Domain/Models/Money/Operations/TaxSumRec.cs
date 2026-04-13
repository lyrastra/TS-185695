using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.Finances.Domain.Models.Money.Operations
{
    public class TaxSumRec
    {
        public TaxationSystemType TaxType { get; set; }
        public decimal Sum { get; set; } = 0;
    }
}