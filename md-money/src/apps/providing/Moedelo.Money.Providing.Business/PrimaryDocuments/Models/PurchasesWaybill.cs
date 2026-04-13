using Moedelo.AccPostings.Enums;
using Moedelo.Money.Enums;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Providing.Business.PrimaryDocuments.Models
{
    class PurchasesWaybill
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public int KontragentId { get; set; }

        public SyntheticAccountCode KontragentAccountCode { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        public IReadOnlyCollection<WaybillItem> Items { get; set; }

        public bool IsCompensated { get; set; }

        public bool IsFromFixedAssetInvestment { get; set; }

        /// <summary>
        /// Номер забытого документа
        /// </summary>
        public string ForgottenDocumentNumber { get; set; }

        /// <summary>
        /// Дата забытого документа
        /// </summary>
        public DateTime? ForgottenDocumentDate { get; set; }

        public ProvidePostingType TaxPostingType { get; set; }
    }

    public class WaybillItem
    {
        public long? StockProductId { get; set; }

        public decimal SumWithNds { get; set; }

        public decimal SumWithoutNds { get; set; }
    }
}
