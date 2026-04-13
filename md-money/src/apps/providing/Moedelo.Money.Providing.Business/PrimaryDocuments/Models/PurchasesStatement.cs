using Moedelo.AccPostings.Enums;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Providing.Business.PrimaryDocuments.Models
{
    class PurchasesStatement
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public int KontragentId { get; set; }

        public SyntheticAccountCode KontragentAccountCode { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        public bool IsCompensated { get; set; }

        public bool IsFromFixedAssetInvestment { get; set; }

        public ProvidePostingType TaxPostingType { get; set; }
        
        public IReadOnlyList<PurchasesStatementItem> Items { get; set; }
    }

    class PurchasesStatementItem
    {
        public decimal SumWithNds { get; set; }

        public decimal SumWithoutNds { get; set; }
    }
}
