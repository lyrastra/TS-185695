using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.Finances.Public.ClientData.Money
{
    public class TaxSumRecClientData
    {
        public TaxationSystemType TaxType { get; set; }
        public decimal Sum { get; set; } = 0;
    }
}