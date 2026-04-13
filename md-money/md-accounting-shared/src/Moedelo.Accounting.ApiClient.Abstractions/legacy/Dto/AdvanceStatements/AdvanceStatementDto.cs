using Moedelo.Accounting.Enums;
using Moedelo.Accounting.Enums.Documents;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.AdvanceStatements
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

        public List<AdvanceStatementPaymentSupplierItemDto> PaymentToSupplierItems { get; set; } = Array.Empty<AdvanceStatementPaymentSupplierItemDto>().ToList();

        public List<AdvanceStatementBusinessTripItemDto> BusinessTripItems { get; set; } = Array.Empty<AdvanceStatementBusinessTripItemDto>().ToList();

        public List<AdvanceStatementProductAndMaterialItemDto> ProductAndMaterialItems { get; set; } = Array.Empty<AdvanceStatementProductAndMaterialItemDto>().ToList();

        public ProvidePostingType TaxPostingType { get; set; }
    }
}
