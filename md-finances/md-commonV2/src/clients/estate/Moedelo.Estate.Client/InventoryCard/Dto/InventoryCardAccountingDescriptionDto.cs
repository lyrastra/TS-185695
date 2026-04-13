using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.Estate.Client.InventoryCard.Dto
{
    public class InventoryCardAccountingDescriptionDto
    { 
        public int UsefulLife { get; set; }
        public int AmortizationAccountCode { get; set; }
        public int? DepartmentId { get; set; }
        public decimal Cost { get; set; }
        public decimal Amortization { get; set; }
        public decimal AmortizationInBalance { get; set; }
        public NomenclatureGroupCode? NomenclatureGroupCode { get; set; }
    }
}
