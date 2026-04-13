using Moedelo.AccPostings.Enums;
using Moedelo.Docs.Enums;
using System;
using System.Collections.Generic;
using TaxationSystemType = Moedelo.Money.Enums.TaxationSystemType;

namespace Moedelo.Money.Providing.Business.PrimaryDocuments.Models
{
    class PurchasesUpd
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public int KontragentId { get; set; }

        public SyntheticAccountCode KontragentAccountCode { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        public IReadOnlyCollection<UpdItem> Items { get; set; }

        /// <summary>
        /// Номер забытого документа
        /// </summary>
        public string ForgottenDocumentNumber { get; set; }

        /// <summary>
        /// Дата забытого документа
        /// </summary>
        public DateTime? ForgottenDocumentDate { get; set; }

        public Enums.ProvidePostingType TaxPostingType { get; set; }
    }

    public class UpdItem
    {
        public ItemType Type { get; set; }

        public long? StockProductId { get; set; }

        public decimal SumWithNds { get; set; }
        
        public decimal SumWithoutNds { get; set; }
    }
}
