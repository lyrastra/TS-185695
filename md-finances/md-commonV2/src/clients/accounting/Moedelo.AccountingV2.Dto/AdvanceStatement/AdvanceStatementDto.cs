using Moedelo.Common.Enums.Enums.Accounting;
using Moedelo.Common.Enums.Enums.Documents;
using System;
using System.Collections.Generic;

namespace Moedelo.AccountingV2.Dto.AdvanceStatement
{
    public class AdvanceStatementDto
    {
        public long Id { get; set; }

        public long DocumentBaseId { get; set; }

        public string Number { get; set; }

        public DateTime Date { get; set; }

        public int WorkerId { get; set; }

        public bool ProvideInAccounting { get; set; }

        public decimal Sum { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        public AdvanceStatementType Type { get; set; }

        public IReadOnlyCollection<AdvanceStatementPaymentSupplierItemDto> PaymentToSupplierItems { get; set; } = Array.Empty<AdvanceStatementPaymentSupplierItemDto>();

        public IReadOnlyCollection<AdvanceStatementBusinessTripItemDto> BusinessTripItems { get; set; } = Array.Empty<AdvanceStatementBusinessTripItemDto>();

        public IReadOnlyCollection<AdvanceStatementProductAndMaterialItemDto> ProductAndMaterialItems { get; set; } = Array.Empty<AdvanceStatementProductAndMaterialItemDto>();

        public ProvidePostingType TaxPostingType { get; set; }
    }
}
