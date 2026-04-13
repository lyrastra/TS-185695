using Moedelo.Common.Enums.Enums.Accounting;

namespace Moedelo.AccountingV2.Dto.PurseOperation
{
    public class ChangeTaxationSystemDto
    {
        public long DocumentBaseId { get; set; }

        public TaxationSystemType TaxationSystemType { get; set; }

        public long? PatentId { get; set; }
    }
}
