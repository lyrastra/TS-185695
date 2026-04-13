using System;
using Moedelo.Common.Enums.Enums.Estate;

namespace Moedelo.Estate.Client.InventoryCard.Dto
{
    public class InventoryCardTaxDescriptionDto
    {
        public bool NeedStateRegistration { get; set; }
        public DateTime? DateOfStateRegistration { get; set; }
        public DateTime? CommissioningDate { get; set; }
        public int UsefulLife { get; set; }
        public decimal WrittenOnExpenses { get; set; }
        public decimal Amortization { get; set; }
        public decimal AmortizationInBalance { get; set; }
        public decimal Cost { get; set; }
        public decimal? PaidCost { get; set; }
        public virtual AccountedFor? AccountedFor { get; set; }
    }
}
