using System;

namespace Moedelo.PaymentImport.Dto
{
    public class IdentifyOperationTaxationSystemResponseDto
    {
        public Guid Guid { get; set; }

        public int? ImportRuleId { get; set; }

        public int? TaxationSystem { get; set; }
    }
}
