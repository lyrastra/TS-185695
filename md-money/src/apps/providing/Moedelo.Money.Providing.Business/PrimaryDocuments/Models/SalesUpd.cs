using System;
using Moedelo.AccPostings.Enums;
using Moedelo.Money.Enums;

namespace Moedelo.Money.Providing.Business.PrimaryDocuments.Models
{
    internal class SalesUpd
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public decimal Sum { get; set; }

        public int KontragentId { get; set; }

        public SyntheticAccountCode KontragentAccountCode { get; set; }

        public TaxationSystemType? TaxationSystemType { get; set; }

        /// <summary>
        /// Номер забытого документа
        /// </summary>
        public string ForgottenDocumentNumber { get; set; }

        /// <summary>
        /// Дата забытого документа
        /// </summary>
        public DateTime? ForgottenDocumentDate { get; set; }
    }
}
