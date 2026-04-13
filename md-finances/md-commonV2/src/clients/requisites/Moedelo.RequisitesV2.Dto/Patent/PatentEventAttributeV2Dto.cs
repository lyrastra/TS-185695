using System;

namespace Moedelo.RequisitesV2.Dto.Patent
{
    public class PatentEventAttributeV2Dto
    {
        public long Id { get; set; }

        public int FirmId { get; set; }

        public long PatentId { get; set; }

        public int EventId { get; set; }

        public long? FinancialOperationBaseId { get; set; }

        public int EventOrder { get; set; }

        public decimal Sum { get; set; }

        public int? PaymentNumber { get; set; }

        public DateTime? PaymentDate { get; set; }

        public int? PaymentMethod { get; set; }

        public long? WizardId { get; set; }

        public decimal? TaxDeductionSum { get; set; }
    }
}